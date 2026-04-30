using UnityEngine;

public class MenuCameraMotion : MonoBehaviour
{
    public float intensidade = 2f;
    public float suavidade = 5f;

    private Vector3 rotacaoInicial;

    void Start()
    {
        rotacaoInicial = transform.eulerAngles;
    }

    void Update()
    {
        // pega posição do mouse (0 a 1)
        float mouseX = Input.mousePosition.x / Screen.width;
        float mouseY = Input.mousePosition.y / Screen.height;

        // transforma em -1 a 1
        float offsetX = (mouseX - 0.5f) * 2f;
        float offsetY = (mouseY - 0.5f) * 2f;

        // calcula rotação alvo
        Quaternion rotacaoAlvo = Quaternion.Euler(
            rotacaoInicial.x - offsetY * intensidade,
            rotacaoInicial.y + offsetX * intensidade,
            0f
        );

        // suaviza movimento
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            rotacaoAlvo,
            suavidade * Time.deltaTime
        );
    }
}