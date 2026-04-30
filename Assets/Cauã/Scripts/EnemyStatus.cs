using UnityEngine;

public class EnemyStatus : MonoBehaviour, IShootable
{
    [SerializeField] private GameObject _bloodEffect;
    [SerializeField] private float _lifeMax = 20;

    private float _currentLife;

    void Start()
    {
        _currentLife = _lifeMax;
    }

    public void Hitted(float damage, Vector3 shootPoint)
    {
        _currentLife -= damage;

        if (_bloodEffect != null)
        {
            GameObject blood = Instantiate(_bloodEffect, shootPoint, Quaternion.identity);
            Destroy(blood, 2f);
        }

        if (_currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}