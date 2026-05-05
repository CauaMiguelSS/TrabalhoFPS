using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float damage = 25f;
    [SerializeField] private float range = 2f;
    [SerializeField] private float radius = 1f;

    [Header("Delay")]
    [SerializeField] private float attackDelay = 0.5f;
    private bool canAttack = true;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Attack();
        }
    }

    void Attack()
    {
        if (!canAttack)
        {
            Debug.Log("Aguarde...");
            return;
        }

        Vector3 center = cam.transform.position + cam.transform.forward * range;

        Collider[] hits = Physics.OverlapSphere(center, radius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out IShootable shootable))
            {
                shootable.Hitted(damage, hit.ClosestPoint(cam.transform.position), cam.transform.forward);
                Debug.Log("Acertou melee em: " + hit.name);
                break;
            }
        }

        StartCoroutine(AttackCooldown());
    }

    System.Collections.IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    // só pra visualizar no editor
    void OnDrawGizmosSelected()
    {
        if (cam == null) return;

        Gizmos.color = Color.red;
        Vector3 center = cam.transform.position + cam.transform.forward * range;
        Gizmos.DrawWireSphere(center, radius);
    }
}
