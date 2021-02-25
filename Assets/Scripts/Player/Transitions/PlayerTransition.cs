using UnityEngine;

public abstract class PlayerTransition : Transition
{
    [SerializeField] private PlayerState _targetState;

    public PlayerState TargetState => _targetState;

    private void OnEnable()
    {
        NeedTransit = false;
        Enable();
    }

    public abstract void Enable();
}
