using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToLevelSelect : MonoBehaviour
{
    public string LevelSelectScene;
    public void OnButtonClick()
    {
        SceneManager.LoadScene(LevelSelectScene);
    }
}