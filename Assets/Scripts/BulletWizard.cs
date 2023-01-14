using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWizard : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private Vector3 _positionTarget;
    private bool _isTouch = false;
    private float _time;
    private Transform _trash;

    public float Damage { get { return _damage; } private set { } }

    private void Update()
    {
        transform.position += _positionTarget * _speed * Time.deltaTime;

        if (transform.position.y > 10)
            Destroy(gameObject);

        if (_isTouch)
            _time += Time.deltaTime;

        if(_time >= 1)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Target target))
        {
            TouchBullet();
        }
    }

    public void Inst(Vector3 positionTarget, Transform trash)
    {
        _positionTarget = positionTarget;
        _trash = trash;
    }

    private void TouchBullet()
    {
        _isTouch = true;
        gameObject.transform.position = _trash.position;
    }
}
