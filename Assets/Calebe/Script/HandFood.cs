using UnityEngine;

public class FoodHandler : MonoBehaviour
{
    public Transform holdPoint;
    public float throwForce = 15f;
    public float pickUpRange = 3f;

    GameObject heldFood;

    void Update()
    {
        // PEGAR
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUp();
        }

        // ARREMESSAR
        if (Input.GetMouseButtonDown(0) && heldFood != null)
        {
            ThrowFood();
        }
    }

    void TryPickUp()
    {
        if (heldFood != null) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickUpRange))
        {
            if (hit.collider.CompareTag("Food"))
            {
                heldFood = hit.collider.gameObject;

                Rigidbody rb = heldFood.GetComponent<Rigidbody>();
                rb.isKinematic = true;

                heldFood.transform.position = holdPoint.position;
                heldFood.transform.parent = holdPoint;
            }
        }
    }

    void ThrowFood()
    {
        heldFood.transform.parent = null;

        Rigidbody rb = heldFood.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        rb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

        heldFood = null;
    }
}
    
