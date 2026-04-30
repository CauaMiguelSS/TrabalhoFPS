using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    private GunSystem _gunSystem;

    void Start()
    {
        _gunSystem = GetComponentInParent<GunSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out ICollectable collectable))
            return;

        if (other.gameObject.CompareTag("Gun"))
        {
            Element element = collectable.Collect();

            if (element is GunElement gun)
            {
                _gunSystem.AddNewGun(gun);
            }
        }
    }
}
