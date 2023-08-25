using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void GoBack()
    {
        // Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the previous scene by subtracting 1 from the current index
        int previousSceneIndex = Mathf.Max(0, currentSceneIndex - 1);

        // Load the previous scene
        SceneManager.LoadScene(previousSceneIndex);
    }
}
