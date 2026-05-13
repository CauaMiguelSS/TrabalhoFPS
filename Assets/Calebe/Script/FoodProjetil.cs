using UnityEngine;

public class FoodProjetil : MonoBehaviour
{
    public GameObject explosionEffect;

    void OnCollisionEnter(Collision collision)
    {
        // Só reage se for inimigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

    }

}