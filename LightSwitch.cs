
using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public InputActionReference lightAction;
    private Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void OnEnable()
    {
        if (lightAction != null)
        {
            lightAction.action.Enable();
            lightAction.action.performed += ChangeLightColor;
        }
    }

    void OnDisable()
    {
        if (lightAction != null)
        {
            lightAction.action.performed -= ChangeLightColor;
        }
    }

    private void ChangeLightColor(InputAction.CallbackContext context)
    {
        myLight.color = Color.red;
    }
}