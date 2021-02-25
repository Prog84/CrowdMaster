using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(HealthContainer))]
public class EnemyStateMachine : StateMachine, IDamageable
{
    [SerializeField] private EnemyState _firstState;
    [SerializeField] private BrokenState _brokenState;
    
    private EnemyState _currentState;
    private float _minDamage;

    public PlayerStateMachine Player { get; private set; }

    public event UnityAction<EnemyStateMachine> Died;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Health = GetComponent<HealthContainer>();
        Player = FindObjectOfType<PlayerStateMachine>();
    }

    private void OnEnable()
    {
        Health.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        Health.Died -= OnEnemyDied;
    }

    private void OnEnemyDied()
    {
        enabled = false;
        Rigidbody.constraints = RigidbodyConstraints.None;
        Died?.Invoke(this);
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(Rigidbody, Animator, Player);
    }

    private void Update()
    {
        if (_currentState == null)
        {
            return;
        }

        EnemyState nextState = _currentState.GetNextState();

        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.Enter(Rigidbody, Animator, Player);
        }
    }

    public bool ApplyDamage(Rigidbody rigidbody, float force)
    {
        if (force > _minDamage && _currentState != _brokenState)
        {
            Health.TakeDamage((int)force);
            Transit(_brokenState);
            _brokenState.ApplyDamage(rigidbody, force);
            return true;
        }

        return false;
    }
}
