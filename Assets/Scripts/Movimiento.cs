using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 5.0f;
    public Vector2 sensibilidad;
    public new Transform camera;
    private List<Item> inventario = new List<Item>();
    private List<GameObject> uiInventario = new List<GameObject>();
    public GameObject itemIconPrefab;
    public Transform contenidoInventario;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
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

        UpdateMouseLook();
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

    public void AnadirInventario(Item item)
    {
        inventario.Add(item);
        GameObject go = Instantiate(itemIconPrefab, contenidoInventario);
        Image im = go.GetComponent<Image>();
        im.sprite = item.itemIcon;
        uiInventario.Add(go);
    }
}
