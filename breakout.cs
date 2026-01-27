using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    public InputActionReference teleportAction;
    
    public Transform insideLocation;
    public Transform outsideLocation;

    private bool isOutside = false;

    void OnEnable(){
        teleportAction.action.Enable();
        teleportAction.action.performed += ToggleLocation;}

    void OnDisable(){
        teleportAction.action.performed -= ToggleLocation;}

    private void ToggleLocation(InputAction.CallbackContext context){
        isOutside = !isOutside;

        if (isOutside){
            transform.position = outsideLocation.position;}
        else{
            transform.position = insideLocation.position;}
    }
}