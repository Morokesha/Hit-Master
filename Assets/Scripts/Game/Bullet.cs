using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Rigidbody _rigidbody;
    private float _delayToDeactivate;
    private float _maxDelayToDeactivate = 2f;

    private void Awake()
    {
        _delayToDeactivate = _maxDelayToDeactivate;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _delayToDeactivate -= Time.deltaTime;

        if (_delayToDeactivate <= 0)
        {
            ReturnToDefaultPosition();
            _delayToDeactivate = _maxDelayToDeactivate;
        }
    }

    private void ReturnToDefaultPosition()
    {
        _rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void ShotOnTarget(Vector3 targetPos)
    {
        Vector3 shotDiretion = (targetPos - transform.position).normalized;
        _rigidbody.velocity = _speed * shotDiretion;
    }
}
