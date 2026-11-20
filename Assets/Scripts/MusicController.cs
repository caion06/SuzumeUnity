using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    [Header("Musicas")]
    [SerializeField] private DataMusic dataMusic;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> musicas;

    private void Awake()
    {
        audioSource.time = dataMusic.musicTime;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Phase_two")
        {
            audioSource.clip = musicas[1];
        }
    }

    private void Update()
    {
        dataMusic.musicTime = audioSource.time;
    }
}
