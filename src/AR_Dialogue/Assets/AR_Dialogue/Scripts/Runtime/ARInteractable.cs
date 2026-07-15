
public interface ARInteractable
{
    public bool IsBeignInteracted {get; set;}
    public void OnEnterInteraction();
    public void Interact();
    public void OnExitInteraction();
}
