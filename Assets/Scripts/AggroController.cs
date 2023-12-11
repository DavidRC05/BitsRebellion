using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroController : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float speed;

    [SerializeField]
    float awakeDistance;

    [SerializeField]
    float stopDistance;

    [SerializeField]
    bool isFacingRight;

    float _distance;

    bool _isChasing;

    Vector2 _position;

    Animator _animator; //para cambiar los parametros de animacion

    void Start()
    {
        _position = transform.position;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _distance = Vector2.Distance(transform.position, player.position);
        if (_distance <= awakeDistance && !_isChasing)
        {
            _isChasing = true;
            //animator
            _animator.SetFloat("speed", 1.0F);
        }
        else if (_distance >= stopDistance && _isChasing)
        {
            _isChasing = false;
            //animator
            _animator.SetFloat("speed", 0.0F);
            //_animator.SetTrigger("shoot");
        }

        Vector2 lookAt = Vector2.zero;

        if(_isChasing)
        {
            Vector2 position = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);
            lookAt = position;
        }

        if (lookAt.x > transform.position.x)
        {
            if (!isFacingRight)
            {
                isFacingRight = true;
                transform.Rotate(0.0F, 180.0F, 0.0F);
            }     
        }
        else
        {
            if (isFacingRight)
            {
                isFacingRight = false;
                transform.Rotate(0.0F, 180.0F, 0.0F);
            }
        }
    }

}
