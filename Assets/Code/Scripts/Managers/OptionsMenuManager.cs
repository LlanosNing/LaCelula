using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionMenu;

    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void ShowMenu()
    {
        Cursor.visible = true;
        optionMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGameFromOM()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        optionMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToOM()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("OptionMenu");
    }
}
