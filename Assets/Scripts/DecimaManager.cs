using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;


public class DecimaManager : MonoBehaviour
{
     [SerializeField] private GameObject rowPrefab; // Prefab de fila con el texto y el botón
    [SerializeField] private Transform tableParent; // El panel donde se mostrarán las filas

    // Cargar las decimas guardadas de PlayFab
    public void LoadDecimas()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null)
            {
                // Filtrar y recorrer todas las claves que comienzan con "Decima"
                foreach (var item in result.Data)
                {
                    if (item.Key.StartsWith("Decima")) // Solo las claves que comienzan con "Decima"
                    {
                        Debug.Log("Clave: " + item.Key + ", Valor: " + item.Value.Value);
                        CreateRow(item.Key, item.Value.Value); // Crear una fila para cada decima
                    }
                }
            }
            else
            {
                Debug.Log("No hay decimas guardadas.");
            }
        }, error =>
        {
            Debug.LogError("Error al recuperar datos de PlayFab: " + error.ErrorMessage);
        });
    }

    // Crear la fila para mostrar cada decima
    void CreateRow(string key, string value)
    {
        GameObject row = Instantiate(rowPrefab, tableParent); // Crear la fila en el Panel

        // Verificar que el prefab se haya instanciado correctamente
        if (row == null)
        {
            Debug.LogError("La fila no se instanció correctamente.");
            return;
        }

        // Configurar el valor de la decima en el TextMeshProUGUI
        TextMeshProUGUI decimaText = row.transform.Find("DecimaText")?.GetComponent<TextMeshProUGUI>();
        if (decimaText == null)
        {
            Debug.LogError("No se encontró el componente 'DecimaText' dentro de la fila.");
            return;
        }

        decimaText.text = value;

        // Obtener el botón "Usar" y asignarle la función de eliminar
        Button useButton = row.transform.Find("UseButton")?.GetComponent<Button>();
        if (useButton == null)
        {
            Debug.LogError("No se encontró el botón 'UseButton' dentro de la fila.");
            return;
        }

        useButton.onClick.AddListener(() => UseDecima(key, row));

    }

    // Función para eliminar la decima
    void UseDecima(string key, GameObject row)
    {
        // Eliminar la decima de PlayFab
        DeleteDecimaFromPlayFab(key);

        // Eliminar la fila de la UI
        Destroy(row);
    }

    // Eliminar la decima de PlayFab
    void DeleteDecimaFromPlayFab(string key)
    {
        var data = new Dictionary<string, string>
        {
            { key, null } // Eliminar la clave de PlayFab
        };

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = data
        },
        result =>
        {
            Debug.Log("Decima eliminada de PlayFab: " + key);
        },
        error =>
        {
            Debug.LogError("Error al eliminar decima de PlayFab: " + error.ErrorMessage);
        });
    }
}
