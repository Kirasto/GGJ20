using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float life = 100f;
    public GameObject lifeBar = null;

    [Header("Monster Move")]
    public bool isMoving = false;
    public bool canMoving = false;
    public float moveSpeed = 1f;
    public float reducedSpeed = 1f;
    public bool isVertical = true;
    private bool isInversed = true;

    [Header("Hp")]
    public bool hasHp = false;

    [Header("Jump")]
    public bool canJump = false;
    public float jumpDuration = 2f;
    public float jumpCooldown = 2f;
    public float jumpHeight = 1f;
    float jumpOffset = 0f;
    float jumpTimer;
    bool isCooldown = true;
    bool isJump = true;
    Vector3 defaultPos;

    public Collider2D collider;

    private void Awake()
    {
        defaultPos = transform.position;
        jumpTimer = 0.0f;
    }

    private void FixedUpdate()
    {
        if (lifeBar != null && lifeBar.activeSelf != GameController.instance.actionInfos[(int)GameController.ActionType.Hp].isActivate)
        {
            lifeBar.SetActive(GameController.instance.actionInfos[(int)GameController.ActionType.Hp].isActivate);
        }

        if (isMoving)
        {
            Vector3 direction = Vector3.zero;
            if (isVertical && isInversed) direction = Vector3.down;
            if (isVertical && !isInversed) direction = Vector3.up;
            if (!isVertical && isInversed) direction = Vector3.left;
            if (!isVertical && !isInversed) direction = Vector3.right;
            transform.position += direction * (GameController.instance.IsActivate(GameController.ActionType.SlowDown) ? reducedSpeed : moveSpeed) * Time.deltaTime;
        }

        if (canMoving && GameController.instance.actionInfos[(int)GameController.ActionType.Move].isActivate)
        {
            isMoving = true;
        }

        JumpUpdate();
    }

    private void JumpUpdate()
    {
        if (canJump == false) return;
        if (GameController.instance.IsActivate(GameController.ActionType.Jump))
        {
            if (isCooldown)
            {
                jumpTimer -= Time.deltaTime;

                if (jumpTimer <= 0f)
                {
                    isCooldown = false;
                    isJump = true;
                    collider.enabled = false;
                }
            }
            else
            {
                jumpOffset = jumpOffset + (isJump ? 1 : -1) * jumpHeight * Time.deltaTime / jumpDuration;

                if (jumpOffset <= 0 && isJump == false)
                {
                    jumpOffset = 0;
                    jumpTimer = jumpCooldown;
                    isCooldown = true;
                    collider.enabled = true;
                }
                else if (jumpOffset >= jumpHeight)
                {
                    isJump = false;
                    jumpOffset = jumpHeight;
                }

                transform.position = defaultPos + new Vector3(0, jumpOffset, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameController.Respawn();
        }

        isInversed = !isInversed;
    }

    public void TakeDamage(float damage)
    {
        if (hasHp == false || GameController.instance.actionInfos[(int)GameController.ActionType.Hp].isActivate) return;
        life -= damage;

        if (life <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
