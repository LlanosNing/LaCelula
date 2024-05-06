using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    //Líneas del diálogo
    public string[] lines;
    //Para saber si el diálogo se puede activar o no
    private bool canActivate;
    //Sprite de diálogo del NPC
    public Sprite theNpcSprite;

    // Update is called once per frame
    void Update()
    {
        //Si el jugador puede activar el diálogo y presiona el botón de interacción y la caja de diálogo no está activa en la jerarquía
        if (canActivate && Input.GetButtonDown("Fire1") && !DialogManager.instance.dialogBox.activeInHierarchy)
        {
            //Llamamos al método que muestra el diálogo y le pasamos las líneas concretas que contiene este objeto
            DialogManager.instance.ShowDialog(lines, theNpcSprite);
        }
    }

    //Si el jugador entra en la zona de Trigger puede activar el diálogo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            canActivate = true;
    }

    //Si el jugador sale de la zona de Trigger ya no puede activar le diálogo
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            canActivate = false;
    }
}
