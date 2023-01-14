using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]

public class TownHall : Target
{
    private string _loss = "Проигрыш";

    public event UnityAction<float, float> ChangeHealPoints;
    public event UnityAction<string> EndGame;

    private void Update()
    {
        if(_healPoints <= 0)
        {
            EndGame?.Invoke(_loss);
        }
    }

    public override void TakeDamage(float damage)
    {
        _healPoints -= damage;
        ChangeHealPoints?.Invoke(_healPoints, MaxHealPoints);
    }
}
