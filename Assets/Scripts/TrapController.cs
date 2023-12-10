using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
  
    [SerializeField] 
    float damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que colisionó es el jugador
        if (other.CompareTag("Player"))
        {
            // Obtener el componente de salud del jugador
            HealthBarController controller = other.GetComponent<HealthBarController>();

            // Verificar si el jugador tiene un componente de salud
            if (controller != null)
            {
                // Causar daño al jugador
                controller.TakeDamage(damage);
            }
        }
    }
}
