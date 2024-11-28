using UnityEngine;
using System.Collections;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System.Text.RegularExpressions;

public class PlayFabRegisterManager : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField inputUserName;
    public TMP_InputField inputEmail;
    public TMP_InputField inputPassword;
    public TMP_InputField inputConfirmPassword;

    [Header("Feedback")]
    public TMP_Text feedbackText;

    private CanvasManager canvasManager;

    private void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
    }


    public void RegisterUser()
    {
        string username = inputUserName.text;
        string email = inputEmail.text.Trim();
        string password = inputPassword.text;
        string confirmPassword = inputConfirmPassword.text;

        if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ShowFeedback("Por favor, completa todos los campos.");
            Debug.Log("Si esta pasando el error");
            return;
        }

        if(!IsValidEmail(email))
        {
            ShowFeedback("Porfavor, ingresa un email válido.");
            return;
        }

        if(password != confirmPassword)
        {
            ShowFeedback("Las contraseñas no coinciden.");
            Debug.Log(password + "Supuesta password");
            Debug.Log(confirmPassword + "SupuestaConfirmPassword");
            return;
        }

        if(password.Length < 6)
        {
            ShowFeedback("La contraseña debe tener al menos 6 caracteres.");
            return;
        }

        var registerRequest = new RegisterPlayFabUserRequest()
        {
            Username = username,
            Email = email,
            Password = password,
            RequireBothUsernameAndEmail = true
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);

    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result) 
    {
        ShowFeedback("!Registro exitoso! Bienvenido, " + result.Username);
        Debug.Log("Userio registrado correctamente: " + result.Username);

        canvasManager.ShowPanel("Login");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        ShowFeedback("Error al registrar: " + error.ErrorMessage);
        Debug.LogError("Error de Playfab: " + error.GenerateErrorReport());
    }

    private void ShowFeedback(string message)
    {
        if(feedbackText != null)
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
