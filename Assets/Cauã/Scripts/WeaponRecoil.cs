using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public Transform weaponHolder;

    private float recoilAmount = 2f;
    private float recoilSpeed = 15f;
    private float returnSpeed = 10f;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, recoilSpeed * Time.deltaTime);

        weaponHolder.localRotation = Quaternion.Euler(currentRotation);
    }

    public void SetRecoil(float amount, float speed, float returnSpd)
    {
        recoilAmount = amount;
        recoilSpeed = speed;
        returnSpeed = returnSpd;
    }

    public void Recoil()
    {
        targetRotation += new Vector3(-recoilAmount, Random.Range(-1f, 1f), 0f);
    }
}
