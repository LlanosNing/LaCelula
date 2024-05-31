using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    public bool isDoor1, isDoor2;

    public GameObject zone1, zone2, zone3;
    public GameObject door1, door2, door3;
    private LevelManager _lM;
    private FadeScreen _fS;

    private void Start()
    {
        _lM = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        _fS = GameObject.Find("FadeScreen").GetComponent<FadeScreen>();
    }

    void Update()
    {
        if (_lM.gemCollected == 10)
        {
            AudioManager.audioMReference.PlaySFX(1);
            OpenDoor1();
        }

        if (_lM.gemCollected == 11)
            CloseDoor1();

        if (_lM.gemCollected == 22)
        {
            AudioManager.audioMReference.PlaySFX(1);
            OpenDoor2();
        }
        if (_lM.gemCollected == 23)
            CloseDoor1();

        if (_lM.gemCollected == 28)
        {
            AudioManager.audioMReference.PlaySFX(1);
            OpenDoor3();
        }
            
    }

    public void OpenDoor1()
    {
            door1.SetActive(false);
    }
    public void CloseDoor1()
    {
        door1.SetActive(true);
    }

    public void OpenDoor2()
    {
        door2.SetActive(false);
    }
    public void CloseDoor2()
    {
        door2.SetActive(true);
    }

    public void OpenDoor3()
    {
        door3.SetActive(false);
    }
}
