using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float life = 100f;
    public GameObject lifeBar = null;

    private void FixedUpdate()
    {
        if (lifeBar != null && lifeBar.activeSelf != true)
        {
            lifeBar.SetActive(true);
        }
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        if (life <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
