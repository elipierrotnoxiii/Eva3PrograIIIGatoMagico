using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLoginManager : MonoBehaviour
{
    [Header("Input Fields (TextMeshPro)")]
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;

    [Header("Feedback (TextMeshPro)")]
    public TMP_Text feedbackText;

    private CanvasManager canvasManager;

    private void Start()
    {
        canvasManager = canvasManager = FindObjectOfType<CanvasManager>();
    }

    public void LoginUser()
    {
        string email = inputEmail.text.Trim();
        string password = inputPassword.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ShowFeedback("Por favor, completa todos los campos.");
            return;
        }

        if (!IsValidEmail(email))
        {
            ShowFeedback("Por favor, ingresa un email válido.");
            return;
        }

       
        var loginRequest = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(loginRequest, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        ShowFeedback("¡Inicio de sesión exitoso! Bienvenido de nuevo.");
        Debug.Log("Usuario logeado correctamente: " + result.PlayFabId);

        Debug.Log(result.PlayFabId);

        canvasManager.ShowPanel("MainMenu");

    }

    private void OnLoginFailure(PlayFabError error)
    {
        string errorMessage = error.ErrorMessage;

        // Manejo de errores comunes
        if (errorMessage.Contains("Invalid email or password"))
        {
            ShowFeedback("El email o la contraseña son incorrectos.");
        }
        else if (errorMessage.Contains("Account not found"))
        {
            ShowFeedback("No existe una cuenta asociada a este email.");
        }
        else
        {
            ShowFeedback("Error al iniciar sesión: " + errorMessage);
        }

        Debug.LogError("Error de PlayFab: " + error.GenerateErrorReport());
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(true); 
            feedbackText.text = message; 
            StopAllCoroutines(); 
            StartCoroutine(HideFeedbackAfterDelay(3f)); 
        }
    }

    private IEnumerator HideFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        feedbackText.gameObject.SetActive(false);
    }

    // Validación de formato de email
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
