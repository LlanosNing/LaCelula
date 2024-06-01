using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private bool dash, shoot, run;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dash = true;
            shoot = false;
            run = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            shoot = true;
            dash = false;
            run = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            shoot = false;
            dash = false;
            run = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            shoot = false;
            dash = false;
            run = true;
        }

        _anim.SetBool("DashActivator", dash);
        _anim.SetBool("ShootRunning", shoot);
        _anim.SetBool("Run", run);
    }
}
