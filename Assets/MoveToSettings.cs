using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveToSettings : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Settings");
    }
}