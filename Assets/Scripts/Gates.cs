using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gates : Target
{
    public event UnityAction GatesChanged;
    public event UnityAction<float, float> ChangeHealPoints;

    private void Update()
    {
        if(_healPoints <= 0)
        {
            GatesChanged?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out BulletWizard bullet))
        {
            TakeDamage(bullet.Damage);
        }
    }

    public override void TakeDamage(float damage)
    {
        _healPoints -= damage;
        ChangeHealPoints?.Invoke(_healPoints, MaxHealPoints);
    }
}
