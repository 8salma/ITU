using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 5.0f;

    void Update()
    {
        // Obtén la entrada del teclado
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcula la dirección del movimiento
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, movimientoVertical);

        // Normaliza el vector de movimiento para evitar movimientos diagonales más rápidos
        movimiento.Normalize();

        // Aplica el movimiento al personaje
        transform.Translate(movimiento * velocidad * Time.deltaTime);
    }
}