using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public void EnemyDeathController()
    {
        AudioManager.audioMReference.PlaySFX(7);
        transform.gameObject.SetActive(false);
    }
}
