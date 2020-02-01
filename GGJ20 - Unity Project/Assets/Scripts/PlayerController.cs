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

    [Header("Shoot")]
    public GameObject projectilePrefab;
    public Transform projectilesParent;

    public float shootCooldownDuration = 1f;
    public float shootCooldownTimer = 0f;

    GameController gameController = null;

    private void Start()
    {
        gameController = GameController.instance;
    }

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        if (!canMove) return;

        if (playerState == PlayerState.Dash)
        {
            dashTimer -= Time.deltaTime;

            transform.position += dashDirection * Time.deltaTime * dashSpeed;

            if (dashTimer <= 0.0f)
            {
                dashTimer = 0.0f;
                playerState = PlayerState.Idle;
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

            // Dash
            if (playerState == PlayerState.Walk && Input.GetAxis("Jump") > 0)
            {
                playerState = PlayerState.Dash;

                dashDirection = direction;
                dashTimer = dashDuration;
            }
        }
    }

    private void Shoot()
    {
        //if (!GameController.instance.IsActivate(GameController.ActionType.Shoot)) return;

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
}
