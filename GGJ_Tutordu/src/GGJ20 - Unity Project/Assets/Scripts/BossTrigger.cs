using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController.instance.actionInfos[(int)GameController.ActionType.Hp].isActivate && collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>().triggerdone = true;
        }
    }
}
