using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesPausa : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void resumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.GetComponent<Movimiento>().bloqueado = false;
    }

    public void exitButton()
    {
        Debug.Log("saliendo...");
        Application.Quit();
    }
}
