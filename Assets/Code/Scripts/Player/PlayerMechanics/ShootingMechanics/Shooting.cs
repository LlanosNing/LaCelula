using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Bullet _b;


    public float shootCooldown;

    public void Start()
    {
        //_b = GameObject.Find("Bullet").GetComponent<Bullet>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CooldownCo());
           // StartCoroutine(Destro)
        }
    }
    private IEnumerator CooldownCo()
    {
        Shoot();
        yield return new WaitForSeconds(3f);    
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
