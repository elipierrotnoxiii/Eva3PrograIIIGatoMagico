using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System.Text.RegularExpressions;

public class AccountRecovery : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField inputEmail; 

    [Header("Feedback")]
    public TMP_Text feedbackText; 

    private CanvasManager canvasManager;

    private void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>(); 
    }

   
    public void RecoverAccount()
    {
        string email = inputEmail.text.Trim();

       
        if (string.IsNullOrEmpty(email))
        {
            ShowFeedback("Por favor ingresa un correo electrónico.");
            return;
        }

      
        if (!IsValidEmail(email))
        {
            ShowFeedback("Por favor ingresa un email válido.");
            return;
        }

        
        var recoveryRequest = new SendAccountRecoveryEmailRequest()
        {
            Email = email
        };

        
        PlayFabClientAPI.SendAccountRecoveryEmail(recoveryRequest, OnRecoveryEmailSent, OnError);
    }

    
    private void OnRecoveryEmailSent(SendAccountRecoveryEmailResult result)
    {
        ShowFeedback("Correo de recuperación enviado. Revisa tu bandeja de entrada.");
    }

    
    private void OnError(PlayFabError error)
    {
        ShowFeedback("Error al enviar el correo de recuperación: " + error.ErrorMessage);
        Debug.LogError("Error de PlayFab: " + error.GenerateErrorReport());
    }

    
    private void ShowFeedback(string message)
    {
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(true); 
            feedbackText.text = message; 

            
            StartCoroutine(HideFeedbackAfterDelay(3f));
        }
    }

   
    private bool IsValidEmail(string email)
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern); 
    }

   
    private IEnumerator HideFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false); 
        }
    }
}
