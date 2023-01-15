using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _wizardAfter;
    [SerializeField] private int _numbersWizard;
    [SerializeField] private float _timeRespawn;
    [SerializeField] private float _timeWaitingNextWave;
    [SerializeField] private float _timeSubwave = 2;
    [SerializeField] private TownHall _townHall;
    [SerializeField] private Gates _gates;
    [SerializeField] private Spawners _spawners;
    [SerializeField] private Path[] _path;
    [SerializeField] private Enemy[] _prefab;
    [SerializeField] private float[] _upgrateStatsWarrior;
    [SerializeField] private float[] _upgrateStatsWizard;
    [SerializeField] private Balance _balance;
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private AudioSource _audioShotWarrior;
    [SerializeField] private AudioSource _audioShotWizard;
    [SerializeField] private Transform _trash;
    [SerializeField] private NextWave _nextWaveButton;
    [SerializeField] private WaveBar _waveBar;

    private float[] _currentStatsWarrior = new float[2];
    private float[] _currentStatsWizard = new float[2];
    private int _waveNumbers = 1;
    private int _respawnedEnemy = 0;
    private int _respawnedEnemyWizard = 0;
    private int _totalDyingNumberEnemy;
    private int _totalEnemySpawned = 0;
    private int _waveNumberEnemy = 0;
    private float _currentTimeWaitingNextWave;
    private Wave _currentWave;
    private bool _isWaitNextWave;
    private int _minEnemyToSkipTimeWave = 2;

    public event UnityAction ExitWave;

    public int TotalDyingNumberEnemy { get { return _totalDyingNumberEnemy; } private set {  } }

    public int AllEnemy { get { return _totalEnemySpawned; } private set { } }

    private void OnEnable()
    {
        if(_gates != null)
            _gates.GatesChanged += OnDiedGates;

        _currentWave = _waves[0];
        _spawners.NextWave += SelectNextWave;
        _nextWaveButton.ChangeTimeNextWave += OnChangeTimeNextWave;
    }

    private void OnDisable()
    {
        if (_gates != null)
            _gates.GatesChanged -= OnDiedGates;

        _spawners.NextWave -= SelectNextWave;
        _nextWaveButton.ChangeTimeNextWave -= OnChangeTimeNextWave;
    }

    private void Start()
    {        
        Time.timeScale = 0;
        TotalDyingNumberEnemy = 0;
        _currentTimeWaitingNextWave = _timeWaitingNextWave;
        _currentStatsWarrior[0] = _prefab[0].HealPoints;
        _currentStatsWarrior[1] = _prefab[0].Damage;
        _currentStatsWizard[0] = _prefab[1].HealPoints;
        _currentStatsWizard[1] = _prefab[1].Damage;
        CountEnemyAllWaves();
        _spawners.CountTotalNumbersEnemy(AllEnemy);
        _waveBar.CalculeitMaxNumberEnemy();
        StartCoroutine(Spawn());
    }

    public int GetAllEnemy()
    {
        return _currentWave.NumberOfEnemy - _waveNumberEnemy;
    }

    private void OnDiedGates()
    {
        _gates = null;
    }

    private IEnumerator Spawn()
    {
        if (_waveNumbers == 3)
        {
            UpgrateStatsEnemy();
            _waveNumbers = 1;
        }

        _nextWaveButton.RunEvent();
        _waveBar.CalculeitMaxNumberEnemy();
        _isWaitNextWave = true;

        yield return new WaitForSeconds(_currentTimeWaitingNextWave);

        _isWaitNextWave = false;
        _currentTimeWaitingNextWave = _timeWaitingNextWave;

        for (int i = 0; i < _currentWave.NumberOfEnemy; i++ )
        {
            if(_waveNumberEnemy == _currentWave.NumberOfEnemy - _minEnemyToSkipTimeWave)
            {
                _nextWaveButton.RunEvent();
            }

            if (_respawnedEnemy >= _wizardAfter)
            {
                if (_numbersWizard == _respawnedEnemyWizard)
                    _respawnedEnemy = -1;

                EnemySpawn(_prefab[1], _audioShotWizard, _currentStatsWizard);
                _respawnedEnemyWizard++;
                _waveNumberEnemy++;
            }
            else
            {
                EnemySpawn(_prefab[0], _audioShotWarrior, _currentStatsWarrior);
                _respawnedEnemy++;
                _waveNumberEnemy++;
            }

            _waveBar.ChangeValue();

            if(_respawnedEnemyWizard == _numbersWizard)
            {
                _respawnedEnemy = 0;
                yield return new WaitForSeconds(_timeSubwave);
            }
            else
            {
                yield return new WaitForSeconds(_timeRespawn);
            }
        }

        ExitWave?.Invoke();
    }

    private void UpgrateStatsEnemy()
    {
        for (int i = 0; i < _currentStatsWarrior.Length; i++)
        {
            _currentStatsWarrior[i] += _upgrateStatsWarrior[i];
            _currentStatsWizard[i] += _upgrateStatsWizard[i];
        }
    }

    private void SelectNextWave()
    {
        _waves.Remove(_waves[0]);
        _waveNumberEnemy = 0;
        _respawnedEnemy = 0;
        _waveNumbers++;

        if (_waves.Count > 0)
        {
            _currentWave = _waves[0];
            StartCoroutine(Spawn());
        }    
    }

    private void EnemySpawn(Enemy prefab, AudioSource audio, float[] upgrateStats)
    {
        Vector3 positionEnemy = new Vector3(transform.position.x + Random.Range(-4, 4), transform.position.y, transform.position.z);
        Enemy enemy = Instantiate(prefab, positionEnemy, Quaternion.identity);
        enemy.UpgrateStats(upgrateStats[0], upgrateStats[1]);
        enemy.Initialize(_townHall, _path, _gates, audio, _trash);
        enemy.Dying += OnEnemyDying;
    }

    private void OnChangeTimeNextWave()
    {
        if (_isWaitNextWave)
            StopCoroutine(Spawn());
        else
            _currentTimeWaitingNextWave = 0;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        _totalDyingNumberEnemy++;
        enemy.Dying -= OnEnemyDying;
        _balance.ChangeMoney(enemy.Reward);
    }

    private void CountEnemyAllWaves()
    {
        foreach(Wave wave in _waves)
        {
            _totalEnemySpawned += wave.NumberOfEnemy;
        }
    }
}

[System.Serializable]
public class Wave
{
    public int NumberOfEnemy;
}
