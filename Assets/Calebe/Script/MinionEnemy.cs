using UnityEngine;

public class MinionEnemy : EnemyBase
{
    [Header("Movimento")]
    public Transform player;
    public float speed = 3f;

    [Header("Ataque")]
    public int damage = 10;
    public float attackCooldown = 1f;

    private float attackTimer;

    private void Update()
    {
        if (player == null) return;

        FollowPlayer();

        attackTimer += Time.deltaTime;
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );
    }

    private void OnCollisionStay(Collision collision)
    {
        PlayerHealth playerHealth =
            collision.collider.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null && attackTimer >= attackCooldown)
        {
            playerHealth.TakeDamage(damage);
            attackTimer = 0f;
        }
    }
}