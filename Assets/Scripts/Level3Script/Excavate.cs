using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Excavate : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject targetObject;
    private bool inZone;
    public TMP_Text uiText;
    public Slider progressBar;
    private float progressIncrementClick = 10f; // Incremento de progreso por clic
    private float progressDecrementIdle = 1f; // Decremento de progreso por inactividad

    // Referencia al script PickUpScript
    public PickUpScript pickUpScript;

    void Start()
    {
        progressBar.value = progressBar.minValue;
    }

    void Update()
    {

        if (inZone)
        {
            // Si el jugador está en la zona, activar el texto UI y la barra de progreso
            if(pickUpScript != null)
            {
                uiText.gameObject.SetActive(true);
            }

            Debug.Log(pickUpScript);
            // Verificar si se presiona el botón izquierdo del ratón
            if (Input.GetMouseButtonDown(0))
            {
                progressBar.gameObject.SetActive(true);
                uiText.gameObject.SetActive(false);
                // Incrementar el progreso de la barra
                IncrementProgress(progressIncrementClick);
            }
            else
            {
                // Decrementar el progreso de la barra por inactividad
                DecrementProgress(progressDecrementIdle);
            }

            // Verificar si el contador de clics alcanza 15
            if (progressBar.value == progressBar.maxValue)
            {
                // Ocultar la barra de progreso
                progressBar.gameObject.SetActive(false);
                

                // Mostrar el mensaje
                Debug.Log("LO SACASTE");

                // Llamar al método para recoger el objeto en el script PickUpScript
                pickUpScript.PickUpObject(targetObject);
            }
        }
        else
        {
            // Si el jugador no está en la zona, desactivar el texto UI y la barra de progreso
            uiText.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(false);
        }
    }

    private void IncrementProgress(float incrementAmount)
    {
        // Incrementar el valor de la barra de progreso
        progressBar.value += incrementAmount;

        // Verificar si la barra de progreso alcanza el máximo
        if (progressBar.value >= progressBar.maxValue)
        {
            // Establecer el valor máximo de la barra de progreso
            progressBar.value = progressBar.maxValue;
        }
    }

    private void DecrementProgress(float decrementAmount)
    {
        // Decrementar el valor de la barra de progreso
        progressBar.value -= decrementAmount;

        // Verificar si la barra de progreso alcanza el mínimo
        if (progressBar.value <= progressBar.minValue)
        {
            // Establecer el valor mínimo de la barra de progreso
            progressBar.value = progressBar.minValue;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inZone = false;
        }
    }
}
