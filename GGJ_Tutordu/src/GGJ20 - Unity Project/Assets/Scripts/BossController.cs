using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public bool triggerdone = false;
    public bool dead = false;
    public bool isEnd = false;

    private void Update()
    {
        if (isEnd && Input.GetAxis("Cancel") != 0)
        {
            Application.Quit();
            Debug.Log("Has Quit");
            isEnd = false;
        }
        
        if (dead)
        {
            transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime / 2;

            if (transform.localScale.x <= 0)
            {
                transform.localScale = Vector3.zero;
                triggerdone = false;
                dead = false;

                GameObject.FindGameObjectWithTag("HUD").transform.Find("WIN").gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canMove = false;
                isEnd = true;
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
