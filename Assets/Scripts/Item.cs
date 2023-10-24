using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite itemIcon;
    public int cantidad;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact(Movimiento player)
    {
        player.AnadirInventario(this);
        Destroy(this.gameObject);
    }
}
