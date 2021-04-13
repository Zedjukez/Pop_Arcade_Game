using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Diagnostics;

using UnityEngine.SceneManagement;

public class WaveHandler : MonoBehaviour
{   

    public TMPro.TMP_Text Wave;
    public TMPro.TMP_Text Timetext;
    public TMPro.TMP_Text Wavetext;
    public GameObject player;
    public GameObject Player2;
    public GameObject[] spawnpoints;
    public List<GameObject> spawnobueno;

    public List<GameObject> Alive;
    
    public List<int> EnemiesToSpawn;


    public GameObject PrefabNormal0;
    public GameObject PrefabTwohit1;
    public GameObject PrefabM;
    int displayedEnemies = -1;
    public int Wavec = 0;
    public Stopwatch timer = new Stopwatch();
   
    public double Totaltime = 0f;
    private double Rondetijd;
    public int FastestWave;
    public double FastestTime= -1;
    public GameObject NoNoSpawn;
    private double viewedTime;

    // Start is called before the first frame update
    void Start()
    {

    
        spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(displayedEnemies != Alive.Count){
        Wave.text = Alive.Count + "";
        displayedEnemies = Alive.Count;
        }

        if (Wavetext.text != Wavec + "")
            Wavetext.text = Wavec + "";




        if(Input.GetKeyDown(KeyCode.R)){
        for(int i = 0; i < Alive.Count; i++){
            Destroy(Alive[i]);
            
            
        }
        Alive.Clear();
        GetTimerData(true);
        Spawner();

        }
        
        if(Input.GetKeyDown(KeyCode.T)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        





        if(Alive.Count == 0) {
        GetTimerData(true);
        Spawner();
        
    }
    }


    void RandomWave(){
        EnemiesToSpawn.Clear();
        if (Wavec < 10) {

            switch(Wavec){

                case 0:
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                break;

                case 1:
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(1);
                break;


                case 2:
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(1);
                break;

                case 3:
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(2);
                break;

                case 4:
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(2);
                EnemiesToSpawn.Add(2);
                break;

                case 5:
                EnemiesToSpawn.Add(2);
                EnemiesToSpawn.Add(2);
                EnemiesToSpawn.Add(2);
                EnemiesToSpawn.Add(2);
                break;

                case 6:
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                break;

                case 7:
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(1);
                break;

                case 8:
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                break;

                case 9:
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(0);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(1);
                EnemiesToSpawn.Add(2);
                EnemiesToSpawn.Add(2);
                break;



            }
            return;
        }



       int r = UnityEngine.Random.Range(0, 4);
       

       switch(r)
       {
           case 0:
            EnemiesToSpawn.Add(0);
            EnemiesToSpawn.Add(0);
            EnemiesToSpawn.Add(0);
            EnemiesToSpawn.Add(0);
            EnemiesToSpawn.Add(1);
            EnemiesToSpawn.Add(1);           
           break;


           case 1:
           EnemiesToSpawn.Add(0);
           EnemiesToSpawn.Add(0);
           EnemiesToSpawn.Add(0);
           EnemiesToSpawn.Add(2);
           EnemiesToSpawn.Add(2);
           EnemiesToSpawn.Add(2);
           break;



           case 2:
            EnemiesToSpawn.Add(1);
            EnemiesToSpawn.Add(1);
            EnemiesToSpawn.Add(1);
            EnemiesToSpawn.Add(1);
            EnemiesToSpawn.Add(2);
            EnemiesToSpawn.Add(2);
           break;


           case 3:
            EnemiesToSpawn.Add(0);
            EnemiesToSpawn.Add(0);
            EnemiesToSpawn.Add(1);
            EnemiesToSpawn.Add(1);
            EnemiesToSpawn.Add(1);
            EnemiesToSpawn.Add(2);
            EnemiesToSpawn.Add(2);
           break;
             



       } 
    }


    void Spawner(){
        RandomWave();
            for(int i = 0; i < EnemiesToSpawn.Count; i++){

                int r = UnityEngine.Random.Range(0, spawnpoints.Length);
                GameObject Spawn = spawnpoints[r];

                if (!spawnobueno.Contains(Spawn)) {
                spawnobueno.Add(Spawn);
                

            if (EnemiesToSpawn[i] == 0) {
               GameObject NewEnemy = Instantiate(PrefabNormal0, Spawn.transform.position, Quaternion.identity);
                NewEnemy.GetComponent<GetHitScript>().WaveManager = this.gameObject;
                NewEnemy.GetComponent<Pathfinding.AIDestinationSetter>().target = player.transform;
                if (StaticGameSaver.Playercount == 2 ){
                NewEnemy.GetComponent<GetHitScript>().Player1 = player;
                NewEnemy.GetComponent<GetHitScript>().Player2 = Player2;
                 }
                Alive.Add(NewEnemy);

            } else if (EnemiesToSpawn[i] == 1){
                GameObject NewEnemy = Instantiate(PrefabTwohit1, Spawn.transform.position, Quaternion.identity);
                NewEnemy.GetComponent<GetHitScript>().WaveManager = this.gameObject;
                NewEnemy.GetComponent<Pathfinding.AIDestinationSetter>().target = player.transform;
                if (StaticGameSaver.Playercount == 2 ){
                NewEnemy.GetComponent<GetHitScript>().Player1 = player;
                NewEnemy.GetComponent<GetHitScript>().Player2 = Player2;
                 }
                Alive.Add(NewEnemy);

            } else if (EnemiesToSpawn[i] == 2){
                GameObject NewEnemy = Instantiate(PrefabM, Spawn.transform.position, Quaternion.identity);
                NewEnemy.GetComponent<GetHitScript>().WaveManager = this.gameObject;
                NewEnemy.GetComponent<Pathfinding.AIDestinationSetter>().target = player.transform;
                if (StaticGameSaver.Playercount == 2 ){
                NewEnemy.GetComponent<GetHitScript>().Player1 = player;
                NewEnemy.GetComponent<GetHitScript>().Player2 = Player2;
                 }
                Alive.Add(NewEnemy);
            }


            } else if (spawnobueno.Contains(Spawn)) i--;

         }
         
         EnemiesToSpawn.Clear();
         spawnobueno.Clear();
         Wavec++;
         addRound();
         if (StaticGameSaver.Playercount == 2){
             Unfreeze();
         }

         StartTimer();
         
        
         }
            



         void StartTimer(){
             timer.Start();

         }

        void GetTimerData(bool withTimeRecording){
            if (Wavec != 0) {
            timer.Stop();
           
           if(!withTimeRecording) {
               if (FastestTime == -1){ FastestTime = 999999999.99; Totaltime = timer.Elapsed.TotalSeconds; Rondetijd = timer.Elapsed.TotalSeconds; return;}
               
               Rondetijd = timer.Elapsed.TotalSeconds - Totaltime; Totaltime = timer.Elapsed.TotalSeconds; return;
           }


            if (FastestTime == -1) {FastestTime = timer.Elapsed.TotalSeconds; FastestWave = Wavec; Totaltime = timer.Elapsed.TotalSeconds; Rondetijd = timer.Elapsed.TotalSeconds;
            viewedTime = System.Math.Round(FastestTime,2);
            updateFastestround(viewedTime);
            Timetext.text = "Fastest Wave: " + FastestWave + "\nTime Wave "+ Wavec + ": " + viewedTime + "s"; return;} 
           
           
            Rondetijd = timer.Elapsed.TotalSeconds - Totaltime;
           
           
           
           
           if ((Rondetijd) < FastestTime){
                Totaltime = timer.Elapsed.TotalSeconds;
                FastestTime = Rondetijd; FastestWave = Wavec;
                viewedTime = System.Math.Round(FastestTime,2);
                updateFastestround(viewedTime);
                Timetext.text = "Fastest Wave: " + FastestWave + "\nTime Wave "+ Wavec + ": " + viewedTime + "s"; return; 
                 
            }
            Totaltime = timer.Elapsed.TotalSeconds;
            }
        }

        void updateFastestround(double viewedTimes){
                if(PlayerPrefs.GetString("fastestround", "N/A") != "N/A"){
                    string playerprefstring = PlayerPrefs.GetString("fastestround", "N/A").Substring(0, PlayerPrefs.GetString("fastestround", "N/A").Length - 1);
                    double.TryParse(playerprefstring, out double playerprefdouble);
                    if(viewedTimes < playerprefdouble) {
                        PlayerPrefs.SetString("fastestround", viewedTimes + "s");
                    }

                } else PlayerPrefs.SetString("fastestround", viewedTimes + "s");
        }

        void addRound() {
        if(PlayerPrefs.GetString("totalrounds", "N/A") != "N/A"){
            string playerprefstring = PlayerPrefs.GetString("totalrounds", "N/A");
            int.TryParse(playerprefstring, out int playerprefint);

            PlayerPrefs.SetString("totalrounds", playerprefint + 1 + "");

            } else PlayerPrefs.SetString("totalrounds", "1");
        }


        public void EndRoundOfDead(){
            for(int i = 0; i < Alive.Count; i++){
            Destroy(Alive[i]);  
        }
        Alive.Clear();
        GameObject[] gemdestroy = GameObject.FindGameObjectsWithTag("Drop");
            for(int i = 0; i < gemdestroy.Length; i++){
                
            Destroy(gemdestroy[i]);  
            
        }
        
        spawnobueno.Add(NoNoSpawn);
        player.transform.position = new Vector3(-2.5f,0, player.transform.position.z);
        if(StaticGameSaver.Playercount == 2)
        Player2.transform.position = new Vector3(2.5f,0, player.transform.position.z);
        gemdestroy = null;
        GetTimerData(false);
        Spawner();

        }

        public void EndSession(){
            SceneManager.LoadScene(2);
            Wavec--;
            Totaltime = timer.Elapsed.TotalSeconds;
            double TotalTimedouble = System.Math.Round(Totaltime,2);
            StaticGameSaver.totaltimed = TotalTimedouble;
            StaticGameSaver.fastestTime = viewedTime;
            StaticGameSaver.wave = Wavec;
            StaticGameSaver.fastestwave= FastestWave;
            SceneManager.LoadScene(2);

        }


        public void Unfreeze(){
            player.GetComponent<PlayerControl>().POP(false);

            Player2.GetComponent<PlayerControl>().POP(false);
        }
}

 


