using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _money = 0;
    private int _allMoney;

    public int Money { get { return _money; } private set { } }

    public int AllMoney { get { return _allMoney; } private set { } }

    private void Start()
    {
        _text.text = _money.ToString();
        _allMoney = _money;
    }

    public void ChangeMoney(int money)
    {
        _money += money;

        if(money > 0)
            _allMoney += money;

        _text.text = _money.ToString();
    }
}
