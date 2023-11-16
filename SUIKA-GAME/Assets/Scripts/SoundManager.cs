using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    static public SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource[] effects;
    public AudioSource bgm;

    public string[] playSoundname;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;
    private void PlayerSE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if(_name == effectSounds[i].name)
            {
                for(int j = 0; j < effects.Length; j++)
                {
                    if (!effects[j].isPlaying)
                    {
                        playSoundname[j] = effectSounds[i].name;
                        effects[j].clip = effectSounds[i].clip;
                        effects[j].Play();
                        return;
                    }
                }
            }
        }
    }
    public void StopAllSE()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i< effects.Length; i++)
        {
            effects[i].Stop();
            return;
        }

    }
}
