using UnityEngine;

public class MeeleWeapon : MonoBehaviour
{
    [Header("Ataque")]
    public Animator animator;
    public int damage = 2;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    private Camera playerCamera;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, enemyLayer))
        {
            EnemyBase enemy =
                hit.collider.GetComponentInParent<EnemyBase>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}