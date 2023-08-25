using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // Unpause the game by setting time scale to 1
        pauseMenuCanvas.SetActive(false);
    }

    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Pause the game by setting time scale to 0
        pauseMenuCanvas.SetActive(true);
    }
}
