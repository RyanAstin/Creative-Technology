using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    // How far the player can interact with things
    public float interactDistance = 5f;

    // Only interact with objects on this layer
    public LayerMask interactMask;

    // The camera used for raycasting
    public Camera cam;

    // Keep track of which target is currently highlighted
    private Target highlightedTarget;

    void Update()
    {
        HandleHighlight(); // Every frame, check if a target should glow

        // Press E to interact with buttons or sliders
        if (Input.GetKeyDown(KeyCode.E))
            TryInteract();
    }

    void HandleHighlight()
    {
        // Shoot a ray from the center of the screen
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        // If the ray hits something
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // Check if it's a target
            if (hit.collider.TryGetComponent<Target>(out Target t))
            {
                // If this is a new target, update highlight
                if (highlightedTarget != t)
                {
                    ClearHighlight();
                    highlightedTarget = t;
                    highlightedTarget.SetHighlight(true); // Make it glow
                }
                return;
            }
        }

        // If nothing is hit or it's not a target, remove highlight
        ClearHighlight();
    }

    void ClearHighlight()
    {
        // Reset highlight on the previously highlighted target
        if (highlightedTarget != null)
        {
            highlightedTarget.SetHighlight(false);
            highlightedTarget = null;
        }
    }

    void TryInteract()
    {
        // Raycast forward again for interactables
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        // Hit only objects on the interactMask
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask))
        {
            // Check if object implements IInteractable
            if (hit.collider.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                // Tell it to run its interaction logic
                interactable.OnInteract(this);
            }
        }
    }
}
