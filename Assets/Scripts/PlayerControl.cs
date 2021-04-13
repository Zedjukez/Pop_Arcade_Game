using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public int PlayerNumber = 1;
    public Rigidbody2D rb;
    public float maxVelocity;
    public float speed;
    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public LayerMask Platforms;
    public LayerMask Playerlayer;
    //bool playernotthesame;
    public SpriteRenderer sprite;
    public bool inbubble;
    public Sprite RescueBubble;
    public Sprite PlayerSprite;
    public float movebubblex=2;
    public float movebubbley=2;
    bool hasntReached = true;
    Vector3 random;
    public Animator animator;
    public float lastjump = 0f;


    private GameObject Soundmanager;
    

    public float jumpforce;
    // Start is called before the first frame update
    void Start()
    {
        Soundmanager = GameObject.FindGameObjectWithTag("Sound");
    }

    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        
        Collider2D col2 = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, Platforms);
        //Collider2D col3 = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, Playerlayer);
        //if (col3.gameObject.GetComponent<PlayerControl>().PlayerNumber != PlayerNumber) {playernotthesame = true;} else { playernotthesame = false;}
        if (collider != null  || col2 != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void Update()
    {
        if(StaticGameSaver.GameisPaused) return;
        if(inbubble && hasntReached){
            transform.position = Vector2.MoveTowards(transform.position, random, 1f * Time.deltaTime);
            if (transform.position == random) hasntReached = false;
            return;
        } else if (inbubble){
            animator.SetBool("inPlace", true);
            return;
        }


        CheckIfGrounded();
        Beweeg();
        Jump();
    }



    void FixedUpdate(){

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }
    


    void Beweeg()
    {
        if (PlayerNumber==1){
        float x = Input.GetAxisRaw("Horizontal");

        if(x == 0){
            animator.SetBool("isWalking", false);
        } else {animator.SetBool("isWalking", true); }
        
        
        float beweeg = x * speed;
        rb.velocity = new Vector2(beweeg, rb.velocity.y);
        if (x == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (x == -1)
        {
            transform.rotation = new Quaternion(0, -180, 0, 0);
            
        }
        return;}

        if (PlayerNumber==2){
        float x = Input.GetAxisRaw("Horizontal2");
        if(x == 0){
            animator.SetBool("isWalking", false);
        } else {animator.SetBool("isWalking", true); }
        float beweeg = x * speed;
        rb.velocity = new Vector2(beweeg, rb.velocity.y);
        if (x == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (x == -1)
        {
            transform.rotation = new Quaternion(0, -180, 0, 0);
            
        }
        return;}

}

    void Jump()
    {
        if (PlayerNumber == 1){
            
        if ( Input.GetKeyDown(KeyCode.W) && isGrounded) 
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            //playernotthesame = false;
            isGrounded = false;
            Soundmanager.GetComponent<SoundeffectsManager>().playSound("jump");

        }
        return;}

        if (PlayerNumber == 2){
        if (Input.GetKeyDown(KeyCode.UpArrow)&& isGrounded) 
        {
            isGrounded = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            Soundmanager.GetComponent<SoundeffectsManager>().playSound("jump");
            //playernotthesame = false;
        }
        return;}
    }

    public void rescueBubble(){
        sprite.sprite = RescueBubble;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        gameObject.layer = 16;
        inbubble = true;
        GetComponent<ParticleSystem>().Play();
        Soundmanager.GetComponent<SoundeffectsManager>().playSound("playerHelp");
        animator.SetBool("Bubble", true);
        
        random = new Vector2(Random.Range(-9f, 9f), Random.Range(-4.25f, 4.25f));

        while (Vector2.Distance(transform.position, random) < 10){
        random = new Vector2(Random.Range(-9f, 9f), Random.Range(-4.25f, 4.25f));

        }

    }


    public void POP(bool Sound){
        inbubble = false;
        sprite.sprite = PlayerSprite;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.layer = 11;
        hasntReached = true;
        animator.SetBool("Bubble", false);
        animator.SetBool("inPlace", false);
        GetComponent<ParticleSystem>().Stop();
        if (Sound)
        Soundmanager.GetComponent<SoundeffectsManager>().playSound("bubblePop");
        
    }

      private void OnCollisionEnter2D(Collision2D collision){
          if (inbubble && collision.gameObject.tag == "Player" ){
              POP(true);
          }

         
      }

        void OnTriggerEnter2D(Collider2D co){
        if (co.gameObject.tag == "Drop") {
            Soundmanager.GetComponent<SoundeffectsManager>().playSound("gemPickup");

        }
    }
}

