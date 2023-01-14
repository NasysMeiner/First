using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveBar : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawner;
    [SerializeField] private Image _image;

    private int _maxEnemy;

    public void ChangeValue()
    {
        if (_maxEnemy != 0)
            _image.fillAmount = (float)CalculeitNumberEnemy() / _maxEnemy;
    }
    public void CalculeitMaxNumberEnemy()
    {
        _maxEnemy = CalculeitNumberEnemy();
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
}
