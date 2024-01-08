using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public static bool onEnable = false;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!onEnable)
            {
                OpenPauseMenu(pauseMenu);
            }
            else
            {
                ClosePauseMenu(pauseMenu);
            }
        }
    }

    public static void OpenPauseMenu(GameObject pauseMenu)
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        onEnable = true;
    }

    public static void ClosePauseMenu(GameObject pauseMenu)
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        onEnable = false;
    }
}
