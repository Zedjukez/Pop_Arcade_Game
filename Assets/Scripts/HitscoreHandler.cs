using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscoreHandler: MonoBehaviour
{
    public int hitsHit=0;
    public int hitsMissed=0;
    private float hitPercentagefloat = 0f;
    public int TotalFired;







    public void hitshitted() {
        hitsHit++;
        hitsHittedPref();
        hitPercentagefloat =  (float)hitsHit / (float)StaticGameSaver.totalshot * 100f;
        double hitpercentagerounded = System.Math.Round(hitPercentagefloat,2);
        StaticGameSaver.totalhit = hitsHit;
        StaticGameSaver.hitaccuracy = hitpercentagerounded;

    }

    public void hitsMis(){
        hitsMissed++;
        hitPercentagefloat =  (float)hitsHit / (float)StaticGameSaver.totalshot * 100f;
        double hitpercentagerounded = System.Math.Round(hitPercentagefloat,2);
        StaticGameSaver.totalmissed = hitsMissed;
        StaticGameSaver.hitaccuracy = hitpercentagerounded;

    }
    public void hitsHittedPref(){
        if(PlayerPrefs.GetString("totalenemies", "N/A") != "N/A"){
            string playerprefstring = PlayerPrefs.GetString("totalenemies", "N/A");
            int.TryParse(playerprefstring, out int playerprefint);

            PlayerPrefs.SetString("totalenemies", playerprefint + 1 + "");

            } else PlayerPrefs.SetString("totalenemies", "1");
        }


}


