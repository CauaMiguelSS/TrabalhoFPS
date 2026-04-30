using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable
{
    public abstract Element Collect();

    protected abstract void Teste1();

    protected virtual void Teste2()
    {

    }

    protected void Teste3()
    {

    }
}
