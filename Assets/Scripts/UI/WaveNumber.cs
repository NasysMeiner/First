using TMPro;
using UnityEngine;

public class WaveNumber : MonoBehaviour
{
    [SerializeField] private Spawners _spawners;
    [SerializeField] private TMP_Text _text;

    private int _wave = 1;

    private void OnEnable()
    {
        _spawners.NextWave += PlusWave;
    }

    private void OnDisable()
    {
        _spawners.NextWave -= PlusWave;
    }

    private void PlusWave()
    {
        _wave++;
        _text.text = _wave.ToString();
    }
}
