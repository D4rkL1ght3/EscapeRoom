using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Starting game...");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
