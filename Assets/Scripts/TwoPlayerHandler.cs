using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerHandler : MonoBehaviour
{
    public GameObject player1;
    public GameObject Player2Prefab;
    


    void Start()
    {
        if (StaticGameSaver.Playercount != 2)
        this.GetComponent<TwoPlayerHandler>().enabled = false;

        if (StaticGameSaver.Playercount == 2) {
            Transform player1pos = player1.gameObject.transform;
            GameObject Player2 = Instantiate(Player2Prefab, new Vector3(player1pos.position.x + 8f, player1pos.position.y+0.1f, player1pos.position.z), Quaternion.identity);
            Player2.GetComponent<instantiateBubble>().HitScoreHandler = this.gameObject;
            Player2.GetComponent<Health>().Wavehandler = this.gameObject;
            Player2.GetComponent<Health>().healthbar =   player1.GetComponent<Health>().healthbar;
            this.GetComponent<WaveHandler>().Player2 = Player2;
            
            Player2.GetComponent<Health>().Otherplayer = player1;
            player1.GetComponent<Health>().Otherplayer = Player2;

        }

    }

}
