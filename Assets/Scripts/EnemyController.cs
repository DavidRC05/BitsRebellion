using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float damage = 10.0f;

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("searching...");

        if (other.collider.CompareTag("Player"))
        {
            Debug.Log("SE HIZO CONTACTO CON PLAYER");
            Vector2 contactPoint = other.GetContact(0).normal;

            if (contactPoint.y < -0.9F)
            {
                Character2DController.Instance.Rebound();
                Destroy(gameObject);
            }
            else
            {

            }

            Debug.Log("Llamado de TakeDamage");
            HealthController controller = other.collider.GetComponent<HealthController>();
            controller.TakeDamage(damage, other.GetContact(0).normal);

        }
    }
}


