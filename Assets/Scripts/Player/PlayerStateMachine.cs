using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(HealthContainer))]
public class PlayerStateMachine : StateMachine
{
    [SerializeField] private PlayerState _firstState;

    private PlayerState _currentState;
    
    public UnityAction Damaged;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
        Health = GetComponent<HealthContainer>();
    }

    private void OnEnable()
    {
        Health.Died += OnDied;
    }

    private void OnDisable()
    {
        Health.Died -= OnDied;
    }

    private void OnDied()
    {
        enabled = false;
        Animator.SetTrigger("broken");
    }

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter(Rigidbody, Animator);
    }

    private void Update()
    {
        if (_currentState == null)
        {
            return;
        }

        PlayerState nextState = _currentState.GetNextState();

        if (nextState != null)
        {
            Transit(nextState);
        }
    }

    private void Transit(PlayerState nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }

        _currentState = nextState;

        if (_currentState != null)
        {
            _currentState.Enter(Rigidbody, Animator);
        }
    }

    public void ApplyDamage(float damage)
    {
        Damaged?.Invoke();
        Health.TakeDamage((int)damage);
    }
}
