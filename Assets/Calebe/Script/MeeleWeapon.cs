using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float attackRange = 2f;
    public int damage = 2;
    public float attackCooldown = 0.5f;

    public Camera playerCamera;
    public Animator animator;

    private bool canAttack = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && canAttack)
        {
            Attack();
        }
    }

    private void Attack()
    {
        canAttack = false;

        animator.SetTrigger("Attack");

        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, attackRange))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}