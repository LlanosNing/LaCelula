using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePlayer : MonoBehaviour
{
    public float slimePlayerTime;

    private LevelManager _lMReference;
    private PlayerController _pCReference;
    private UIController _uIReference;

    public GameObject slimePlayer;
    
   
    void Start()
    {
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _pCReference = GameObject.Find("Player").GetComponent<PlayerController>();
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    void Update()
    {
        if (_lMReference.gemCollected >= 10 || Input.GetKeyDown(KeyCode.H))
        {
            //_pickupReference.ResetADN();
            SlimePlayerCo();
            Debug.Log("Corrutina ejecutada");
        }
    }

    private IEnumerator SlimePlayerCo()
    {
        _lMReference.gemCollected = 0;
        _uIReference.UpdateGemCount();
        //_pCReference.gameObject.SetActive(false);
        Debug.Log("Jugador desactivado");
        yield return new WaitForSeconds(slimePlayerTime);
       // _pCReference.gameObject.SetActive(true);
    }

}
