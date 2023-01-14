using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealPointBar : Bar
{
    [SerializeField] private TownHall _townHall;

    private void OnEnable()
    {
        _townHall.ChangeHealPoints += OnChangeHealPoint;
    }

    private void OnDisable()
    {
        _townHall.ChangeHealPoints -= OnChangeHealPoint;
    }
}
