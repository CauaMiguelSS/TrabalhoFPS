using UnityEngine;

public class FoodProjetil : MonoBehaviour
{
    [Header("Configuração")]
    public int damage = 1;
    public GameObject explosionEffect;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Enemy"))
            return;

        EnemyBase enemy =
            collision.collider.GetComponentInParent<EnemyBase>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        if (explosionEffect != null)
        {
            Instantiate(
                explosionEffect,
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }
}