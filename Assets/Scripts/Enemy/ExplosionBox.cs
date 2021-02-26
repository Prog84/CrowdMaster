using UnityEngine;

public class ExplosionBox : MonoBehaviour, IDamageable
{
    private float _explosionForce = 50f;

    public bool ApplyDamage(Rigidbody attachedBody, float force)
    {
        Vector3 direction = transform.position - attachedBody.position;
        direction.y = 0;
        attachedBody.AddForce(direction.normalized * force * _explosionForce, ForceMode.Impulse);
        return true;
    }
}
