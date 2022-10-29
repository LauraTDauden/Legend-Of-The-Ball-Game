using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicLoop : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicStart;

    private static MusicLoop musicInstance;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.PlayOneShot(musicStart);
        musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
    }

    private void Awake()
    {
        if (musicInstance == null)
        {
            musicInstance = this;
        }
        else Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {

    }
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex != 2)
        {
            Destroy(this);
        }
    }
}
