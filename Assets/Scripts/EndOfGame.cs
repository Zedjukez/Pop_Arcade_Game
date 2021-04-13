using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfGame : MonoBehaviour
{
   public GameObject leaderboardnotifier;
   public Text leaderboardnotifiertext;

   public GameObject CanvasTotal;
   public GameObject CanvasScoreboard;
   public GameObject CanvasResults;
   public GameObject NameEditorCanvas;
   
    public InputField namebox;
    public Text placeholdertext;
    public Text Error;


    public Text[] UIList;
    public Text[] Highscore;

    public Text[] HighscorelistNames;

    public Text[] highscorelistPlayercount;
    public Text[] HighscorelistScores;
    public GameObject[] ScorebordEditorIcons;
    
    private int newspot = -1;
    private bool _hScorefound = false;
    private bool nomove = true;
    
    private string lastscore;
    private string thisscore;
    private string lastname;
    private string thisname;

    private string playercountstring;
    private string lastplayercount;
    private string thisplayercount;
        
    
    // Start is called before the first frame update
    void Start()
    {
        
        NameEditorCanvas.SetActive(false);
        CanvasTotal.SetActive(true);
        CanvasScoreboard.SetActive(false);
        CanvasResults.SetActive(true);



        UpdateGametimeGlobal();
        UpdateHighscore(StaticGameSaver.points);

        string playerprefstring = PlayerPrefs.GetString("highscore", "N/A");
        int.TryParse(playerprefstring, out int playerprefint);

        string playerprefstring1 = PlayerPrefs.GetString("longestgame", "N/A").Substring(0, PlayerPrefs.GetString("longestgame", "N/A").Length - 1);
        double.TryParse(playerprefstring1, out double playerprefdouble);

        string playerprefstring2 = PlayerPrefs.GetString("fastestround", "N/A").Substring(0, PlayerPrefs.GetString("fastestround", "N/A").Length - 1);
        double.TryParse(playerprefstring2, out double playerprefdouble1);



        UIList[0].text = StaticGameSaver.points + "";
        if(StaticGameSaver.points == playerprefint){Highscore[0].gameObject.SetActive(true);} else {Highscore[0].gameObject.SetActive(false);}


        UIList[1].text = StaticGameSaver.totaltimed + "s";
        if(StaticGameSaver.totaltimed == playerprefdouble){Highscore[1].gameObject.SetActive(true);} else {Highscore[1].gameObject.SetActive(false);}

        if (StaticGameSaver.fastestTime != 999999999.99){
        UIList[2].text = StaticGameSaver.fastestTime + "s";
        } else{UIList[2].text = "N/A";}

        if (StaticGameSaver.fastestTime == playerprefdouble1) {Highscore[2].gameObject.SetActive(true);} else {Highscore[2].gameObject.SetActive(false);}
        
        UIList[3].text = StaticGameSaver.gem + "";
        UIList[4].text = StaticGameSaver.wave + "";
        UIList[5].text = StaticGameSaver.totalshot + "";
        UIList[6].text = StaticGameSaver.totalhit + "";
        UIList[7].text = StaticGameSaver.hitaccuracy + "%";
        UIList[8].text = "Wave " + StaticGameSaver.fastestwave;

        UpdateScoreboard();

    }

    public void Continue(){
        SceneManager.LoadScene(0);
        StaticGameSaver.gem = 0;
    }


    void UpdateGametimeGlobal() {
    if(PlayerPrefs.GetString("longestgame", "N/A") != "N/A"){
        string playerprefstring = PlayerPrefs.GetString("longestgame", "N/A").Substring(0, PlayerPrefs.GetString("longestgame", "N/A").Length - 1);
        double.TryParse(playerprefstring, out double playerprefdouble);
        if(StaticGameSaver.totaltimed > playerprefdouble) {
        PlayerPrefs.SetString("longestgame", StaticGameSaver.totaltimed + "s");
        }

    } else PlayerPrefs.SetString("longestgame", StaticGameSaver.totaltimed + "s");

    }


    void UpdateHighscore(int score){
        if(PlayerPrefs.GetString("highscore", "N/A") != "N/A"){
        string playerprefstring = PlayerPrefs.GetString("highscore", "N/A");
        int.TryParse(playerprefstring, out int playerprefint);
        if(score > playerprefint){
            PlayerPrefs.SetString("highscore", score + "");
        }

    }   else PlayerPrefs.SetString("highscore", score + "");


    }

    void UpdateScoreboard(){
        for(int i = 0; i < HighscorelistScores.Length; i++){
        
        int.TryParse( PlayerPrefs.GetString(HighscorelistScores[i].name, "0000000"), out int HScore);
        int CurrentScore = StaticGameSaver.points;
        if (CurrentScore != 0){
            if (!_hScorefound){
            if (HScore == 0){
                nomove = true;
                _hScorefound = true;
                newspot = i;
                PlayerPrefs.SetString(HighscorelistScores[i].name, CurrentScore.ToString("0000000"));
                
                }
            else if (HScore < CurrentScore){
                newspot = i;
                nomove = false;
                _hScorefound = true;
                lastscore = PlayerPrefs.GetString(HighscorelistScores[i].name, "0000000");
                PlayerPrefs.SetString(HighscorelistScores[i].name, CurrentScore.ToString("0000000"));
                }    
            }
    	 else if (!nomove){
             if (newspot != -1){
            thisscore =  PlayerPrefs.GetString(HighscorelistScores[i].name, "0000000");
            PlayerPrefs.SetString(HighscorelistScores[i].name, lastscore);
            lastscore = thisscore;}
        }
        }
            HighscorelistScores[i].text = PlayerPrefs.GetString(HighscorelistScores[i].name, "0000000");
        }


        for(int i = 0; i < HighscorelistNames.Length; i++){
        if (newspot != -1){
            if(newspot == i){
                string naamnu = PlayerPrefs.GetString("name", "player");
                lastname = PlayerPrefs.GetString(HighscorelistNames[i].name, "---");
                PlayerPrefs.SetString(HighscorelistNames[i].name, naamnu);
            }
            else if (newspot < i && !nomove){
                thisname = PlayerPrefs.GetString(HighscorelistNames[i].name, "---");
                PlayerPrefs.SetString(HighscorelistNames[i].name, lastname);
                lastname = thisname;
            }
        }
        HighscorelistNames[i].text = PlayerPrefs.GetString(HighscorelistNames[i].name, "---");
        }


        for(int i = 0; i < highscorelistPlayercount.Length; i++){
        if (newspot != -1){
            if (newspot == i){
                int playercountcurrent = StaticGameSaver.Playercount;
                if(playercountcurrent == 1) {playercountstring = "SOLO";}
                else { playercountstring = "DUO";}
                lastplayercount = PlayerPrefs.GetString(highscorelistPlayercount[i].name, "---");
                PlayerPrefs.SetString(highscorelistPlayercount[i].name, playercountstring);
            }
            else if (newspot < i && !nomove){
            thisplayercount = PlayerPrefs.GetString(highscorelistPlayercount[i].name, "---");
            PlayerPrefs.SetString(highscorelistPlayercount[i].name, lastplayercount);
            lastplayercount = thisplayercount;
        }
        }
        highscorelistPlayercount[i].text = PlayerPrefs.GetString(highscorelistPlayercount[i].name, "---");
        }

        //text notificatie
        if (newspot != -1){
            placeholdertext.text = PlayerPrefs.GetString(HighscorelistNames[newspot].name, "---");
            leaderboardnotifier.SetActive(true);
            int newspotactual = newspot + 1;
            Error.gameObject.SetActive(false);
            leaderboardnotifiertext.text = "U hit spot #" + newspotactual + "\non the leaderbord!";
            for(int i = 0; i < ScorebordEditorIcons.Length; i++) {
                if (i == newspot){ScorebordEditorIcons[i].gameObject.SetActive(true);}
                else if (i != newspot) {ScorebordEditorIcons[i].gameObject.SetActive(false);}
            }

        } else{leaderboardnotifier.SetActive(false); 
         for(int i = 0; i < ScorebordEditorIcons.Length; i++) {
             ScorebordEditorIcons[i].gameObject.SetActive(false);
            
            }

        }

        
    }
    

    public void SaveScoreBoardName(){
        if(namebox.text.Length < 3){
           Error.text = "Your name must be atleast 3 characters long.";
            Error.gameObject.SetActive(true);
           return;
           
        } else if(namebox.text.Length > 10) {
            Error.text = "Your name cannot be longer than 10 characters.";
            Error.gameObject.SetActive(true);
            return;
        }
        
        if(namebox.text ==  PlayerPrefs.GetString(HighscorelistNames[newspot].name, "---")){
            Error.text = "Your new name cannot be the same.";
            Error.gameObject.SetActive(true);
            return;
        }

        GetComponent<keyboard>().DisableKeyboard();
        Error.gameObject.SetActive(false);
        PlayerPrefs.SetString(HighscorelistNames[newspot].name, namebox.text);
        HighscorelistNames[newspot].text = PlayerPrefs.GetString(HighscorelistNames[newspot].name, "---");
        placeholdertext.text = PlayerPrefs.GetString(HighscorelistNames[newspot].name, "---");
        NameEditorCanvas.SetActive(false);
        CanvasTotal.SetActive(true);
        CanvasScoreboard.SetActive(true);
        CanvasResults.SetActive(false);
    }

    public void Cancel(){
        placeholdertext.text = PlayerPrefs.GetString(HighscorelistNames[newspot].name, "---");
        namebox.text = "";
    }



}
