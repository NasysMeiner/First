using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBar : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawner;
    [SerializeField] private Image _image;
    [SerializeField] private Spawners _spawners;

    private int _allEnemy;
    private int _maxEnemy;

    private void OnEnable()
    {
        foreach(Spawner spawner in _spawner)
        {
            spawner.SelectWave += CalculeitMaxNumberEnemy;
        }
    }

    private void OnDisable()
    {
        foreach (Spawner spawner in _spawner)
        {
            spawner.SelectWave -= CalculeitMaxNumberEnemy;
        }
    }

    private void Start()
    {
        CalculeitMaxNumberEnemy();
    }

    private void Update()
    {
        _allEnemy = CalculeitNumberEnemy();
        ChangeValue();
    }

    private int CalculeitNumberEnemy()
    {
         int allEnemy = 0;

        foreach (Spawner spawner in _spawner)
        {
            allEnemy += spawner.GetAllEnemy();
        }

        return allEnemy;
    }

    private void CalculeitMaxNumberEnemy()
    {
        _maxEnemy = CalculeitNumberEnemy();
    }

    private void ChangeValue()
    {
        if (_spawner[0].IsWaveChanged == false)
            _image.fillAmount = (float)_allEnemy / _maxEnemy;
    }
}
