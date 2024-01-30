using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InputField : MonoBehaviour
{
    public TMP_InputField inputField;
    private const int maxDigits = 5;
    public PuertaOpen puertaOpenScript; // Asigna el script PuertaOpen en el inspector

    private bool resetOnNextEnter = false;

    void Start()
    {
        // Establecer el tipo de entrada como número
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;

        // Hacer que el InputField esté seleccionado al inicio
        inputField.Select();

        // Agregar un listener para controlar la entrada
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }

    void OnInputValueChanged(string newValue)
    {
        // Limitar la longitud de la entrada a maxDigits
        if (newValue.Length > maxDigits)
        {
            inputField.text = newValue.Substring(0, maxDigits);
        }

        // Garantizar que la entrada sea un número
        if (!string.IsNullOrEmpty(inputField.text) && !int.TryParse(inputField.text, out _))
        {
            inputField.text = "";
        }

        // Verificar si el valor es igual a "11111"
        if (inputField.text == "11111")
        {
            // Activa la puerta solo si el script PuertaOpen está asignado
            if (puertaOpenScript != null)
            {
                puertaOpenScript.OpenDoor();
            }
        }
    }

    void Update()
    {
        if (resetOnNextEnter && inputField.text != "11111")
        {
            // Limpiar el inputField y habilitar la edición nuevamente
            inputField.text = "";
            inputField.interactable = true;
            resetOnNextEnter = false; // Restablecer la bandera
        }

        // Verificar si se ha presionado la tecla "Enter" en el InputField
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            resetOnNextEnter = true; // Establecer la bandera para restablecer después de presionar Enter
            ChangeToAnotherScene();
        }

        // Verificar si se ha presionado la tecla "Escape"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeToAnotherScene();
        }

        // Habilitar la edición cuando se hace clic en el InputField
        if (Input.GetMouseButtonDown(0) && !inputField.isFocused)
        {
            inputField.interactable = true;
            inputField.Select();
            inputField.ActivateInputField();
        }
    }

    private void ChangeToAnotherScene()
    {
    // Cambia al nivel 3 si la puerta está abierta, de lo contrario, al nivel 1
    string sceneToLoad = puertaOpenScript != null && puertaOpenScript.IsDoorOpen() ? "Level3" : "Level1";
    SceneManager.LoadScene(sceneToLoad);
    }
}
