using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public void EnemyDeathController()
    {
        transform.gameObject.SetActive(false);
    }
}
