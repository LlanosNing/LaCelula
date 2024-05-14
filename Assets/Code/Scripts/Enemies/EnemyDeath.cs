using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private LevelManager _lMReference;
    private UIController _uIReference;

    private void Start()
    {
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
    }
    public void EnemyDeathController()
    {
        AudioManager.audioMReference.PlaySFX(7);
        transform.gameObject.SetActive(false);
        _lMReference.gemCollected++;
        _uIReference.UpdateGemCount();
    }
}
