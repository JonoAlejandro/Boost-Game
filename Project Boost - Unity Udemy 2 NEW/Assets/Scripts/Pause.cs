using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    [SerializeField] GameObject mainMenu;

    // Components
    Mover moverScript;
    Rigidbody rb;

    //States
    bool pause = false;


    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(false);
        GrabCompenents();
    }

    void GrabCompenents()
    {
        moverScript = GetComponent<Mover>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
        UnpauseGame();
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            Debug.Log("Going to Pause Menu");
            Cursor.visible = true;
            mainMenu.SetActive(true);
            moverScript.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }


    }

    void UnpauseGame() 
    {
        if (Input.GetKeyDown(KeyCode.P) || pause)
        {
            UnpauseSequence();
        }
    }

    public void UnpauseSequence()
    {   
        Debug.Log("here");
        Cursor.visible = false;
        mainMenu.SetActive(false);
        rb.freezeRotation = false;
        moverScript.enabled = true;
        rb.constraints = RigidbodyConstraints.None;
        
    }
}
