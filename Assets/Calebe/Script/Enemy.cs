using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public float speed = 3f;
    public Transform player;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}