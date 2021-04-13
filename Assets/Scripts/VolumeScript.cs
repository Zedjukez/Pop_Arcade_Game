using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public GameObject OptionsMenu;
    public GameObject AudioSource;
    public float musicVolume;
    private float SelectedMusicVolume; // alle selected/presave benamingen zijn van toen ik Cancel en Save button wilde maken ~ Vincent
    private int musicVolumeMute;

    public Slider MusicVolumeSlider;
    public Toggle MusicMute;


    public Toggle MuteSound;
    private float SoundVolume;
    private int SoundMute;
    private bool _mutesound;

    public Slider SoundVolumeSlider;
    public GameObject SoundManager;

    void Start(){
        OptionsMenu.SetActive(false);
        if (GameObject.FindGameObjectWithTag("musicbox") == null) return;
        AudioSource = GameObject.FindGameObjectWithTag("musicbox");
        
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.35f);
        musicVolumeMute = PlayerPrefs.GetInt("musicVolumeMute", 0);

        AudioSource.GetComponent<AudioSource>().volume = musicVolume;
        MusicVolumeSlider.value = musicVolume;


        if (musicVolumeMute == 1){
            AudioSource.GetComponent<AudioSource>().mute = true;
            MusicVolumeSlider.interactable = false;
            MusicMute.isOn = true;
        } else { AudioSource.GetComponent<AudioSource>().mute = false;
        MusicVolumeSlider.interactable = true;
        MusicMute.isOn = false;}




        //SOUND VFX
        SoundVolume = PlayerPrefs.GetFloat("soundVolume", 1f);
        SoundMute = PlayerPrefs.GetInt("soundVolumeMute", 0);
        SoundVolumeSlider.value = SoundVolume;
        
        if (SoundMute == 1){
            SoundVolumeSlider.interactable = false;
            MuteSound.isOn = true;
            _mutesound = true;
        } else {
        SoundVolumeSlider.interactable = true;
        _mutesound = false;
        MuteSound.isOn = false;}

        if (SceneManager.GetActiveScene().buildIndex == 1){
          SoundManager.GetComponent<SoundeffectsManager>().SetVolumeAll(SoundVolume);
          SoundManager.GetComponent<SoundeffectsManager>().SetMuteState(_mutesound);
        }


    }




    public void PreSaveMusicAudio(float volume){
        SelectedMusicVolume = volume;
        AudioSource.GetComponent<AudioSource>().volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);

    }

    public void PreMuteMusicAudio(bool mute){
        if (mute){
            MusicVolumeSlider.interactable = false;
            AudioSource.GetComponent<AudioSource>().mute = true;
            PlayerPrefs.SetInt("musicVolumeMute", 1);
        } else {
            MusicVolumeSlider.interactable = true;
            AudioSource.GetComponent<AudioSource>().mute = false;
            PlayerPrefs.SetInt("musicVolumeMute", 0);}

    }

    public void SaveSoundVolume(float volume){
        SoundVolume = volume;
        PlayerPrefs.SetFloat("soundVolume", volume);
        if (SceneManager.GetActiveScene().buildIndex == 1)
        SoundManager.GetComponent<SoundeffectsManager>().SetVolumeAll(SoundVolume);
    }

    public void SaveMute(bool mute){
        if (mute){
            SoundVolumeSlider.interactable = false;
            PlayerPrefs.SetInt("soundVolumeMute", 1);


        } else {
            SoundVolumeSlider.interactable = true;
            PlayerPrefs.SetInt("soundVolumeMute", 0);
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        SoundManager.GetComponent<SoundeffectsManager>().SetMuteState(mute);

    }





    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)){
            if (OptionsMenu.activeInHierarchy == true){
                OptionsMenu.SetActive(false);
                Time.timeScale = 1;
                StaticGameSaver.GameisPaused = false;
               


            } else { OptionsMenu.SetActive(true); Time.timeScale = 0; StaticGameSaver.GameisPaused = true;

            
            }
        }
    }

}
