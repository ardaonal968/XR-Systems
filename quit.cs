using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{
    public InputActionReference quitAction;

    void OnEnable()
    {
        quitAction.action.Enable();
        quitAction.action.performed += OnQuitButtonPressed;
    }

    void OnDisable()
    {
        //
        quitAction.action.performed -= OnQuitButtonPressed;
    }

    private void OnQuitButtonPressed(InputAction.CallbackContext context)
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}