using UnityEngine;

public class BossEnemy : EnemyBase
{
    [Header("Ataque")]
    public GameObject[] foodProjectiles;
    public Transform shootPoint;
    public Transform player;

    public float attackCooldown = 3f;
    public float projectileForce = 20f;

    private float timer;

    private void Update()
    {
        if (player == null || shootPoint == null)
            return;

        Vector3 directionToPlayer =
            player.position - transform.position;

        directionToPlayer.y = 0f;

        if (directionToPlayer != Vector3.zero)
        {
            transform.rotation =
                Quaternion.LookRotation(directionToPlayer);

            transform.Rotate(0f, 180f, 0f);
        }

        timer += Time.deltaTime;

        if (timer >= attackCooldown)
        {
            ShootFood();
            timer = 0f;
        }
    }

    private void ShootFood()
    {
        if (player == null || shootPoint == null)
            return;

        int index = Random.Range(0, foodProjectiles.Length);

        GameObject projectile = Instantiate(
            foodProjectiles[index],
            shootPoint.position,
            Quaternion.identity
        );

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction =
                (player.position - shootPoint.position).normalized;

            rb.AddForce(
                direction * projectileForce,
                ForceMode.Impulse
            );
        }
    }
}