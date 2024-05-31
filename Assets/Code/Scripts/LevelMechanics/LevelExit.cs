using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public bool isBossBattle;

    private LevelManager _lMReference;
    private FadeScreen _fS;

    private void Awake()
    {
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _fS = GameObject.Find("FadeScreen").GetComponent<FadeScreen>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el que entra es el jugador
        if (collision.CompareTag("Player") && !isBossBattle)
        {
            UnlockNewLevel();
            _fS.FadeToBlack();
            _lMReference.ExitLevel();
        }
        else
        {
            _fS.FadeToWhite();
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
