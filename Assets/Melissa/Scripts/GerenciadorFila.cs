using UnityEngine;

[System.Serializable]
public class PontoAtendimento
{
    public Transform posicao;
    public Transform olhar;

    [HideInInspector] public bool ocupado;
}

public class GerenciadorFila : MonoBehaviour
{
    public PontoAtendimento[] pontos;

    public PontoAtendimento PegarPontoLivre()
    {
        foreach (var p in pontos)
        {
            if (!p.ocupado)
            {
                p.ocupado = true;
                return p;
            }
        }

        return null;
    }

    public void LiberarPonto(PontoAtendimento ponto)
    {
        if (ponto != null)
            ponto.ocupado = false;
    }
}