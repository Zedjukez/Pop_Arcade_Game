using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckPlayerData : MonoBehaviour
{
    public GameObject canvasNew;
    public GameObject canvasOldPlayer;
    public GameObject SelectScreen;
    public GameObject Resetscreen;
    public GameObject Creditsscreen;

    public Text Greetings;

    public Text Error;
    public InputField namebox;
    public Text[] UIList;

    void Start(){
        Creditsscreen.SetActive(false);
        Error.text = "";
        Resetscreen.SetActive(false);
        SelectScreen.SetActive(false);
    if(!PlayerPrefs.HasKey("name")){
        canvasNew.SetActive(true);
        canvasOldPlayer.SetActive(false); 
         } else {
         canvasOldPlayer.SetActive(true); 
         canvasNew.SetActive(false);

         Greetings.text = "Hi, " + PlayerPrefs.GetString("name", "player");
        calculateAvergHit();
        



        for(int i = 0; i < UIList.Length; i++){
                UIList[i].text = PlayerPrefs.GetString(UIList[i].name, "N/A");

        }

         }


    }



    public void savePlayerName(){
        if(namebox.text.Length < 3){
           Error.text = "Your name must be atleast 3 characters long.";
           return;
           
        } else if(namebox.text.Length > 10) {
            Error.text = "Your name can't be longer than 10 characters.";
            return;
        }
        
        PlayerPrefs.SetString("name", namebox.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void ResetGame(){
        SaveSettingsIntoStatic();
        PlayerPrefs.DeleteAll();
        SaveSettingsIntoPref(); //Om te verkomen dat Resetgame de Options reset.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetProfile(){
        for(int i = 0; i < UIList.Length; i++){
        PlayerPrefs.DeleteKey(UIList[i].name);

        }
        PlayerPrefs.DeleteKey("name");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGameSolo(){
        StaticGameSaver.Playercount = 1;
        StaticGameSaver.points = 0;
        StaticGameSaver.totalhit = 0;
        StaticGameSaver.totalshot = 0;
        StaticGameSaver.totalmissed = 0;
        StaticGameSaver.hitaccuracy = 0;
        SceneManager.LoadScene(1);
    }

        public void StartGameDuo(){
        StaticGameSaver.Playercount = 2;
        StaticGameSaver.points = 0;
        StaticGameSaver.totalhit = 0;
        StaticGameSaver.totalshot = 0;
        StaticGameSaver.totalmissed = 0;
        StaticGameSaver.hitaccuracy = 0;
        SceneManager.LoadScene(1);
    }




    private void calculateAvergHit(){
        if (PlayerPrefs.GetString("totalbubbles", "N/A") != "N/A" && PlayerPrefs.GetString("totalenemies", "N/A") != "N/A") {
            string playerprefstringTotalb = PlayerPrefs.GetString("totalbubbles", "N/A");
            float.TryParse(playerprefstringTotalb, out float totalbubblesf);

            string playerprefstringTotalEnem = PlayerPrefs.GetString("totalenemies", "N/A");
            float.TryParse(playerprefstringTotalEnem, out float totalenemiesf);

            float hitPercentagefloat =  totalenemiesf / totalbubblesf * 100f;
            double hitpercentagerounded = System.Math.Round(hitPercentagefloat,2);


            PlayerPrefs.SetString("hitaccur", hitpercentagerounded + "%");

            } else return;
        }

   private void SaveSettingsIntoStatic(){
        StaticGameSaver.MusicVolume = PlayerPrefs.GetFloat("musicVolume", 0.35f);
        StaticGameSaver.MusicVolumeMute = PlayerPrefs.GetInt("musicVolumeMute", 0);
        StaticGameSaver.SoundVolume = PlayerPrefs.GetFloat("soundVolume", 1);
        StaticGameSaver.SoundVolumeMute = PlayerPrefs.GetInt("soundVolumeMute", 0);
    }

    private void SaveSettingsIntoPref(){
        PlayerPrefs.SetFloat("musicVolume", StaticGameSaver.MusicVolume);
        PlayerPrefs.SetInt("musicVolumeMute", StaticGameSaver.MusicVolumeMute);
        PlayerPrefs.SetFloat("soundVolume", StaticGameSaver.SoundVolume);
        PlayerPrefs.SetInt("soundVolumeMute", StaticGameSaver.SoundVolumeMute);
    }

}
