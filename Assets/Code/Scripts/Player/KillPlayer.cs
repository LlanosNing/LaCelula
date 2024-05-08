using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private PlayerHealthController _pHReference;
    private UIController _uIReference;
    private LevelManager _lMReference;

    void Start()
    {
        _pHReference = GameObject.Find("Player").GetComponent<PlayerHealthController>();
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _pHReference.currentHealth = 0;
            _uIReference.UpdateHealthDisplay();
            _lMReference.RespawnPlayer();
            _lMReference.RespawnEnemy();
        }
    }
}
