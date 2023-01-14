using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArcherTower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private Vector3 _enemy;

    public float Speed { get { return _speed; } private set { } }

    public float Damage { get { return _damage; } private set { } }

    private void Update()
    {
        transform.position += _enemy * _speed * Time.deltaTime;

        if (transform.position.y < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Enemy enemy))
        {
            Destroy(gameObject);
        }
    }

    public void Initilization(Vector3 enemy, Tower tower)
    {
        _enemy = enemy;
    }

    public void UpgradeDamage(float damage)
    {
        _damage = damage;
    }
}
