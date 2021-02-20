using UnityEngine;

public interface IDamageable
{
    bool ApplyDamage(Rigidbody rigidbody, float force);
}
