using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class GunInventory
{
    [SerializeField] private List<GunElement> _guns = new List<GunElement>();

    public List<GunElement> Guns => _guns;

    public void AddWeapon(GunElement newGun)
    {
        if (!_guns.Contains(newGun))
            _guns.Add(newGun);
    }
}

public class GunSystem : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private GunInventory _gunInventory;

    [Header("Gun")]
    [SerializeField] private GunElement _handGun;
    [SerializeField] private Transform _handGunModelParent;

    private Transform _camera;
    private float _shootTimer;
    private bool _isReloading;

    void Start()
    {
        _camera = Camera.main.transform;

        SetupGun(_handGun);
        _gunInventory.AddWeapon(_handGun);
        ChangeGunVisual();
    }

    void Update()
    {
        HandleWeaponChange();
        HandleReload();
        HandleShoot();
    }

    private void HandleWeaponChange()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll != 0)
        {
            ChangeWeapon(scroll);
        }
    }

    private void HandleReload()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            if (_isReloading) return;
            if (_handGun.CurrentClip >= _handGun.ClipSize) return;
            if (_handGun.TotalAmmo <= 0) return;

            _handGun.OnReload.Invoke();
        }
    }

    private void HandleShoot()
    {
        _shootTimer += Time.deltaTime;

        if (_isReloading) return;
        if (_shootTimer < _handGun.ShootRate) return;

        bool shootInput;

        if (_handGun.Automatic)
            shootInput = Mouse.current.leftButton.isPressed;
        else
            shootInput = Mouse.current.leftButton.wasPressedThisFrame;

        if (!shootInput) return;

        if (!_handGun.UseAmmo()) return;

        ShootHitscan();

        _shootTimer = 0;
    }

    private void ShootHitscan()
    {
        for (int i = 0; i < _handGun.Pellets; i++)
        {
            Vector3 direction = _camera.forward;

            direction += _camera.right * Random.Range(-_handGun.Spread, _handGun.Spread);
            direction += _camera.up * Random.Range(-_handGun.Spread, _handGun.Spread);

            if (Physics.Raycast(_camera.position, direction, out RaycastHit hit, _handGun.Range))
            {
                if (hit.collider.TryGetComponent(out IShootable shootable))
                {
                    shootable.Hitted(_handGun.Damage, hit.point);
                }
            }
        }
    }

    private void ChangeWeapon(float direction)
    {
        if (_gunInventory.Guns.Count <= 1) return;
        if (_isReloading) return;

        int currentIndex = _gunInventory.Guns.IndexOf(_handGun);
        currentIndex += direction > 0 ? 1 : -1;

        if (currentIndex >= _gunInventory.Guns.Count)
            currentIndex = 0;
        else if (currentIndex < 0)
            currentIndex = _gunInventory.Guns.Count - 1;

        SetupGun(_gunInventory.Guns[currentIndex]);
        ChangeGunVisual();
    }

    private void SetupGun(GunElement gun)
    {
        _handGun = gun;
        _handGun.Initialize();

        _handGun.OnReload.RemoveAllListeners();
        _handGun.OnReload.AddListener(() => StartCoroutine(Reload()));

        _shootTimer = _handGun.ShootRate;
    }

    private IEnumerator Reload()
    {
        if (_isReloading) yield break;

        _isReloading = true;

        yield return new WaitForSeconds(_handGun.ReloadTime);

        _handGun.Reload();

        _shootTimer = _handGun.ShootRate;
        _isReloading = false;
    }

    public void AddNewGun(GunElement newGun)
    {
        SetupGun(newGun);
        _gunInventory.AddWeapon(newGun);
        ChangeGunVisual();
    }

    private void ChangeGunVisual()
    {
        if (_handGunModelParent.childCount > 0)
            Destroy(_handGunModelParent.GetChild(0).gameObject);

        if (_handGun.GunModel == null) return;

        GameObject gun = Instantiate(_handGun.GunModel, _handGunModelParent);

        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;

        gun.layer = LayerMask.NameToLayer("Gun");
    }
}
