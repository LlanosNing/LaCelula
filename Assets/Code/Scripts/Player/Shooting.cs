using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shootCooldown;
    private bool _isShooting;

    public Rigidbody2D _rb;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetButtonDown("FireNormal")) && !PauseMenu.isPaused)
            StartCoroutine(ShootCo());

        _anim.SetBool("isShooting", _isShooting);
    }
    public IEnumerator ShootCo()
    {
        _isShooting = true;
        AudioManager.audioMReference.PlaySFX(5);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(0.1f);

        _isShooting = false;
    }

}
