using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawners : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] private VictoryMenu _victoryMenu;

    private int _exitWave = 0;
    private int _totalNumbersEnemy = 0;
    private int _allDying = 0;
    private string _victory = "Победа";

    public event UnityAction NextWave;
    public event UnityAction<string> EndGame;

    public int AllEnemy { get { return _allDying; } private set { } }

    private void OnEnable()
    {
        foreach(Spawner spawner in _spawners)
        {
            spawner.ExitWave += OnExitWave;
        }
    }

    private void OnDisable()
    {
        foreach (Spawner spawner in _spawners)
        {
            spawner.ExitWave -= OnExitWave;
        }
    }

    private void Update()
    {
        _allDying = 0;

        if(_exitWave == transform.childCount)
        {
            _allDying = 0;
            NextWave?.Invoke();
            _exitWave = 0;
        }

        foreach (Spawner spawner in _spawners)
        {
            _allDying += spawner.TotalDyingNumberEnemy;
        }

        if (_allDying == _totalNumbersEnemy)
        {
            _victoryMenu.gameObject.SetActive(true);
            EndGame?.Invoke(_victory);
            _totalNumbersEnemy++;
        }
    }

    public void CountTotalNumbersEnemy(int allEnemy)
    {
        _totalNumbersEnemy += allEnemy;
    }

    private void OnExitWave()
    {
        _exitWave++;
    }
}
