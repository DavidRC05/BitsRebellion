using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    [SerializeField]
    float health = 100.0F;
    

    private void Awake()
    {
        healthBar.value = health;
    }

    // Función para recibir daño
    public void TakeDamage(float damage)
    {
        health -= Mathf.Abs(damage);
        if(health <= 0.0F)
        {
            Destroy(gameObject);
            return;
        }
        healthBar.value = health / 100.0F;
    }
}

