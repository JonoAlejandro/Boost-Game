using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRotate : MonoBehaviour
{
    [SerializeField] Vector3 rockRotation;

    // Update is called once per frame
    void Update()
    {   
        transform.Rotate(rockRotation * Time.deltaTime);
    }
}
