using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool triggerdone = false;
    public bool dead = false;

    private void Update()
    {
        if (dead)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime / 2;

            if (transform.localScale.x <= 0)
            {
                Application.Quit();
                transform.localScale = Vector3.zero;
                triggerdone = false;
                dead = false;
            }
        }
        else if (triggerdone)
        {
            Vector3 pos = transform.position;
            pos.y -= Time.deltaTime;
            transform.position = pos;
        }
    }
}
