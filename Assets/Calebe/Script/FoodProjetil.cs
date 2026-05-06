using UnityEngine;

public class FoodProjetil : MonoBehaviour
{
    public GameObject explosionEffect;

    void OnCollisionEnter(Collision collision)
    {
        // Só reage se for inimigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Inimigo enemy = collision.gameObject.GetComponent<Inimigo>();

            if (enemy != null)
            {
                enemy.Die();
            }

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}