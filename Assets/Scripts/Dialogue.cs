using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{


    [SerializeField] private GameObject BotonRespuesta1;
    [SerializeField] private GameObject BotonRespuesta2;
    [SerializeField] private GameObject BotonRespuesta3;
    [SerializeField] private GameObject BotonRespuesta4;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    //[SerializeField] TextMeshProUGUI textResUno;
    //[SerializeField] TextMeshProUGUI textResDos;
    //[SerializeField] TextMeshProUGUI textResTres;

    //[SerializeField] PlantillaDialogo plantilla;
    //[SerializeField] PlantillaDialogo[] arrayPlantillas;


    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool isDialogueStarted;
    private int lineIndex;

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // mostrar cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (!isDialogueStarted)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                StopAllCoroutines();

                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        isDialogueStarted = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            isDialogueStarted = false;
            dialoguePanel.SetActive(false);
            BotonRespuesta1.SetActive(false);
            BotonRespuesta2.SetActive(false);
            BotonRespuesta3.SetActive(false);
            BotonRespuesta4.SetActive(false);
            Time.timeScale = 1f;
            typingTime = 0.05f;
        }
        if (lineIndex == dialogueLines.Length - 2)
        {
            BotonRespuesta1.SetActive(true);
            BotonRespuesta2.SetActive(true);
            BotonRespuesta3.SetActive(true);
            BotonRespuesta4.SetActive(true);
            typingTime = 0f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Se puede iniciar el diálogo");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("No se puede iniciar el diálogo");
        }
    }

    public void acertado()
    {
        // hacemos invisible el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Clickado");
        NextDialogueLine();

        BotonRespuesta1.SetActive(false);
        BotonRespuesta2.SetActive(false);
        BotonRespuesta3.SetActive(false);
        BotonRespuesta4.SetActive(false);

        typingTime = 0.05f;
    }
}