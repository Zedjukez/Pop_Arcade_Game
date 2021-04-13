using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{

    void Awake(){
        GameObject[] musicboxes = GameObject.FindGameObjectsWithTag("musicbox");
        if (musicboxes.Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
