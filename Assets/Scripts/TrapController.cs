using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField]
    float damage = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto que colisionó es el jugador
        if (collision.collider.CompareTag("Player"))
        {
            // Obtener el componente de salud del jugador
            HealthBarController controller = collision.collider.GetComponent<HealthBarController>();

            // Verificar si el jugador tiene un componente de salud 
            if (controller != null)
            {
                // Causar daño al jugador
                controller.TakeDamage(damage);
            }
        }
    }
}