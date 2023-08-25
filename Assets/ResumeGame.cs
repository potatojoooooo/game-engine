using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public GameObject pauseMenuCanvas; // Reference to your pause menu canvas

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Unpause the game by setting time scale to 1
        pauseMenuCanvas.SetActive(false); // Hide the pause menu canvas
    }
}
