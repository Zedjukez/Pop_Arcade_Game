using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Score : MonoBehaviour
{
    
    private int points = 0;
    private int displayedPoints = -1;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (displayedPoints != points){
            GetComponent<TMPro.TMP_Text>().text = points.ToString("0000000");
            displayedPoints = points;
        }

    }

    public void addPointsSmoothly(int amount){
        if(StaticGameSaver.Playercount ==2) amount = amount / 2;
        points = points + amount;
        StaticGameSaver.points = points;
    }


    public void ResetPoints(){
        points = 0;
    }


}
