using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
