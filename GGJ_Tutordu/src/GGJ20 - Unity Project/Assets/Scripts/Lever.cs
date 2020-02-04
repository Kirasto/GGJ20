using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite disactivate;
    public Sprite activate;

    bool hasBeenActivate = false;

    public GameObject onActivation;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = disactivate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenActivate == false && collision.gameObject.tag == "Player")
        {
            spriteRenderer.sprite = activate;
            onActivation.SetActive(false);

            hasBeenActivate = true;
        }
    }
}
