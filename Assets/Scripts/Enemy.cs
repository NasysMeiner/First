using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private protected int _speed;
    [SerializeField] private protected float _timeBetweenAttacks = 1.5f;
    [SerializeField] private protected float _damage;
    [SerializeField] private protected float _healpoints;
    [SerializeField] private protected float _maxDistansAttack;
    [SerializeField] private protected int _reward;
    [SerializeField] private protected AudioSource _audioShot;

    private protected float _time;
    private protected Gates _gates;
    private protected List<Vector3> _points = new List<Vector3>();
    private protected List<Target> _objects = new List<Target>();
    private protected Vector3 _target;
    private protected Vector3 _direction;
    private protected int _currentAmount;
    private protected int intermediate;
    private protected Transform _trash;

    public event UnityAction<Enemy> Dying;

    public Vector3 Direction { get { return _direction; } private set { } }

    public float Speed { get { return _speed; } private set { } }

    public int Reward { get { return _reward; } private set { } }

    public float Damage { get { return _damage; } private set { } }

    public float HealPoints { get { return _healpoints; } private set { } }

    private void OnDisable()
    {
        if(_points.Count >= 3)
            _gates.GatesChanged -= OnDiedGates;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent(out BulletArcherTower bullet))
        {
            _healpoints -= bullet.Damage;
        }
    }

    private void Update()
    {
        Move();
        ApplyDamage();

        if (_healpoints <= 0)
        {
            Dying?.Invoke(this);
            Destroy(gameObject);
        }
    }

    public abstract void ApplyDamage();

    public void Instantiet(TownHall townHall, Path[] path, Gates gates, AudioSource audio, Transform trash)
    {
        _audioShot = audio;
        _objects.Add(townHall);
        CreatePath(townHall);

        if (gates != null)
        {
            _gates = gates;
            _objects.Add(gates);
            CreatePath(gates);
            _gates.GatesChanged += OnDiedGates;
        }

        foreach(Path pathNew in path)
        {
            _objects.Add(pathNew);
            CreatePath(pathNew);
        }

        _currentAmount = _points.Count - 1;
        _target = _points[_currentAmount];
        intermediate = _speed;
        _trash = trash;
    }

    public void UpgrateStats(float hp, float damage)
    {
        _healpoints = hp;
        _damage = damage;
    }

    private void OnDiedGates()
    {
        _currentAmount--;
        _target = _points[_currentAmount];
        _speed = intermediate;
        _points.Remove(_points[1]);
        _objects.Remove(_objects[1]);
    }

    private void CreatePath(Target target)
    {
        if (target != null)
        {
            Vector3 path = new Vector3(GeneratePoint(target.transform), target.transform.position.y, target.transform.position.z);
            _points.Add(path);
        }           
    }

    private float GeneratePoint(Transform gameObject)
    {
        return Random.Range(gameObject.position.x - 0.5f, gameObject.position.x + 0.5f);
    }

    private protected void Move()
    {
        if(MakeCheck(_points[_currentAmount], _objects[_currentAmount], _maxDistansAttack) == false)
        {
            _direction = (_target - transform.position).normalized;
            _direction.y = 0;
            transform.position += _direction * _speed * Time.deltaTime;
            ChangeRotation();
        }
        else
        {
            if(_currentAmount > 0 && _speed > 0)
            {
                _currentAmount--;
                _target = _points[_currentAmount];
            }
        }
    }

    private bool MakeCheck(Vector3 target, Target structure,float maxDistans)
    {
        if (target.z - transform.position.z <= maxDistans)
        {
            if(structure.IsTarget != true)
            {
                return true;
            }

            _speed = 0;

            return true;
        }

        return false;
    }

    private void ChangeRotation()
    {
        Quaternion target = Quaternion.LookRotation(new Vector3(_direction.x, 0, _direction.z));
        Debug.Log(target);
        transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * _speed);
    }
}
