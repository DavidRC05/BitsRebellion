using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField]
    float damage = 10;
    float damageCooldown = 3f;
    float lastDamageTime;

    void Start()
    {
        lastDamageTime = -damageCooldown;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time - lastDamageTime > damageCooldown)
        {
            if (collision.collider.CompareTag("Player"))
            {
                /*
                HealthBarController controller = collision.collider.GetComponent<HealthBarController>();
                
                if (controller != null)
                {
                    controller.TakeDamage(damage);

                    lastDamageTime = Time.time;
                }
                */
                HealthController controller = collision.collider.GetComponent<HealthController>();

                if (controller != null)
                {
                    //prueba arthuro
                    controller.TakeDamage(damage, Vector2.zero);

                    lastDamageTime = Time.time;
                }
            }
        }
    }
}