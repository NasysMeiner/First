using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesHealPointBar : Bar
{
    [SerializeField] private Gates _gates;

    private void OnEnable()
    {
        _gates.ChangeHealPoints += OnChangeHealPoint;
        _gates.GatesChanged += OnGatesChanged;
    }

    private void OnDisable()
    {
        _gates.ChangeHealPoints -= OnChangeHealPoint;
        _gates.GatesChanged -= OnGatesChanged;
    }

    private void OnGatesChanged()
    {
        _image.fillAmount = 0;
    }
}
