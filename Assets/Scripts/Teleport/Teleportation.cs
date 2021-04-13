using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject To;
    public GameObject player;


    
    void Start()
    {
        
    }

   void OnTriggerEnter2D(Collider2D c){

  

       c.transform.position = new Vector3(c.transform.position.x, To.transform.position.y , c.transform.position.z);


   }


}
