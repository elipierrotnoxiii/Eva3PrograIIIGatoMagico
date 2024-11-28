using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanelManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenu; 
    public GameObject programacion;
    public GameObject ciencias;
    public GameObject ingles;

    private void Start()
    {
       
        CloseAllPanels();
        mainMenu.SetActive(true);
    }

    public void OpenPanel(string panelName)
    {
        Debug.Log("Intentando abrir panel: " + panelName);
        CloseAllPanels();

        switch (panelName)
        {
            case "Programacion":
                programacion.SetActive(true);
                Debug.Log("Panel Programación activado.");
                break;
            case "Ciencias":
                ciencias.SetActive(true);
                Debug.Log("Panel Ciencias activado.");
                break;
            case "Ingles":
                ingles.SetActive(true);
                Debug.Log("Panel Inglés activado.");
                break;
            default:
                Debug.LogWarning("El panel solicitado no existe: " + panelName);
                break;
        }
        
        if (mainMenu != null)
        {
            mainMenu.SetActive(false);
        }
    }

    private void CloseAllPanels()
    {
        programacion.SetActive(false);
        ciencias.SetActive(false);
        ingles.SetActive(false);
    }
}
