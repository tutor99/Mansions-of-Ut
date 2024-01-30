using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPad : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject targetObject;
    public string sceneToLoad = "NombreDeLaSiguienteEscena";

    void Update()
    {
        // Verificar si la tecla E está siendo presionada
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Lanzar un rayo desde la cámara hacia adelante
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Verificar si el rayo impacta con el objeto deseado
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == targetObject)
            {
                // Cambiar a la siguiente escena
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}