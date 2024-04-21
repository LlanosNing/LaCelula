using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //Variable para saber si este objeto es una gema o una cura
    public bool isADN, isATP;

    //Variable para conocer si un objeto ya ha sido recogido
    private bool _isCollected;

     //Referencia al LevelManager
    private LevelManager _lMReference;
    //Referencian al UIController
    private UIController _uIReference;
    //Referencia al PlayerHealthController
    private PlayerHealthController _pHReference;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al LevelManager
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        //Inicializamos la referencia al UIController
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        //Inicializamos la referencia al PlayerHealthController
        _pHReference = GameObject.Find("Player").GetComponent<PlayerHealthController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona y el objeto no había sido recogido
        if (collision.CompareTag("Player") && !_isCollected)
        {
            //Si el objeto en este caso es una gema
            if (isADN)
            {
                AudioManager.audioMReference.PlaySFX(0);
                //Sumo uno al contador de gemas
                _lMReference.gemCollected++;
                //Actualizamos el contador en la UI
                _uIReference.UpdateGemCount();
                //Le decimos que el objeto ha sido recogido
                _isCollected = true;
                //Destruimos el objeto
                Destroy(gameObject);
            }
            //Si el objeto en este caso es una cura
            if (isATP)
            {
                AudioManager.audioMReference.PlaySFX(1);
                //Si el jugador no tiene la vida al máximo
                if (_pHReference.currentHealth != _pHReference.maxHealth)
                {
                    //Hacemos el método que cura la vida del jugador
                    _pHReference.HealPlayer();
                    //Le decimos que el objeto ha sido recogido
                    _isCollected = true;
                    //Destruimos el objeto
                    Destroy(gameObject);
                }
            }
        }
    }
}
