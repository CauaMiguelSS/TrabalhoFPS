using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [Header("Configuração")]
    public int damage = 15;
    public float lifeTime = 8f;

    private bool hasHit = false;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
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
        else
        {
            Destroy(gameObject);
        }
    }
}