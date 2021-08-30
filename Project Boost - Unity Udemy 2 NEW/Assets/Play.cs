using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public AudioSource backGroundMusic;

    bool isPlaying = false;
    // Start is called before the first frame update
    void Update()
    {   
        if (!isPlaying)
        {
            if (backGroundMusic.isPlaying == false)
            {
                backGroundMusic.Play();
            }
            
        }       
    }
    
}
