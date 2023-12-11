using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float damage = 10.0f;

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.CompareTag("Player"))
        {
            Vector2 contactPoint = other.GetContact(0).normal;

            if (contactPoint.y < -0.9F)
            {
                Character2DController.Instance.Rebound();
                Destroy(gameObject);
            }
            HealthController controller = other.collider.GetComponent<HealthController>();
            if (controller != null)
            {
                controller.TakeDamage(damage, other.GetContact(0).normal);
            }
        }
        else if (other.gameObject.CompareTag("Plasma"))
        {
            Destroy(gameObject); // Destruye el enemigo si es golpeado por una "bala" de plasma
        }
    }
}
