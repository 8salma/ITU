using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    public GameObject pauseMenu;

    [Header("Movimiento del Personaje")]
    CharacterController characterController;
    public float walkSpeed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravedad = 20.0f;
    private Vector3 move = Vector3.zero;

    public bool bloqueado = false;


    public float velocidad = 5.0f;
    public Vector2 sensibilidad;
    public new Transform camera;
    private bool enSuelo = true; // Variable para verificar si el personaje está en el suelo
    public float saltoFuerza = 8.0f; // Fuerza del salto
    public float manzanasRecolectadas = 0;
    public bool ayuda;
    public bool estaEnDialogo = false;
    public int puntuacionKarma = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // pausar con escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            // hacemos visible el cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            bloqueado = true;
        }

        if (!estaEnDialogo && !bloqueado)
        {
            UpdateMouseLook();
            UpdateMovimiento();
        }
    }

    // Movimiento camara en primera persona
    private void UpdateMouseLook()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        if (hor != 0)
        {
            transform.Rotate(0, hor * sensibilidad.x, 0);
        }

        if (ver != 0)
        {
            Vector3 rotation = camera.localEulerAngles;
            rotation.x = (rotation.x - ver * sensibilidad.y + 360) % 360;

            if (rotation.x > 80 && rotation.x < 180)
            {
                rotation.x = 80;
            }
            if (rotation.x < 280 && rotation.x > 180)
            {
                rotation.x = 280;
            }
            camera.Rotate(-ver * sensibilidad.y, 0, 0);
        }
    }

    private void UpdateMovimiento()
    {
        if (characterController.isGrounded)
        {
            move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move) * walkSpeed;

            // Salto
            if (Input.GetKey(KeyCode.Space))
            {
                move.y = jumpSpeed;
            }
        }

        move.y -= gravedad * Time.deltaTime;
        characterController.Move(move * Time.deltaTime);
    }
    // Detectar cuando el personaje toca el suelo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo")) // Ajusta la etiqueta según tu escenario
        {
            enSuelo = true; // El personaje está en el suelo
        }
    }

    // Detectar cuando el personaje se separa del suelo
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo")) // Ajusta la etiqueta según tu escenario
        {
            enSuelo = false; // El personaje ya no está en el suelo
        }
    }
}
