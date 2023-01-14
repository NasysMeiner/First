using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class VictoryMenu : MonoBehaviour
{
    [SerializeField] private List<ArcherTower> _towers;
    [SerializeField] private Balance _balance;
    [SerializeField] private Spawners _spawners;
    [SerializeField] private TownHall _hall;
    [SerializeField] private TMP_Text _textGameStatus;
    [SerializeField] private TMP_Text _textStatistics;

    private int _allEnemy = 0;

    private void OnEnable()
    {
        _spawners.EndGame += OnEndGame;
        _hall.EndGame += OnEndGame;
    }

    private void OnDisable()
    {
        _spawners.EndGame -= OnEndGame;
        _hall.EndGame -= OnEndGame;
    }

    private void OnEndGame(string statusGame)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);

        foreach (ArcherTower tower in _towers)
        {
            _allEnemy += tower.ShotsDone;
        }

        _textGameStatus.text = statusGame;
        _textStatistics.text = $"Врагов убито: {_spawners.AllEnemy}\nВыстрелов сделано: {_allEnemy}\nМонет заработано: {_balance.AllMoney}";
    }
}
