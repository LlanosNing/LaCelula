using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shootCooldown;
    
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("FireNormal")) && !PauseMenu.isPaused)
            Shoot();
    }

    void Shoot()
    {
        AudioManager.audioMReference.PlaySFX(5);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
