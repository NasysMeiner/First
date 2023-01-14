using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private protected BulletArcherTower _bullet;
    [SerializeField] private protected float _timeToShot = 1;
    [SerializeField] private protected AudioSource _audioShot;

    private Queue<Enemy> _enemys = new Queue<Enemy>();
    private Enemy _currentEnemy;
    private float _time = 0;
    private float _damageBullet;

    public BulletArcherTower Bullet { get { return _bullet; } private set { } }

    public float AttackSpeed { get { return _timeToShot; } private set { } }

    private void Start()
    {
        _damageBullet = _bullet.Damage;
    }

    private void Update()
    {
        SelectEnemy();

        if (_currentEnemy != null && _time >= _timeToShot)
        {
            Shot(_damageBullet);
            _audioShot.Play();
            _time = 0;
        }

        _time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Enemy enemy))
            _enemys.Enqueue(enemy);
    }

    public abstract void Shot(float damageBullet);

    public Vector3 CalculetePosition(BulletArcherTower bullet)
    {
        float positionNotX = Mathf.Sqrt(Mathf.Pow(_currentEnemy.transform.position.z - transform.GetChild(1).position.z, 2) + Mathf.Pow(_currentEnemy.transform.position.y - transform.GetChild(1).position.y, 2));
        float timeToPoint = Mathf.Sqrt(Mathf.Pow(positionNotX, 2) + Mathf.Pow(_currentEnemy.transform.position.x - transform.GetChild(1).position.x, 2)) / bullet.Speed;
        Vector3 advancePosition = _currentEnemy.Speed * timeToPoint * _currentEnemy.Direction + _currentEnemy.transform.position;

        return (advancePosition - transform.GetChild(1).position).normalized;
    }

    public void UpgradeAttakeSpeed(float value)
    {
        _timeToShot -= value;
    }

    public void UpgradeDamageBullet(float damage)
    {
        _damageBullet += damage;
    }

    private void SelectEnemy()
    {
        if(_currentEnemy == null && _enemys.Count > 0)
            _currentEnemy = _enemys.Dequeue();
    }
}
