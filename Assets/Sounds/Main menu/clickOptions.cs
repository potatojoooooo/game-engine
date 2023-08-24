using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickOptions : MonoBehaviour
{
    public AudioSource selectSound, quitSound, backSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSelectSoundEffect()
    {
        selectSound.Play();
    }
    public void playBackSoundEffect()
    {
        backSound.Play();
    }
    public void playQuitSoundEffect()
    {
        quitSound.Play();
    }
}
