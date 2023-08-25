using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToLevelSelectScreen : MonoBehaviour
{
    public string LevelSelectScene;
    public void OnButtonClick()
    {
        SceneManager.LoadScene(LevelSelectScene);
    }
}
