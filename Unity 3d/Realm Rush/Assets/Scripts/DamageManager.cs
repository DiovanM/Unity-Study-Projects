using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{

    [SerializeField] private float lifePoints = 50f;

    public void TakeDamage(float damage)
    {
        lifePoints -= damage;
        if (lifePoints <= 0)
        {
            Die();
        }
    }  

    public void Die()
    {
        FindObjectOfType<GameManager>().IncreaseScore();
        FindObjectOfType<GameManager>().IncreaseDestroyedEnemies();
        Destroy(gameObject);
    }

}
