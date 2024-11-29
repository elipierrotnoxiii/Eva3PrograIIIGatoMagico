using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanelManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenu;
    public GameObject programacion;
    public GameObject ciencias;
    public GameObject ingles;

    private void Start()
    {
        ShowPanel("MainMenu"); 
    }

    public void ShowPanel(string panelName)
    {
        
        mainMenu.SetActive(false);
        programacion.SetActive(false);
        ciencias.SetActive(false);
        ingles.SetActive(false);

        
        switch (panelName)
        {
            case "MainMenu":
                mainMenu.SetActive(true);
                break;
            case "Programacion":
                programacion.SetActive(true);
                break;
            case "Ciencias":
                ciencias.SetActive(true);
                break;
            case "Ingles":
                ingles.SetActive(true);
                break;
            default:
                Debug.LogWarning("El panel solicitado no existe: " + panelName);
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("RaspeGenerator");
    }
}
