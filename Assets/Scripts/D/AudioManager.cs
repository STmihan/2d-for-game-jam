using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get { return _instance; }
    }
    static AudioManager _instance = null;

    public AudioSource audioSource;
    public AudioClip[] clipArray;
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (_instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        audioSource = GetComponent<AudioSource>();

        LoadClips();
        AudioMusic();
    }

    void LoadClips()
    {
        for (int i = 0; i < clipArray.Length ; i++)
        {
            audioClips.Add(clipArray[i].name, clipArray[i]);
        }        
    }   

    public void PlayClip(string NameClip)
    {
        audioSource.PlayOneShot(audioClips[NameClip]);       
    }
    void AudioMusic()
    {
        if (!audioSource.isPlaying)
        {
            if (!audioSource.clip == audioClips["Music"]) audioSource.clip = audioClips["Music"];
            audioSource.loop = true;
            audioSource.Play();            
        }
    }

}
