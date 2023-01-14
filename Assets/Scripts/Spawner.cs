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
    [SerializeField] private int ID;
    [SerializeField] private AudioSource _audioShotWarrior;
    [SerializeField] private AudioSource _audioShotWizard;
    [SerializeField] private Transform _trash;

    private float[] _currentStatsWarrior = new float[2];
    private float[] _currentStatsWizard = new float[2];
    private int _nextWave = 0;
    private int _waveNumbers = 1;
    private int _respawnedEnemy = 0;
    private int _respawnedEnemyWizard = 0;
    private int _totalDyingNumberEnemy;
    private int _allEnemy = 0;
    private int _waveNumberEnemy;
    private float _currentTimeWaitingNextWave;
    private float _time;
    private bool _isWaveChanged = false;
    private bool _isWaveEnd = false;
    private Wave _currentWave;
    private bool _isEndGame;

    public event UnityAction ExitWave;
    public event UnityAction SelectWave;

    public bool IsWaveChanged { get { return _isWaveChanged; } private set { } }

    public int NumbersEnemy { get { return _currentWave.NumberOfEnemy; } private set { } }

    public int TotalDyingNumberEnemy { get { return _totalDyingNumberEnemy; } private set {  } }

    public bool IsEnaGame { get { return _isEndGame; } private set { } }

    public int AllEnemy { get { return _allEnemy; } private set { } }

    private void OnEnable()
    {
        if(_gates != null)
            _gates.GatesChanged += OnDiedGates;

        _spawners.NextWave += SelectNextWave;
        _currentWave = _waves[0];
    }

    private void OnDisable()
    {
        if (_gates != null)
            _gates.GatesChanged -= OnDiedGates;

        _spawners.NextWave -= SelectNextWave;
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
    }

    private void Update()
    {
        if(_waveNumbers == 3)
        {
            UpgrateStatsEnemy();
            _waveNumbers = 1;
        }

        Spawn();
        _time += Time.deltaTime;
    }

    public void NextWave()
    {
        _nextWave = 1;
    }

    public int GetAllEnemy()
    {
        return _currentWave.NumberOfEnemy - _waveNumberEnemy;
    }

    private void OnDiedGates()
    {
        _gates = null;
    }

    private void Spawn()
    {
        if (_time >= _currentTimeWaitingNextWave && _isWaveChanged)
            _isWaveChanged = false;

        if (_nextWave > 0)
        {
            _currentTimeWaitingNextWave = 0;
        }
        else
        {
            _currentTimeWaitingNextWave = _timeWaitingNextWave;
        }

        if (_currentWave.NumberOfEnemy != _waveNumberEnemy && _waves.Count > 0 && _isWaveChanged == false)
        {
            if (_respawnedEnemy == -1 && _time >= _timeSubwave)
            {
                _respawnedEnemy = 0;
                _time = 0;
            }

            if (_time >= _timeRespawn && _respawnedEnemy != -1 && (_numbersWizard <= _respawnedEnemyWizard || _respawnedEnemy != -1))
            {
                if (_respawnedEnemy == _wizardAfter)
                {
                    EnemySpawn(_prefab[1], _audioShotWizard, _currentStatsWizard);
                    _respawnedEnemyWizard++;

                    if(_numbersWizard == _respawnedEnemyWizard)
                        _respawnedEnemy = -1;

                    _time = 0;
                    _waveNumberEnemy++;
                }
                else
                {
                    EnemySpawn(_prefab[0], _audioShotWarrior, _currentStatsWarrior);
                    _respawnedEnemy++;
                    _time = 0;
                    _waveNumberEnemy++;
                }
            }
        }
        else
        {
            if (_waves.Count > 0 && _currentWave.NumberOfEnemy == _waveNumberEnemy && _isWaveEnd == false)
            {
                _isWaveEnd = true;
                ExitWave?.Invoke();
            }              
        }
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
        _nextWave = 0;
        _time = 0;
        _waveNumbers++;
        _isWaveChanged = true;
        _isWaveEnd = false;

        if (_waves.Count > 0)
        {
            _currentWave = _waves[0];
            SelectWave?.Invoke();
        }    
    }

    private void EnemySpawn(Enemy prefab, AudioSource audio, float[] upgrateStats)
    {
        Vector3 positionEnemy = new Vector3(transform.position.x + Random.Range(-4, 4), transform.position.y, transform.position.z);
        Enemy enemy = Instantiate(prefab, positionEnemy, Quaternion.identity);
        enemy.UpgrateStats(upgrateStats[0], upgrateStats[1]);
        enemy.Instantiet(_townHall, _path, _gates, audio, _trash);
        enemy.Dying += OnEnemyDying;
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
            _allEnemy += wave.NumberOfEnemy;
        }
    }
}

[System.Serializable]
public class Wave
{
    public int NumberOfEnemy;
}
