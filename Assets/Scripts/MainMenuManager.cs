using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Starting game...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
