using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    public int currentPoint;

    public Transform _platformPosition; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _platformPosition.position = Vector3.MoveTowards(_platformPosition.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

        //Si la plataforma prácticamente ha llegado a su punto de destino
        if (Vector3.Distance(_platformPosition.position, points[currentPoint].position) < 0.01f)
        {
            currentPoint++;
            //Comprobamos si hemos llegado al último punto del array
            if (currentPoint >= points.Length)
                //Reseteamos al primer punto del array
                currentPoint = 0;
        }
    }
}
