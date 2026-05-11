using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    [Header("Recoil")]
    [SerializeField] private Vector3 recoilDirection = new Vector3(0, 0, -0.08f);
    [SerializeField] private float recoilReturnSpeed = 8f;
    [SerializeField] private float recoilSnappiness = 12f;

    private Vector3 startPosition;
    private Vector3 currentOffset;
    private Vector3 targetOffset;

    void Start()
    {
        startPosition = transform.localPosition;
    }

    void Update()
    {
        targetOffset = Vector3.Lerp(targetOffset, Vector3.zero, recoilReturnSpeed * Time.deltaTime);
        currentOffset = Vector3.Lerp(currentOffset, targetOffset, recoilSnappiness * Time.deltaTime);

        transform.localPosition = startPosition + currentOffset;
    }

    public void Recoil()
    {
        targetOffset += recoilDirection;
    }
}
