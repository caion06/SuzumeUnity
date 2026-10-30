using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionsController : MonoBehaviour
{
    public GameObject configuracoes;

    public void InitialGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenConfig()
    {
        configuracoes.SetActive(true);
    }

    public void ExitConfig()
    {
        configuracoes.SetActive(false);
    }

    public void Shine25()
    {
        Screen.brightness = 0.5f;
    }

    public void Shine50()
    {
        Screen.brightness = 0.25f;
    }

    public void Shine100()
    {
        Screen.brightness = 1.0f;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;

        #else
            
            Application.Quit();

        #endif
    }
}
