using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actions : MonoBehaviour
{
    public AudioSource crouchSound, jumpSound, attackSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            crouchSound.enabled = true;
            jumpSound.enabled = false;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            jumpSound.enabled = true;
            crouchSound.enabled = false;
        }
        else if (Input.GetKey(KeyCode.Mouse0))
        {
            attackSound.enabled = true;
            jumpSound.enabled = false;
            crouchSound.enabled = false;
        }
        else
        {
            crouchSound.enabled = false;
            jumpSound.enabled = false;
            attackSound.enabled = false;
        }
    }
}