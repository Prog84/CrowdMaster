using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Shuriken : MonoBehaviour
{
    private Vector3 _moveDirection;
    private float _speed;
    private PlayerStateMachine _player;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Vector3 moveDirection, float speed)
    {
        _moveDirection = moveDirection;
        _speed = speed;

         _player = FindObjectOfType<PlayerStateMachine>();
        transform.position = _player.transform.position;

    }

    private void Update()
    {
        transform.Translate(_moveDirection * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            if (damageable.ApplyDamage(_rigidbody, _speed) == false)
            {
                return;
            }
        }
        Destroy(gameObject);
    }
}
