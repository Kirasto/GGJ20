using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GrabbableObject : MonoBehaviour
{
    public GameController.ActionType type;

    public Sprite close;
    public Sprite open;

    bool hasBeenGet = false;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = close;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenGet == false && collision.gameObject.tag == "Player")
        {
            spriteRenderer.sprite = open;
            GameController.instance.SetUnlock(type);
            if (type == GameController.ActionType.Hp
                || type == GameController.ActionType.Move
                || type == GameController.ActionType.SlowDown
                || type == GameController.ActionType.Jump)
            {
                GameController.instance.SetActivate(type);
            }

            hasBeenGet = true;
        }
    }
}
