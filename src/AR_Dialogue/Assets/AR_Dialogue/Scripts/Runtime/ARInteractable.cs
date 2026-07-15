
public interface ARInteractable
{
    public bool IsWaitingForInput { get; set; }
    public bool IsBeignInteracted {get; set;}
    public void OnEnterInteraction();
    public void Interact();
    public void OnExitInteraction();
}
