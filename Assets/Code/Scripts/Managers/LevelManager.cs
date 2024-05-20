using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Variable de tiempo para la corrutina
    public float waitToRespawn;
    public float enemyRespawn;

    //Variable para guardar el nombre del nivel al queremos ir
    public string levelToLoad;

    //Variable para el contador de gemas
    public int gemCollected;

    private PlayerController _pCReference;
    private CheckpointController _cReference;
    private UIController _uIReference;
    private PlayerHealthController _pHReference;
    private SlimePlayer _sPReference;

    public GameObject[] horizontalEnemies;
    public GameObject[] verticalEnemies;
    public GameObject[] chasingEnemies;
    public GameObject[] adn;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        _pCReference = GameObject.Find("Player").GetComponent<PlayerController>();
        _cReference = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        _pHReference = GameObject.Find("Player").GetComponent<PlayerHealthController>();
        _sPReference = GameObject.Find("Player").GetComponent<SlimePlayer>();
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerCo());
    }

    public void RespawnEnemy()
    {
        StartCoroutine(RespawnHorizontalEnemyCo());
        StartCoroutine(RespawnVerticalEnemyCo());
        StartCoroutine(RespawnChasingEnemyCo());
    }

    private IEnumerator RespawnPlayerCo()
    {
        _pCReference.gameObject.SetActive(false);
        _sPReference.NormalStats();
        yield return new WaitForSeconds(waitToRespawn);
        _pCReference.gameObject.SetActive(true);
        _pCReference.transform.position = _cReference.spawnPoint;
        _pHReference.currentHealth = _pHReference.maxHealth;
        _uIReference.UpdateHealthDisplay();
    }

    private IEnumerator RespawnHorizontalEnemyCo()
    {
        for (int i = 0; i < horizontalEnemies.Length; i++) //poner el nombre del array.Length
        {
            horizontalEnemies[i].SetActive(false);//con la i se recorre todos los elementos del array
        }
        for (int i = 0; i < horizontalEnemies.Length; i++)
        {
            horizontalEnemies[i].SetActive(true);
        }

        yield return new WaitForSeconds(enemyRespawn);
    }
    private IEnumerator RespawnVerticalEnemyCo()
    {
        for (int i = 0; i < verticalEnemies.Length; i++) 
        {
            verticalEnemies[i].SetActive(false);
        }
        for (int i = 0; i < verticalEnemies.Length; i++)
        {
            verticalEnemies[i].SetActive(true);
        }

        yield return new WaitForSeconds(enemyRespawn);
    }
    private IEnumerator RespawnChasingEnemyCo()
    {
        for (int i = 0; i < chasingEnemies.Length; i++)
        {
            chasingEnemies[i].SetActive(false);
        }
        for (int i = 0; i < verticalEnemies.Length; i++)
        {
            chasingEnemies[i].SetActive(true);
        }

        yield return new WaitForSeconds(enemyRespawn);
    }
    private IEnumerator RespawnAdnCo()
    {
        for (int i = 0; i < adn.Length; i++)
        {
            adn[i].SetActive(false);
        }
        for (int i = 0; i < adn.Length; i++)
        {
            adn[i].SetActive(true);
        }

        yield return new WaitForSeconds(1);
    }

    public void ExitLevel()
    {
        StartCoroutine(ExitLevelCo());
    }

    public IEnumerator ExitLevelCo()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(levelToLoad);
    }
}
