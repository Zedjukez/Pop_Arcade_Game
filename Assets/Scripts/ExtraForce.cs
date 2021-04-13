using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraForce : MonoBehaviour
{

void OnTriggerEnter2D(Collider2D co){
    co.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(co.gameObject.GetComponent<Rigidbody2D>().velocity.x, co.gameObject.GetComponent<Rigidbody2D>().velocity.y+0.8f);
}


}
