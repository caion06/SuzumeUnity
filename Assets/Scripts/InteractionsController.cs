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

    public void ExitGame()
    {
        #if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;

        #else
            
            Application.Quit();

        #endif
    }
}
