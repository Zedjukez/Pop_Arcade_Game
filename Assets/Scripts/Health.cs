using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject Wavehandler;
    public int lasthit;
    public GameObject[] healthbar;
    
    public Sprite EatenPOP;
    public GameObject Otherplayer;

    

    void OnCollisionEnter2D(Collision2D c) {
        if (StaticGameSaver.Playercount == 2){
            if(c.gameObject.tag == "Enemy") {
            if(c.gameObject.GetComponent<GetHitScript>().bubbleform) return;
            if (!Otherplayer.GetComponent<PlayerControl>().inbubble){
                this.GetComponent<PlayerControl>().rescueBubble();
                return;
            }
            
            
            }









        }
        if(c.gameObject.tag == "Enemy") {
        if(c.gameObject.GetComponent<GetHitScript>().bubbleform) return;
        if(lasthit == Wavehandler.GetComponent<WaveHandler>().Wavec) return;
            for(int i = healthbar.Length-1; i >= 0; i--){
            
                if(healthbar[i].gameObject.tag != "Heart wasted"){
                    GameObject heart =healthbar[i].gameObject;
                    heart.tag = "Heart wasted";
                    heart.GetComponent<Image>().color = new Color(0.8f,0.8f,0.8f, 0.6f);
                    heart.GetComponent<Image>().sprite = EatenPOP;
                    Wavehandler.GetComponent<WaveHandler>().EndRoundOfDead();
                    if (i == 0){
                        Wavehandler.GetComponent<WaveHandler>().EndSession();
                    }
                    return;
                }

                
            }
        }

    }






}
