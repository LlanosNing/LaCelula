using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMenuManager : MonoBehaviour
{
    public GameObject keyboardMenu, controllerMenu;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        keyboardMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isPaused)
            {
                ResumeFromKM();
            }
            else
            {
                ShowKeyboardMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isPaused)
            {
                ResumeFromCM();
            }
            else
            {
                ShowControllerMenu();
            }
        }
    }

    public void ShowKeyboardMenu()
    {
        Cursor.visible = true;
        keyboardMenu.SetActive(true);
        controllerMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeFromKM()
    {
        Cursor.visible = false;
        keyboardMenu.SetActive(false);
        controllerMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void ShowControllerMenu()
    {
        Cursor.visible = true;
        controllerMenu.SetActive(true);
        keyboardMenu.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeFromCM()
    {
        Cursor.visible = false;
        controllerMenu.SetActive(false);
        keyboardMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
