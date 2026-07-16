using UnityEngine;
using UnityEngine.InputSystem;


public class ARInputHandler : MonoBehaviour , ARInputActions.IVisualNovelActions
{
    public ARInteractable CurrentInteractable { get; private set; }
    private ARInputActions inputActions;
    private void Awake()
    {
        inputActions = new ARInputActions();
        inputActions.Enable();

        inputActions.VisualNovel.Interact.performed += OnInteract;
        inputActions.VisualNovel.AdvanceDialogue.performed += OnAdvanceDialogue;
        inputActions.VisualNovel.MousePosition.performed += OnMousePosition;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(CurrentInteractable == null)
                return;
            CurrentInteractable.OnEnterInteraction();
        }
    }

    public void OnAdvanceDialogue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CurrentInteractable == null)
                return;
            CurrentInteractable.Interact();
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        
        var mousePos = context.ReadValue<Vector2>();
        if (CurrentInteractable != null)
        {
            if (!CurrentInteractable.IsBeignInteracted)
                CurrentInteractable = null;
            else 
                return;
        }
        //Debug.Log(mousePos);
        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out RaycastHit hit))
        {
            CurrentInteractable = hit.collider.GetComponent<ARInteractable>();
            //Debug.Log(CurrentInteractable);
        }
    }
}
