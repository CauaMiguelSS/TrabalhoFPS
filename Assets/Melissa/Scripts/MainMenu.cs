using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string nomeDaCenaDoJogo;

    public void Jogar()
    {
        SceneManager.LoadScene(nomeDaCenaDoJogo);
    }

    public void Sair()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}