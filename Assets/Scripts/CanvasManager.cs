using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("Canva's Panels")]
    public GameObject panelLogin;
    public GameObject panelRegister;

    private void Start()
    {
        ShowPanel("Login");
    }

    public void ShowPanel(string panelName)
    {
        
        panelLogin.SetActive(false);
        panelRegister.SetActive(false);
       
        
        switch (panelName)
        {
            case "Login":
                panelLogin.SetActive(true);
                break;
            case "Register":
                panelRegister.SetActive(true);
                break;
            default:
                Debug.LogWarning("El panel solicitado no existe: " + panelName);
                break;
        }
    }
}
