using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatsText : MonoBehaviour
{
    Transform child; 
    private void Start()
    {
        child = gameObject.transform.Find("CheatsToggle");
    }



    public void Activate()
    {
         child.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        child.gameObject.SetActive(false);
    }
}
