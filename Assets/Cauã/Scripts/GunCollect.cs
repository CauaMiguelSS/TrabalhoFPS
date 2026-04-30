using UnityEngine;

public class GunCollect : Item
{
    [SerializeField] private GunElement _attributes;

    public override Element Collect()
    {
        Destroy(gameObject);
        return _attributes;
    }

    protected override void Teste1()
    {

    }
}
