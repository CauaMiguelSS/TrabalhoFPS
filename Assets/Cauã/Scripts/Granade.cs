using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Explosão")]
    [SerializeField] private float delay = 2f;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private GameObject explosionEffect;

    void Start()
    {
        Invoke(nameof(Explode), delay);
    }

    void Explode()
    {

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
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
