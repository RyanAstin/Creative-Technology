// Interface that ensures anything interactable has an OnInteract function.
// PlayerInteractor will call this on objects the player interacts with.
public interface IInteractable
{
    void OnInteract(PlayerInteractor interactor);
}
