using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateBubble : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject instantiatePoint;
    public GameObject Prefab;
    public bool L;
    public float coolDownPeriodInSeconds = 2f;
    private float timeStamp = 0;
    public GameObject HitScoreHandler;
    public Animator Animator;
    
    private GameObject SoundManager;


    void Start(){
        SoundManager = GameObject.FindGameObjectWithTag("Sound");
    }


    
    void Update()
    {
    if (StaticGameSaver.GameisPaused) return;
        if (transform.rotation.y == 0) L = false;
        else if (transform.rotation.y < 0) L = true;
        
    if (GetComponent<PlayerControl>().inbubble) return;


    if (playerNumber == 1){ 
        if (Input.GetKeyDown(KeyCode.F)){
            if (transform.rotation.y == 0){
                SpawnBubble();
                L = false;
            } else if (transform.rotation.y < 0){
                SpawnBubble();
                L = true;
            }
        }
     return;}

    if (playerNumber == 2){ 
        if (Input.GetKeyDown("[9]") || Input.GetKeyDown(KeyCode.RightShift)){

                    if (transform.rotation.y == 0){
                SpawnBubble();
                L = false;
            } else if (transform.rotation.y < 0){
                SpawnBubble();
                L = true;
            }
        }
     return;}



    }

    private void SpawnBubble(){
        if(timeStamp <= Time.time){
        SoundManager.GetComponent<SoundeffectsManager>().playSound("bubbleShot");
        bubbleaddpref();
        Animator.SetTrigger("Shooting 0");
        GameObject newBubble = Instantiate(Prefab, instantiatePoint.transform.position, Quaternion.identity);
        newBubble.GetComponent<Bubble>().HitscoreHandler = HitScoreHandler;
        timeStamp = Time.time + coolDownPeriodInSeconds;
        
        if (L)
        newBubble.GetComponent<Bubble>().speedbubble = -newBubble.GetComponent<Bubble>().speedbubble;
        } else return;
    } 

    public void bubbleaddpref(){
        StaticGameSaver.totalshot = StaticGameSaver.totalshot + 1;
        if(PlayerPrefs.GetString("totalbubbles", "N/A") != "N/A"){
            string playerprefstring = PlayerPrefs.GetString("totalbubbles", "N/A");
            double.TryParse(playerprefstring, out double playerprefdouble);

            PlayerPrefs.SetString("totalbubbles", playerprefdouble + 1 + "");

            } else PlayerPrefs.SetString("totalbubbles", "1");
        }
}





