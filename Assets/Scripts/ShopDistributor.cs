using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDistributor : MonoBehaviour
{
    [SerializeField] private List<ArcherTower> _archers;
    [SerializeField] private TownHall _townHall;
    [SerializeField] private Gates _gates;
    [SerializeField] private Balance _balance;
    [SerializeField] private GatesHealPointBar _gatesHealBar;
    [SerializeField] private HealPointBar _townHallHealBar;

    private bool _isLiveGates = true;

    private void OnEnable()
    {
        _gates.GatesChanged += OnGatesChanged;
    }

    private void OnDisable()
    {
        _gates.GatesChanged -= OnGatesChanged;
    }

    public void Upgrade(int id, float value, int price, ButtonShop buttonShop)
    {
        switch (id)
        {
            case 1:
                foreach(ArcherTower archerTower in _archers)
                {
                    archerTower.UpgradeDamageBullet(value);
                }

                break;

            case 2:
                if (_archers[0].AttackSpeed >= 0.5)
                {
                    foreach (ArcherTower archerTower in _archers)
                    {
                        archerTower.UpgradeAttakeSpeed(value);
                    }
                }
                else
                {
                    _balance.ChangeMoney(price);
                    buttonShop.IsPressed();
                }

                break;

            case 3:
                _townHall.RepairHp(value);
                _townHallHealBar.OnChangeHealPoint(_townHall.HealPoints, _townHall.MaxHealPoints);

                break;

            case 4:
                _townHall.UpgrateHp(value);
                _townHallHealBar.OnChangeHealPoint(_townHall.HealPoints, _townHall.MaxHealPoints);

                break;

            case 5:
                if (_isLiveGates)
                {
                    _gates.RepairHp(value);
                    _gatesHealBar.OnChangeHealPoint(_gates.HealPoints, _gates.MaxHealPoints);
                }
                else
                {
                    _balance.ChangeMoney(price);
                    buttonShop.IsPressed();
                }

                break;

            case 6:
                if (_isLiveGates)
                {
                    _gates.UpgrateHp(value);
                    _gatesHealBar.OnChangeHealPoint(_gates.HealPoints, _gates.MaxHealPoints);
                }
                else
                {
                    _balance.ChangeMoney(price);
                    buttonShop.IsPressed();
                }

                break;
        }
    }

    private void OnGatesChanged()
    {
        _isLiveGates = false;
    }
}
