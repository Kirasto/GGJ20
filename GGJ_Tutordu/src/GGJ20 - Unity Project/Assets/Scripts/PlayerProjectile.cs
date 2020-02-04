using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 3f;
    public float lifeSpanDuration = 4f;
    public float damage = 20f;

    private float lifeSpan = 0f;

    public Vector3 direction;

    private void Start()
    {
        lifeSpan = lifeSpanDuration;
    }

    private void Update()
    {
        transform.position += direction * Time.deltaTime * speed;

        lifeSpan -= Time.deltaTime;

        if (lifeSpan <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ApplyDamage(collision.gameObject);
        }

        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            ApplyDamage(collision.gameObject);
        }

        if (collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }

    private void ApplyDamage(GameObject enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();

        enemyController.TakeDamage(damage);
    }
}
