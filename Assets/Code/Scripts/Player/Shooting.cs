using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shootCooldown;
    float currentCooldown;
    
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) & !PauseMenu.isPaused)
            Shoot();
    }
    //private IEnumerator ShootingCo()
    //{
    //    AudioManager.audioMReference.PlaySFX(5);
    //    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    //    yield return new WaitForSeconds(shootCooldown);
    //}

    void Shoot()
    {
        AudioManager.audioMReference.PlaySFX(5);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
