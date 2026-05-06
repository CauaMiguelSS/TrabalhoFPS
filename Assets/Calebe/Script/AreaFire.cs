using UnityEngine;

public class AreaFire : MonoBehaviour
{
    public GameObject foodPrefab;
    public Transform firePoint;
    public float throwForce = 15f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowFood();
        }
    }

    void ThrowFood()
    {
        GameObject food = Instantiate(foodPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = food.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * throwForce, ForceMode.Impulse);
    }
}