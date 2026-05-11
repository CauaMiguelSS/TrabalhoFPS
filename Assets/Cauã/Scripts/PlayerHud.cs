using UnityEngine;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GunSystem gunSystem;
    [SerializeField] private TMP_Text ammoText;

    void Update()
    {
        UpdateAmmo();
    }

    private void UpdateAmmo()
    {
        if (gunSystem == null) return;
        if (gunSystem.CurrentGun == null) return;
        if (ammoText == null) return;

        GunElement gun = gunSystem.CurrentGun;

        ammoText.text = gun.CurrentClip + " / " + gun.TotalAmmo;
    }
}
