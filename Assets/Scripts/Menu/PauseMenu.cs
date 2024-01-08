using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Continue()
    {
        GameManager.ClosePauseMenu(gameObject);
    }

    public void Settings()
    {
        SettingsMenu.lastScene = 1;
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
