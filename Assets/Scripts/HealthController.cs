using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float health;

    [SerializeField]
    float maxHealth = 100.0F;

    [SerializeField]
    float lostControlTimeout = 1.25F;

    [SerializeField]
    Vector2 reboundVelocity;

    HealthBarController _healthBarController;
    Character2DController _characterController;

    Rigidbody2D _rb;

    void Start()
    {
        health = maxHealth;

        _healthBarController = GetComponent<HealthBarController>();
        _healthBarController.Initialize(health);

        _characterController = Character2DController.Instance;

        _rb = GetComponent<Rigidbody2D>();
    }


    IEnumerator LoseControl()
    {
        _characterController._canMove = false;
        yield return new WaitForSeconds(lostControlTimeout);
        _characterController._canMove = true;
    }


    private void Rebound(Vector2 contactPoint)
    {

        _rb.velocity = new Vector2
            (-reboundVelocity.x * contactPoint.x, reboundVelocity.y);

    }

    public void TakeDamage(float damage, Vector2 contactPoint)
    {
        Debug.Log("health controller.");
        health -= damage;
        if (health <= 0.0F)
        {
            Debug.Log("danio en health controller.");
            Destroy(gameObject);
            //SceneManager.LoadScene("Start");
            //return;
        }

        //Podemos meter una animacion de hit

        //_characterController.animator.SetTrigger("hit");
        _healthBarController.OnDamage.Invoke(damage);

        Rebound(contactPoint);
        StartCoroutine(LoseControl());
    }

    public void Heal(float value)
    {
        if (health + value >= 100)
        {
            value = 100 - health;
        }
        health += Mathf.Abs(value);
        _healthBarController.OnHeal.Invoke(value);
    }
}
