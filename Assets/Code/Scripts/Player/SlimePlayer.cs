using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePlayer : MonoBehaviour
{
    public float slimePlayerTime;

    private LevelManager _lMReference;
    private PlayerController _pCReference;
    private UIController _uIReference;
    private PlayerHealthController _pHController;
    private SpriteRenderer _theSR;

    void Start()
    {
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _pCReference = GameObject.Find("Player").GetComponent<PlayerController>();
        _theSR = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
        _pHController = GameObject.Find("Player").GetComponent<PlayerHealthController>();
    }

    void Update()
    {
        if (_lMReference.gemCollected >= 10 || Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(SlimeModeCo());
        }
    }
    public IEnumerator SlimeModeCo()
    {
        _lMReference.gemCollected = 0;
        _uIReference.UpdateGemCount();

        _pCReference.speed = 9.5f;
        _pCReference.jumpingPower = 27f;
        _pHController.MaxHealPlayer();
        _theSR.color = new Color(0.3631742f, 1, 0.2584905f, 1);
        yield return new WaitForSeconds(slimePlayerTime);
        _pCReference.speed = 8f;
        _pCReference.jumpingPower = 16f;
        _theSR.color = new Color(255, 255, 255, 1);
    }
    public void NormalStats()
    {
        _pCReference.speed = 8f;
        _pCReference.jumpingPower = 16f;
        _theSR.color = new Color(255, 255, 255, 1);
    }
}
