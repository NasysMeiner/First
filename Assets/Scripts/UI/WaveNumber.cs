using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveNumber : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _text;

    private bool _isPlusWave = true;
    private int _wave = 1;

    private void OnEnable()
    {
        _spawner.SelectWave += PlusWave;
    }

    private void OnDisable()
    {
        _spawner.SelectWave -= PlusWave;
    }

    private void Update()
    {
        if(_isPlusWave && _spawner.IsWaveChanged == false)
        {
            _text.text = _wave.ToString();
            _isPlusWave = false;
        }
    }

    private void PlusWave()
    {
        _isPlusWave = true;
        _wave++;
    }
}
