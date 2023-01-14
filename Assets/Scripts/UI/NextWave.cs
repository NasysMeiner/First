using UnityEngine;
using UnityEngine.Events;

public class NextWave : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Spawners _spawners;

    private bool _isPressedButton = false;

    public event UnityAction ChangePressButton;
    public event UnityAction ChangeTimeNextWave;

    private void OnEnable()
    {
        ChangePressButton += OnChangePressButton;
        _spawners.NextWave += OnNextWave;
    }

    private void OnDisable()
    {
        ChangePressButton -= OnChangePressButton;
        _spawners.NextWave -= OnNextWave;
    }

    public void RunEvent()
    {
        ChangePressButton?.Invoke();
    }

    public void ClickButton()
    {
        _gameObject.SetActive(false);
        _isPressedButton = true;
        ChangeTimeNextWave?.Invoke();
    }

    private void OnChangePressButton()
    {
        if(_isPressedButton == false)
            _gameObject.SetActive(true);
    }

    private void OnNextWave()
    {
        _isPressedButton = false;
    }
}
