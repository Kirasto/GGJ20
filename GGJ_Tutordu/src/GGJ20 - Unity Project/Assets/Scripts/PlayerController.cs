using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        Dash,
        Grap,
        _Count
    };

    public PlayerState playerState;

    public bool canMove = true;

    public float movementSpeed = 5f;
    private Vector3 lookDirection = Vector3.up;

    [Header("Dash")]
    public float dashSpeed = 1f;
    public float dashDuration = 1f;
    private float dashTimer = 1f;
    private Vector3 dashDirection;
    float dashCooldown = 0.0f;

    [Header("Shoot")]
    public GameObject projectilePrefab;
    public Transform projectilesParent;

    public float shootCooldownDuration = 1f;
    public float shootCooldownTimer = 0f;

    [Header("GrapplingHook")]
    public float grappleDistance = 1;
    public float grappleSpeed = 5;
    private bool isGrapKeyDown = false;

    private Vector3? grappleHitPoint = null;

    private GameObject myLine = null;
    private GameObject hook;
    public Material lineMat;
    public GameObject hookPref;

    // Other
    GameController gameController = null;

    private void Start()
    {
        gameController = GameController.instance;
    }

    private void Update()
    {
        if (!canMove) return;

        if (UpdateGrappling())
        {
            Move();
            Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (grappleHitPoint is Vector3 _grappleHitPoint)
        {
            DestroyGrapple();
            playerState = PlayerState.Idle;
        }
    }

    private bool UpdateGrappling()
    {
        if (grappleHitPoint is Vector3 _grappleHitPoint)
        {
            transform.position += (_grappleHitPoint - transform.position).normalized * Time.deltaTime * grappleSpeed;
            var lineRenderer = myLine.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, transform.position);

            if ((_grappleHitPoint - transform.position).magnitude <= 0.5)
            {
                DestroyGrapple();
                playerState = PlayerState.Idle;
            }
            return false;
        }
        return true;
    }

    private void Move()
    {
        if (playerState == PlayerState.Dash)
        {
            dashTimer -= Time.deltaTime;

            transform.position += dashDirection * Time.deltaTime * dashSpeed;

            if (dashTimer <= 0.0f)
            {
                dashTimer = 0.0f;
                playerState = PlayerState.Idle;

                dashCooldown = 0.5f;
            }
        }
        else
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            direction.Normalize();
            transform.position += direction * Time.deltaTime * movementSpeed;

            playerState = direction != Vector3.zero ? PlayerState.Walk : PlayerState.Idle;

            if (direction != Vector3.zero)
            {
                lookDirection = direction;
            }

            if (GameController.instance.IsActivate(GameController.ActionType.Dash)
                && playerState == PlayerState.Walk && Input.GetAxis("Jump") > 0 && dashCooldown == 0f)
            {
                playerState = PlayerState.Dash;

                dashDirection = direction;
                dashTimer = dashDuration;
            }
            else if (GameController.instance.IsActivate(GameController.ActionType.Grappling)
                && (playerState == PlayerState.Walk || playerState == PlayerState.Idle)
                && Input.GetAxis("Fire2") != 0 && isGrapKeyDown == false)
            {
                isGrapKeyDown = true;
                var hit = Physics2D.Raycast(origin: transform.position, direction: lookDirection, distance: grappleDistance);
                hook = Instantiate(hookPref);
                if (hit.collider != null)
                {
                    DrawGrappleLine(transform.position, hit.point, Color.red);
                    hook.transform.position = hit.point;
                    grappleHitPoint = hit.point;
                    playerState = PlayerState.Grap;
                }
                else
                {
                    var endPoint = transform.position + lookDirection * grappleDistance;
                    DrawGrappleLine(transform.position, endPoint, Color.red);
                    hook.transform.position = endPoint;
                    DestroyGrapple();
                }

                if (hook != null)
                {
                    Vector3 vectorToTarget = transform.position - hook.transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    hook.transform.rotation = q;
                    //hook.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
                    //hook.transform.eulerAngles = toPlayer;
                }
            }

            if (dashCooldown > 0)
            {
                dashCooldown -= Time.deltaTime;
                
                if (dashCooldown <= 0)
                {
                    dashCooldown = 0f;
                }
            }

            if (Input.GetAxis("Fire2") == 0)
            {
                isGrapKeyDown = false;
            }
        }
    }

    private void Shoot()
    {
        if (!GameController.instance.IsActivate(GameController.ActionType.Shoot)) return;

        if (shootCooldownTimer > 0)
        {
            shootCooldownTimer -= Time.deltaTime;

            if (shootCooldownTimer <= 0f)
            {
                shootCooldownTimer = 0f;
            }
        }
        else if (Input.GetAxis("Fire1") != 0)
        {
            // Fire
            Transform projTransform = Instantiate(projectilePrefab, projectilesParent).transform;
            PlayerProjectile proj = projTransform.GetComponent<PlayerProjectile>();
            proj.direction = lookDirection;
            projTransform.position = transform.position;

            shootCooldownTimer = shootCooldownDuration;
        }
    }

    private void DestroyGrapple(float duration = 0.2f)
    {
        grappleHitPoint = null;
        Destroy(myLine, duration);
        myLine = null;
        Destroy(hook, duration);
    }

    private void DrawGrappleLine(Vector3 start, Vector3 end, Color color)
    {
        start.z = 2;
        end.z = 2;

        myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = lineMat;
        lr.startColor = color;
        lr.endColor = color;
        lr.SetWidth(0.5f, 0.5f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingOrder = 2;
    }
}
