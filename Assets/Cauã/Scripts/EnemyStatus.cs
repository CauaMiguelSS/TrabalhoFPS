using UnityEngine;

public class EnemyStatus : MonoBehaviour, IShootable
{
    [SerializeField] private GameObject _bloodEffect;
    [SerializeField] private float _lifeMax = 20f;

    [Header("Blood Settings")]
    [SerializeField] private float _bloodSize = 2f;
    [SerializeField] private Vector3 _rotationOffset = Vector3.zero;

    private float _currentLife;

    void Start()
    {
        _currentLife = _lifeMax;
    }

    public void Hitted(float damage, Vector3 shootPoint, Vector3 shootDirection)
    {
        _currentLife -= damage;

        if (_bloodEffect != null)
        {
            Vector3 bloodDirection = -shootDirection.normalized;

            Quaternion rotation = Quaternion.LookRotation(bloodDirection);

            GameObject blood = Instantiate(
                _bloodEffect,
                shootPoint,
                rotation * Quaternion.Euler(_rotationOffset)
            );

            blood.transform.localScale *= _bloodSize;

            Destroy(blood, 3f);
        }

        if (_currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hitted(float damage, Vector3 vector3)
    {
        throw new System.NotImplementedException();
    }
}