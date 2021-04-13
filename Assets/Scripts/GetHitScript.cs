using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetHitScript : MonoBehaviour
{
    public bool bubbleform = false;
    public int AmountOfPoints;
    public GameObject Prefab;
    public GameObject WaveManager;
    public Sprite newSprite;
    public int hitsneeded = 1;
    public GameObject Player1;
    private float distancetoPlayer1;
    public GameObject Player2;
    private float distancetoPlayer2;
    public Animator animator;
    private GameObject Soundmanager;
    public GameObject rendererobject;

    // Start is called before the first frame update

    void Start(){
        Soundmanager = GameObject.FindGameObjectWithTag("Sound");
    }


    void FixedUpdate(){
    if (!bubbleform) 
{        if (this.GetComponent<Pathfinding.AIBase>().desiredVelocity.x < 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (this.GetComponent<Pathfinding.AIBase>().desiredVelocity.x >= 0 )
        {
            transform.rotation = new Quaternion(0, -180, 0, 0);
            
         }
          
        if (StaticGameSaver.Playercount ==2){
            if(Player1.GetComponent<PlayerControl>().inbubble || Player2.GetComponent<PlayerControl>().inbubble){
                if(Player1.GetComponent<PlayerControl>().inbubble) {this.GetComponent<Pathfinding.AIDestinationSetter>().target = Player2.transform; return;}
                if(Player2.GetComponent<PlayerControl>().inbubble) {this.GetComponent<Pathfinding.AIDestinationSetter>().target = Player1.transform; return;}
            }





            distancetoPlayer1 = Vector2.Distance(transform.position, Player1.transform.position);
            distancetoPlayer2 = Vector2.Distance(transform.position, Player2.transform.position);
            if (distancetoPlayer1 < distancetoPlayer2){
                this.GetComponent<Pathfinding.AIDestinationSetter>().target = Player1.transform;
            } else this.GetComponent<Pathfinding.AIDestinationSetter>().target = Player2.transform;
        }

          
          
        }
    }

   private void OnCollisionEnter2D(Collision2D collision){
        
        if (collision.gameObject.tag == "Bubble"){
            if (bubbleform) return;
            if (hitsneeded > 1){
                
                hitsneeded = hitsneeded - 1;
                rendererobject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                animator.SetTrigger("1HitLeft");
                Destroy(collision.gameObject);
                return;
            }
            animator.SetTrigger("Isbubble");
            GetComponent<Rigidbody2D>().constraints =  RigidbodyConstraints2D.FreezePositionY;
            rendererobject.GetComponent<SpriteRenderer>().sprite = newSprite;
            Soundmanager.GetComponent<SoundeffectsManager>().playSound("enemyHit");
            GetComponent<ParticleSystem>().Play();
            rendererobject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            
            gameObject.layer = 16;
            Destroy(collision.gameObject);
            move();
        }

        if (bubbleform && collision.gameObject.tag != "Bubble" && collision.gameObject.tag != "Drop" && collision.gameObject.tag != "Enemy" && collision.gameObject.layer != 14){
            Soundmanager.GetComponent<SoundeffectsManager>().playSound("bubblePop");
            GameObject NewGem = Instantiate(Prefab, transform.position, Quaternion.identity);
            GetComponent<ParticleSystem>().Stop();
            WaveManager.GetComponent<WaveHandler>().Alive.Remove(this.gameObject);
            Destroy(gameObject);
        }


   }

    public void move(){
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        bubbleform = true;
    }

    void Update(){
        if (bubbleform == true){;

        
        transform.position = transform.position + new Vector3(0.1f * Time.deltaTime, 1f * Time.deltaTime, 0);
        }
    }

}
