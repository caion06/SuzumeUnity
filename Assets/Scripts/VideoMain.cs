using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoMain : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    [SerializeField]private GameObject fhater;
    [SerializeField] private VideoClip clip2;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += endVideo;

        audioSource.Stop();

        if(SceneManager.GetActiveScene().name == "Phase_two")
        {
            videoPlayer.clip = clip2;
        }
    }

    private void endVideo(VideoPlayer vp)
    {
        audioSource.Play();
        Destroy(fhater);
    }
}
