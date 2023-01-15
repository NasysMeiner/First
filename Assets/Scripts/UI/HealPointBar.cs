using UnityEngine;

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
