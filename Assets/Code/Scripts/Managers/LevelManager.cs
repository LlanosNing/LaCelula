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

    //Referencia al PlayerController
    private PlayerController _pCReference;
    //Referencia al CheckpointController
    private CheckpointController _cReference;
    //Referencia al UIController
    private UIController _uIReference;
    //Referencia al PlayerHealthController
    private PlayerHealthController _pHReference;

    public GameObject[] horizontalEnemies;
    public GameObject[] verticalEnemies;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al PlayerController
        _pCReference = GameObject.Find("Player").GetComponent<PlayerController>();
        //Inicializamos la referencia al CheckpointController
        _cReference = GameObject.Find("CheckpointController").GetComponent<CheckpointController>();
        //Inicializamos la referencia al UIController
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        //Inicializamos la referencia al PlayerHealthController
        _pHReference = GameObject.Find("Player").GetComponent<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Método para respawnear al jugador cuando muere
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPlayerCo());
    }

    public void RespawnEnemy()
    {
        StartCoroutine(RespawnHorizontalEnemyCo());
        StartCoroutine(RespawnVerticalEnemyCo());
    }

    private IEnumerator RespawnPlayerCo()
    {
        //Desactivamos al jugador
        _pCReference.gameObject.SetActive(false);
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(waitToRespawn);
        //Reactivamos al jugador
        _pCReference.gameObject.SetActive(true);
        //Lo ponemos en la posición de Respawn
        _pCReference.transform.position = _cReference.spawnPoint;
        //Ponemos la vida del jugador al máximo
        _pHReference.currentHealth = _pHReference.maxHealth;
        //Actualizamos la UI
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
        for (int i = 0; i < horizontalEnemies.Length; i++)
        {
            verticalEnemies[i].SetActive(true);
        }

        yield return new WaitForSeconds(enemyRespawn);
    }

    //Método para terminar un nivel
    public void ExitLevel()
    {
        //Llamamos a la corrutina de salir del nivel
        StartCoroutine(ExitLevelCo());
    }

    //Corrutina de terminar el nivel
    public IEnumerator ExitLevelCo()
    {
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(1.5f);
        //Ir a la pantalla de carga o al selector de niveles
        SceneManager.LoadScene(levelToLoad);
    }
}
