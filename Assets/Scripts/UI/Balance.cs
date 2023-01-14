using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _currentMoney = 0;
    private int _earnedMoney;

    public int Money { get { return _currentMoney; } private set { } }

    public int AllMoney { get { return _earnedMoney; } private set { } }

    private void Start()
    {
        _text.text = _currentMoney.ToString();
        _earnedMoney = _currentMoney;
    }

    public void ChangeMoney(int money)
    {
        _currentMoney += money;

        if(money > 0)
            _earnedMoney += money;

        _text.text = _currentMoney.ToString();
    }
}
