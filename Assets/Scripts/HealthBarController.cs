using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class HealthBarController : MonoBehaviour
{

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    float health = 100.0F;

    [SerializeField]
    public UnityEvent<float> OnDamage;

    [SerializeField]
    public UnityEvent<float> OnHeal;

    void Awake()
    {

        OnDamage.AddListener(DecreaseHealth);
        OnHeal.AddListener(IncreaseHealth);

    }
    public void Initialize(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void DecreaseHealth(float value)
    {
        healthBar.value -= Mathf.Abs(value);
    }

    public void IncreaseHealth(float value)
    {
        healthBar.value += Mathf.Abs(value);
    }

    // Función para recibir daño
    public void TakeDamage(float damage)
    {
        Debug.Log("healthbar.");
        health = Mathf.Abs(damage);
        if (health <= 0.0F)
        {
            Debug.Log("Danio a healthbar.");
            Destroy(gameObject);
            return;
        }
        healthBar.value = health / 100.0F;
    }

}