using UnityEngine;
using UnityEngine.InputSystem;

public class GrenadeThrower : MonoBehaviour
{
    [Header("Granada")]
    [SerializeField] private GameObject grenadePrefab;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float upwardForce = 3f;

    [Header("Limite")]
    [SerializeField] private int maxGrenades = 3;
    private int currentGrenades;

    [Header("Delay entre granadas")]
    [SerializeField] private float throwDelay = 0.8f; // tempo entre usos
    private bool canThrow = true;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        currentGrenades = maxGrenades;
    }

    void Update()
    {
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        if (!canThrow)
        {
            return;
        }

        if (currentGrenades <= 0)
        {
            return;
        }

        if (grenadePrefab == null)
        {
            return;
        }

        Vector3 spawnPos = cam.transform.position + cam.transform.forward * 1f;

        GameObject grenade = Instantiate(
            grenadePrefab,
            spawnPos,
            Quaternion.identity
        );

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 force = cam.transform.forward * throwForce + Vector3.up * upwardForce;
            rb.AddForce(force, ForceMode.Impulse);
        }

        currentGrenades--;
        Debug.Log("Granadas restantes: " + currentGrenades);

        // ativa delay
        StartCoroutine(DelayBetweenThrows());
    }

    System.Collections.IEnumerator DelayBetweenThrows()
    {
        canThrow = false;
        yield return new WaitForSeconds(throwDelay);
        canThrow = true;
    }
}