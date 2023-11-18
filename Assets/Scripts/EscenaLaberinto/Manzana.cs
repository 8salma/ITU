using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manzana : MonoBehaviour
{
    bool dentro = false;
    public GameObject puerta;
    public GameObject player;
    public GameObject canvas;
    public TMP_Text contador;
    bool ayuda;
    public AudioSource sonidoPuerta;

    // Start is called before the first frame update
    void Start()
    {
        ayuda = player.GetComponent<Movimiento>().ayuda;
        sonidoPuerta = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && dentro)
        {
            player.GetComponent<Movimiento>().manzanasRecolectadas++;
            UpdateManzanas();
            Destroy(gameObject);
        }

        if (player.GetComponent<Movimiento>().manzanasRecolectadas == 6 && ayuda)
        {
            sonidoPuerta.Play();
            Debug.Log("Papaya");
            Destroy(puerta);
            canvas.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("dentro");
            dentro = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dentro = false;
        }
    }

    private void UpdateManzanas()
    {
        //GameObject go = GameObject.FindGameObjectWithTag("UI");
        Debug.Log("Papaya");
        contador.text = "Manzanas: " + player.GetComponent<Movimiento>().manzanasRecolectadas;
    }
}