using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class keyboard : MonoBehaviour
{
    
public GameObject ToetsenbordCanvas;

public InputField inputtext;
public GameObject Q;


 void Start(){
     ToetsenbordCanvas.SetActive(false);
 }

public void EnableKeyboard(){
    ToetsenbordCanvas.SetActive(true);
    EventSystem.current.SetSelectedGameObject(Q);

    
}

public void DisableKeyboard(){
    ToetsenbordCanvas.SetActive(false);
}

public void AddKey(){
    string key = EventSystem.current.currentSelectedGameObject.name;
    inputtext.text = inputtext.text + key;
}

void Update(){
    if(ToetsenbordCanvas.activeInHierarchy){
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            if(inputtext.text.Length > 0) {
                inputtext.text = inputtext.text.Substring(0, inputtext.text.Length - 1);
            }
        }
    }
}

}
