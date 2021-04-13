using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    public float speedbubble = 0.1f;
    public GameObject HitscoreHandler;
    
    
 
 
     void Start ()
     {

     }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(speedbubble * Time.deltaTime, 0, 0);
    }


    private void OnCollisionEnter2D(Collision2D col){

       if (col.gameObject.tag == "Blok") {
        GetComponent<ParticleSystem>().Stop();
        HitscoreHandler.GetComponent<HitscoreHandler>().hitsMis();
        Destroy(gameObject, 1f);
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
        if (col.gameObject.tag == "Enemy") {
            if(col.gameObject.GetComponent<GetHitScript>().bubbleform == false)
            HitscoreHandler.GetComponent<HitscoreHandler>().hitshitted();
         }
    }
}

