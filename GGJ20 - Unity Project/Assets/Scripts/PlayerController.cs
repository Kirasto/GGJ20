using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        _Count
    };

    public PlayerState playerState;

    public float movementSpeed = 5f;
    public float jumpRange = 1f;
    public float jumpDuration = 1f;

    GameController gameController = null;

    private void Start()
    {
        gameController = GameController.instance;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        direction.Normalize();
        transform.position += direction * Time.deltaTime * movementSpeed;

        playerState = direction != Vector3.zero ? PlayerState.Idle : PlayerState.Walk;
    }
}
