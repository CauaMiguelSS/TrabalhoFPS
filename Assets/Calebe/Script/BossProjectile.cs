using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public int damage = 15;
    private bool hasHit;

    private void Start()
    {
        Destroy(gameObject, 8f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        PlayerHealth player =
            collision.collider.GetComponentInParent<PlayerHealth>();

        if (player != null)
        {
            hasHit = true;
            player.TakeDamage(damage);

            Destroy(gameObject, 3f);
        }
    }
}