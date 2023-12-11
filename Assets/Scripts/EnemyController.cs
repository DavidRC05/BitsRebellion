using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float life = 3.0F;

    [SerializeField]
    float damage = 10.0f;

    [SerializeField]
    bool boss = false;

    //escenas
    int sceneCount;

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
            life -= 1;
            if (life <= 0)
            {
                Destroy(gameObject); // Destruye el enemigo si es golpeado por una "bala" de plasma
                if (boss)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }
    }
}
