using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    public void SFXPlay(string sfxname, AudioClip Clip)
    {
        GameObject go = new GameObject(sfxname + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();

        audioSource.clip = Clip;
        audioSource.Play();

        Destroy(go, Clip.length);

    }

}
