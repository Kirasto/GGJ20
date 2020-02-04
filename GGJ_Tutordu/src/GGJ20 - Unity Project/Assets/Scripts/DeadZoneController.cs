using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController playerController = collision.transform.GetComponent<PlayerController>();

            if (playerController.playerState != PlayerController.PlayerState.Dash
                && playerController.playerState != PlayerController.PlayerState.Grap)
            {
                GameController.Respawn();
            }
        }
        else if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<BossController>().dead = true;
        }
    }
}
