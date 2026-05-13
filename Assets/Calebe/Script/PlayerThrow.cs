using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [Header("Referências")]
    public Camera playerCamera;
    public Transform handPoint;
    public float pickDistance = 5f;
    public float throwForce = 15f;

    private GameObject heldItem;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldItem == null)
                TryPickItem();
            else
                ThrowItem();
        }
    }

    private void TryPickItem()
    {
        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );

        if (Physics.Raycast(ray, out RaycastHit hit, pickDistance))
        {
            if (hit.collider.CompareTag("Fruit"))
            {
                PickItem(hit.collider.gameObject);
            }
        }
    }

    private void PickItem(GameObject item)
    {
        heldItem = item;

        heldItem.transform.SetParent(handPoint);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;

        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void ThrowItem()
    {
        Rigidbody rb = heldItem.GetComponent<Rigidbody>();

        heldItem.transform.SetParent(null);

        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;

        rb.AddForce(
            playerCamera.transform.forward * throwForce,
            ForceMode.Impulse
        );

        Destroy(heldItem, 5f);

        heldItem = null;
    }
}
