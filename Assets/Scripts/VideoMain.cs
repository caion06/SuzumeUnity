using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoMain : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField]private GameObject fhater;
    [SerializeField] private AudioSource audioSource;
    public bool qualquer;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += endVideo;

        audioSource.Stop();
    }

    private void endVideo(VideoPlayer vp)
    {
        audioSource.Play();
        Destroy(fhater);

        if (SceneManager.GetActiveScene().name == "Phase_two")
        {
            SceneManager.LoadScene(0);
        }
    }
}
