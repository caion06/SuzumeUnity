using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Controla as interações dos botões e menus da interface
public class InteractionsController : MonoBehaviour
{
    public GameObject configuracoes; // Painel do menu de configurações
    public Image shinebackground;    // Camada escura sobreposta para simular o controle de brilho

    // Carrega a cena do jogo (índice 1 no Build Settings)
    public void InitialGame()
    {
        SceneManager.LoadScene(1);
    }

    // Abre o menu de configurações
    public void OpenConfig()
    {
        configuracoes.SetActive(true);
    }

    // Fecha o menu de configurações
    public void ExitConfig()
    {
        configuracoes.SetActive(false);
    }

    // Simula brilho em 25% deixando a camada escura em 50% de opacidade (Alpha = 0.5)
    public void Shine25()
    {
        // Pega a cor atual da imagem
        Color cor = shinebackground.color;

        // Define a transparência (Alpha)
        cor.a = 0.5f;
        
        // Aplica a nova cor modificada de volta na imagem
        shinebackground.color = cor;
    }

    // Simula brilho em 50% deixando a camada escura em 25% de opacidade (Alpha = 0.25)
    public void Shine50()
    {
        Color cor = shinebackground.color;

        cor.a = 0.25f;

        shinebackground.color = cor;
    }

    // Simula brilho em 100% deixando a camada escura totalmente transparente (Alpha = 0)
    public void Shine100()
    {
        Color cor = shinebackground.color;

        cor.a = 0f;

        shinebackground.color = cor;
    }

    // Fecha o jogo tanto no modo de teste do Editor quanto no executável final (Build)
    public void ExitGame()
    {
        #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;

        #else
            
            Application.Quit();

        #endif
    }
}