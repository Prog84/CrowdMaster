using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Shuriken Abiliity", menuName = "Player/Abilities/Shuriken", order = 52)]
public class ShurikenAbility : Ability
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _usefulTime;
    [SerializeField] private Shuriken _shuriken;
    
    private AttackState _state;
    private Coroutine _coroutine;

    public override event UnityAction AbilityEnded;

    public override void UseAbility(AttackState attack)
    {
        if (_coroutine != null)
        {
            Reset();
        }
        _state = attack;
        _coroutine = _state.StartCoroutine(Attack(_state));
    }

    private IEnumerator Attack(AttackState state)
    {
        var spawned = Instantiate(_shuriken);
        spawned.Init(state.Rigidbody.velocity, _attackForce);
        yield return new WaitForSeconds(_usefulTime);
        Reset();
        AbilityEnded?.Invoke();
    }

    private void Reset()
    {
        _state.StopCoroutine(_coroutine);
        _coroutine = null;
    }
}
