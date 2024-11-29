using UnityEngine;

public class PruebaCUbito2 : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject gatos; // podrias hacer un arreglo de gatos
    public int indiceFrecuencia = 10;
    public float umbral = 0.1f;
    public float factorEscala = 5.0f;
    public bool isSelecet;
    public float jumpUmbral = 0.01f;
    bool isGrounded = true;

    private float[] spectrumData = new float[512];

    private void Start()
    {
        indiceFrecuencia = Random.Range(1, 150);
    }

    void Update()
    {
        if(audioSource != null)
        {
            audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

            float valorFrecuencia = spectrumData[indiceFrecuencia];
            
            if(isSelecet && valorFrecuencia > jumpUmbral)
            {
                if (isGrounded)
                {
                    GetComponent<Rigidbody>().AddForce(Vector3.up * Random.Range(4, 8f), ForceMode.Impulse);

                    isGrounded = false;
                }
            }

            if (valorFrecuencia > umbral)
            {
                //Debug.Log("Paso");
                float nuevoTamano = Mathf.Lerp(gatos.transform.localScale.y, factorEscala, Time.deltaTime * 10);
                gatos.transform.localScale = new Vector3(gatos.transform.localScale.x, nuevoTamano, gatos.transform.localScale.z);
            }
            else
            {
                //Debug.Log(valorFrecuencia);
                float nuevoTamano = Mathf.Lerp(gatos.transform.localScale.y, 1.0f, Time.deltaTime * 10);
                gatos.transform.localScale = new Vector3(gatos.transform.localScale.x, nuevoTamano, gatos.transform.localScale.z);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}