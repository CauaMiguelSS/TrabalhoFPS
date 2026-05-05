using UnityEngine;

public interface IShootable
{
    void Hitted(float damage, Vector3 shootPoint, Vector3 direction);
}
