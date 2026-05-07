using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Explosion")]
    [SerializeField] private float delay = 2f;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private GameObject explosionEffect;

    private bool exploded;

    void Start()
    {
        Invoke(nameof(Explode), delay);
    }

    void Explode()
    {
        if (exploded) return;
        exploded = true;

        if (explosionEffect != null)
        {
            GameObject effect = Instantiate(
                explosionEffect,
                transform.position,
                Quaternion.identity
            );

            Destroy(effect, 3f);
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out IShootable shootable))
            {
                Vector3 direction = (hit.transform.position - transform.position).normalized;
                shootable.Hitted(damage, transform.position, direction);
            }
        }

        Destroy(gameObject);
    }
}