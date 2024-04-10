using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private LevelManager _lMReference;

    private void Awake()
    {
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el que entra es el jugador
        if (collision.CompareTag("Player"))
        {
            UnlockNewLevel();
            Debug.Log("Finish Level");
            _lMReference.ExitLevel();
        }
    }

    private void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
