using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Element { }

[System.Serializable]
public class GunElement : Element
{
    public UnityEvent OnReload = new UnityEvent();

    [Header("Visual")]
    [SerializeField] private GameObject _gunModel;
    [SerializeField] private string _name;

    [Header("Stats")]
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _shootRate = 0.3f;
    [SerializeField] private float _range = 100f;
    [SerializeField] private bool _automatic;

    [Header("Ammo")]
    [SerializeField] private int _totalAmmo = 60;
    [SerializeField] private int _clipSize = 12;
    [SerializeField] private float _reloadTime = 1.2f;

    [Header("Spread")]
    [SerializeField] private int _pellets = 1;
    [SerializeField] private float _spread = 0f;

    private int _currentClip;
    private bool _initialized;

    public void Initialize()
    {
        if (_initialized) return;

        _currentClip = _clipSize;
        _initialized = true;
    }

    public bool UseAmmo()
    {
        if (_currentClip <= 0)
        {
            if (_totalAmmo > 0)
                OnReload.Invoke();

            return false;
        }

        _currentClip--;
        return true;
    }

    public void Reload()
    {
        if (_totalAmmo <= 0) return;

        int ammoNeeded = _clipSize - _currentClip;
        if (ammoNeeded <= 0) return;

        int ammoToReload = Mathf.Min(ammoNeeded, _totalAmmo);

        _currentClip += ammoToReload;
        _totalAmmo -= ammoToReload;
    }

    public string Name => _name;
    public float Damage => _damage;
    public float ShootRate => _shootRate;
    public float Range => _range;
    public bool Automatic => _automatic;
    public int TotalAmmo => _totalAmmo;
    public int CurrentClip => _currentClip;
    public int ClipSize => _clipSize;
    public float ReloadTime => _reloadTime;
    public int Pellets => _pellets;
    public float Spread => _spread;
    public GameObject GunModel => _gunModel;
}
