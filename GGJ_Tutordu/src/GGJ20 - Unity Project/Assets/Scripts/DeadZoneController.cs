using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneController : MonoBehaviour
{
    PlayerController player = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController playerController = collision.transform.GetComponent<PlayerController>();

            player = playerController;

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

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
        {
            player = null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (player != null
            && player.playerState != PlayerController.PlayerState.Dash
            && player.playerState != PlayerController.PlayerState.Grap)
        {
            GameController.Respawn();
        }
    }
}
