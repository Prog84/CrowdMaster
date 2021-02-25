using UnityEngine;

public abstract class EnemyTransition : Transition
{
    [SerializeField] private EnemyState _targetState;

    public EnemyState TargetState => _targetState;
    protected PlayerStateMachine Player { get; private set; }

    public void Init(PlayerStateMachine player)
    {
        Player = player;
    }

    protected virtual void OnEnable()
    {
        NeedTransit = false;   
    }
}
