using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Variable de tiempo para la corrutina
    public float waitToRespawn;

    //Variable para guardar el nombre del nivel al queremos ir
    public string levelToLoad;

    //Referencia al PlayerController
    private PlayerController _pCReference;
   //Referencia al UIController
    private UIController _uIReference;
    //Referencia al PlayerHealthController
    private PlayerHealthController _pHReference;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al PlayerController
        _pCReference = GameObject.Find("Player").GetComponent<PlayerController>();
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

    //Corrutina para respawnear al jugador
    private IEnumerator RespawnPlayerCo()
    {
        //Desactivamos al jugador
        _pCReference.gameObject.SetActive(false);
        //Esperamos un tiempo determinado
        yield return new WaitForSeconds(waitToRespawn);
        //Reactivamos al jugador
        _pCReference.gameObject.SetActive(true);
         //Ponemos la vida del jugador al máximo
        _pHReference.currentHealth = _pHReference.maxHealth;
        //Actualizamos la UI
        _uIReference.UpdateHealthDisplay();
    }
}
