using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject goos;

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Detiene el tiempo
        isPaused = true;
        goos.transform.position = new Vector2(2000, goos.transform.position.y);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Restaura el tiempo normal
        isPaused = false;
        goos.transform.position = new Vector2(0, goos.transform.position.y);
    }
}
