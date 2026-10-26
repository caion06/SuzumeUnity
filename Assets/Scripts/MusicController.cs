using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        dataMusic.musicTime = audioSource.time;
    }
}
