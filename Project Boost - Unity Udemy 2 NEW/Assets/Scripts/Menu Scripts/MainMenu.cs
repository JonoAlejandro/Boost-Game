
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        int levelOne = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(levelOne);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quitting");
        Application.Quit();
    }


}
