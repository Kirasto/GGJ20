using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMove : MonoBehaviour
{

    private Rigidbody rb;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    private float Cooldown;

    public GameObject dashEffect;

    void Start()
    {
        Cooldown = 1;
        rb = GetComponent<Rigidbody>();
        dashTime = startDashTime;
    }

    void Update()
    {
        Cooldown -= Time.deltaTime;

        if (direction == 0 && (Input.GetKey(KeyCode.LeftShift)))
        {
            if (Input.GetKey(KeyCode.Q) && !(Input.GetKey(KeyCode.Z)) && !(Input.GetKey(KeyCode.S)) && (Cooldown <= 0))
            {
                gameObject.tag = "PlayerInvisible";
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.Z)) && (Cooldown <= 0))
            {
                gameObject.tag = "PlayerInvisible";
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                direction = 2;
            }
            else if (Input.GetKey(KeyCode.Z) && !(Input.GetKey(KeyCode.D)) && !(Input.GetKey(KeyCode.Q)) && (Cooldown <= 0))
            {
                gameObject.tag = "PlayerInvisible";
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                direction = 3;
            }
            else if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.Q)) && !(Input.GetKey(KeyCode.D)) && (Cooldown <= 0))
            {
                gameObject.tag = "PlayerInvisible";
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                direction = 4;
            }
            else if (Input.GetKey(KeyCode.Q) && (Input.GetKey(KeyCode.Z)) && (Cooldown <= 0))
            {
                gameObject.tag = "PlayerInvisible";
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                direction = 5;
            }
            else if (Input.GetKey(KeyCode.Z) && (Input.GetKey(KeyCode.D)) && (Cooldown <= 0))
            {
                gameObject.tag = "PlayerInvisible";
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                direction = 6;
            }
            else if (Input.GetKey(KeyCode.D) && (Input.GetKey(KeyCode.S)) && (Cooldown <= 0))
            {
                gameObject.tag = "PlayerInvisible";
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                direction = 7;
            }
            else if (Input.GetKey(KeyCode.S) && (Input.GetKey(KeyCode.Q)) && (Cooldown <= 0))
            {
                gameObject.tag = "PlayerInvisible";
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                direction = 8;
            }

        } else
        {
            if(dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            } else
            {
                dashTime -= Time.deltaTime;

                if(direction == 1)
                {
                    rb.velocity = Vector3.left * dashSpeed;
                    Cooldown = 1;
                    gameObject.tag = "Player";
                } else if(direction == 2)
                {
                    rb.velocity = Vector3.right * dashSpeed;
                    Cooldown = 1;
                    gameObject.tag = "Player";
                } else if(direction == 3)
                {
                    rb.velocity = Vector3.forward * dashSpeed;
                    Cooldown = 1;
                    gameObject.tag = "Player";
                } else if(direction == 4)
                {
                    rb.velocity = Vector3.back * dashSpeed;
                    Cooldown = 1;
                    gameObject.tag = "Player";
                } else if (direction == 5)
                {
                    rb.velocity = Vector3.left * dashSpeed + Vector3.forward * dashSpeed;
                    Cooldown = 1;
                    gameObject.tag = "Player";
                } else if (direction == 6)
                {
                    rb.velocity = Vector3.forward * dashSpeed + Vector3.right * dashSpeed;
                    Cooldown = 1;
                    gameObject.tag = "Player";
                } else if (direction == 7)
                {
                    rb.velocity = Vector3.right * dashSpeed + Vector3.back * dashSpeed;
                    Cooldown = 1;
                    gameObject.tag = "Player";
                } else if (direction == 8)
                {
                    rb.velocity = Vector3.back * dashSpeed + Vector3.left * dashSpeed;
                    Cooldown = 1;
                    gameObject.tag = "Player";
                }
            }
        }
    }


}
