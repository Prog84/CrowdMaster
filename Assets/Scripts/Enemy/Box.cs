using UnityEngine;

public class Box : MonoBehaviour, IDamageable
{
    public bool ApplyDamage(Rigidbody rigidbody, float force)
    {
        Debug.Log("Я коробка");
        Vector3 direction = (transform.position - rigidbody.position);
        direction.y = 0;
        rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            var i = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            var i = 0;
        }
    }


}
