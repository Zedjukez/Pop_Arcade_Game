using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundeffectsManager : MonoBehaviour
{

    public Sound[] sounds;
    
    void Awake(){
        foreach(Sound s in sounds){

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audio;
            s.source.volume = s.Volume;
            s.source.loop = false;

        }
    }


    public void playSound(string name){
        foreach (Sound s in sounds){
           if (s.name == name){
               s.source.Play();

           } 

        }
    }








    public void SetVolumeAll(float volume){
        foreach(Sound s in sounds){
            
            s.Volume = volume;
            s.source.volume = s.Volume;
            

        }
    }

    public void SetMuteState(bool mute){
        foreach(Sound s in sounds){
        s.source.mute = mute;
        }
    }


}

[System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip audio;
        [Range(0f, 1f)]
        public float Volume;

        [HideInInspector]
        public AudioSource source;
        
    }