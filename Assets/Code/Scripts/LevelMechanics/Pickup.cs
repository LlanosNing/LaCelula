using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isADN, isATP;

    private bool _isCollected;

    private LevelManager _lMReference;
    private UIController _uIReference;
    private PlayerHealthController _pHReference;

    // Start is called before the first frame update
    void Start()
    {
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
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
                _lMReference.gemCollected++;
                _uIReference.UpdateGemCount();
                _isCollected = true;
                Destroy(gameObject);
            }
        
            if (isATP)
            {
                if (_pHReference.currentHealth != _pHReference.maxHealth)
                {
                    AudioManager.audioMReference.PlaySFX(1);
                    _pHReference.HealPlayer();
                    _isCollected = true;
                    Destroy(gameObject);
                }
            }
        }
    }

}
