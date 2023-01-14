using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Target : MonoBehaviour
{
    [SerializeField] private protected float _healPoints;
    [SerializeField] private bool _isTarget = false;

    private float _maxHealPoints;

    public bool IsTarget { get { return _isTarget; } private set { } }

    public float MaxHealPoints { get { return _maxHealPoints; } private set { } }

    public float HealPoints { get { return _healPoints; } private set { } }

    private void Start()
    {
        _maxHealPoints = _healPoints;
    }

    public abstract void TakeDamage(float damage);

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out BulletWizard bullet))
        {
            _healPoints -= bullet.Damage;
        }
    }

    public void RepairHp(float hp)
    {
        _healPoints += hp;
    }

    public void UpgrateHp(float hp)
    {
        _maxHealPoints += hp;
        _healPoints += hp;
    }
}
