using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWave : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners = new List<Spawner>();
    [SerializeField] private int _minEnemy = 2;
    [SerializeField] private GameObject _gameObject;

    private int _allEnenmy = 0;
    private bool _isChanged;
    private bool _isPressed = false;

    private void OnEnable()
    {
        foreach(Spawner spawner in _spawners)
        {
            spawner.SelectWave += ChangePressButton;
        }
    }

    private void OnDisable()
    {
        foreach (Spawner spawner in _spawners)
        {
            spawner.SelectWave -= ChangePressButton;
        }
    }

    private void Update()
    {
        CalculeitNumberEnemy();

        if((_allEnenmy <= _minEnemy || _isChanged) && _isPressed == false)
            _gameObject.SetActive(true);
        else
            _gameObject.SetActive(false);
    }

    public void ChangeActive()
    {
        _gameObject.SetActive(false);
        _isPressed = true;
    }

    private void ChangePressButton()
    {
        _isPressed = false;
    }

    private void CalculeitNumberEnemy()
    {
        _allEnenmy = 0;

        foreach(Spawner spawner in _spawners)
        {
            _allEnenmy += spawner.GetAllEnemy();
            _isChanged = spawner.IsWaveChanged;
        }
    }
}
