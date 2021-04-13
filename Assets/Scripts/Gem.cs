using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private float force;
    public float maxVelocity;
    public GameObject Score;
    public Score script;
    public Rigidbody2D rb;
    

    void Awake(){
        GameObject Score = GameObject.Find("ScoreText");
        script = Score.GetComponent<Score>();
    }


    void Start()
    {
       force = Random.Range(-5, 5);
       rb.velocity = new Vector2(force, 2);
    }

    void FixedUpdate(){
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity); 
    }



    void OnTriggerEnter2D(Collider2D co){
        if (co.gameObject.tag == "Player") {
            addGemTotal();

            script.addPointsSmoothly(250);
            Destroy(gameObject);
        }
    }

    void addGemTotal(){
        if(PlayerPrefs.GetString("totalgems", "N/A") != "N/A"){
            string playerprefstring = PlayerPrefs.GetString("totalgems", "N/A");
            int.TryParse(playerprefstring, out int playerprefint);
            StaticGameSaver.gem++;
            PlayerPrefs.SetString("totalgems", playerprefint + 1 + "");

            } else PlayerPrefs.SetString("totalgems", "1");
        }
    

}

