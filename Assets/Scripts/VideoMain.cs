using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoMain : MonoBehaviour
{
    // Referências ao componente de vídeo, objeto pai, áudio de fundo e controle auxiliar
    private VideoPlayer videoPlayer;
    [SerializeField]private GameObject fhater;
    [SerializeField] private AudioSource audioSource;
    public bool qualquer;

    // Configura o evento de conclusão do vídeo e pausa o áudio principal ao iniciar
    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += endVideo;

        audioSource.Stop();
    }

    // Disparado automaticamente quando o vídeo chega ao fim
    private void endVideo(VideoPlayer vp)
    {
        // Retoma o áudio de fundo e destrói o objeto pai do vídeo
        audioSource.Play();
        Destroy(fhater);

        // Se estiver na fase "Phase_two", volta para o menu/cena inicial (índice 0)
        if (SceneManager.GetActiveScene().name == "Phase_two")
        {
            SceneManager.LoadScene(0);
        }
    }
}