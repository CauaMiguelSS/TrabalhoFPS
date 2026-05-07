using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float damage = 25f;
    [SerializeField] private float range = 2f;
    [SerializeField] private float radius = 1f;
    [SerializeField] private float attackDelay = 0.5f;

    [Header("Effect")]
    [SerializeField] private GameObject slashEffect;
    [SerializeField] private float effectDistance = 1f;
    [SerializeField] private float effectLifeTime = 0.5f;

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
        if (!canAttack) return;

        SpawnSlashEffect();

        Vector3 center = cam.transform.position + cam.transform.forward * range;
        Collider[] hits = Physics.OverlapSphere(center, radius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out IShootable shootable))
            {
                shootable.Hitted(
                    damage,
                    hit.ClosestPoint(cam.transform.position),
                    cam.transform.forward
                );

                break;
            }
        }

        StartCoroutine(AttackCooldown());
    }

    void SpawnSlashEffect()
    {
        if (slashEffect == null) return;

        Vector3 spawnPos = cam.transform.position + cam.transform.forward * effectDistance;

        GameObject effect = Instantiate(
            slashEffect,
            spawnPos,
            cam.transform.rotation
        );

        effect.transform.SetParent(cam.transform);

        Destroy(effect, effectLifeTime);
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
