using UnityEngine;

public class PanelButton : MonoBehaviour, IInteractable
{
    // Different button types so one script can handle multiple functions
    public enum ButtonType { MusicToggle, LightsToggle, StartGame }
    public ButtonType buttonType;

    // Reference to the main control panel manager
    public ControlPanelManager panel;

    // Renderer so we can change colour and glow
    public Renderer buttonRenderer;

    [Header("Colours")]
    public Color idleColor = Color.white;
    public Color highlightColor = Color.cyan;
    public Color pressedColor = Color.green;

    [Header("Animation")]
    public float pressDepth = 0.05f;    // How far the button moves when pressed
    public float pressSpeed = 8f;       // How fast the animation plays

    private bool isHighlighted = false;
    private Vector3 originalPos;        // Resting position
    private Vector3 pressedPos;         // Pressed-down position
    private bool isAnimating = false;

    void Start()
    {
        // Save original position
        originalPos = transform.localPosition;

        // Calculate how far the button should move down
        pressedPos = originalPos - new Vector3(0, pressDepth, 0);

        // Setup glow emission if renderer exists
        if (buttonRenderer != null)
        {
            buttonRenderer.material.EnableKeyword("_EMISSION");
            buttonRenderer.material.color = idleColor;
            buttonRenderer.material.SetColor("_EmissionColor", idleColor * 1.5f);
        }
    }

    public void OnInteract(PlayerInteractor interactor)
    {
        // Play press animation
        StopAllCoroutines();
        StartCoroutine(PressAnimation());

        // Run the appropriate action based on button type
        switch (buttonType)
        {
            case ButtonType.MusicToggle:
                panel.ToggleMusic();
                break;

            case ButtonType.LightsToggle:
                panel.ToggleLights();
                break;

            case ButtonType.StartGame:
                panel.StartGame();
                break;
        }
    }

    System.Collections.IEnumerator PressAnimation()
    {
        isAnimating = true;

        // Move button DOWN
        float t = 0;
        while (t < 1)
        {
            transform.localPosition = Vector3.Lerp(originalPos, pressedPos, t);
            t += Time.deltaTime * pressSpeed;
            yield return null;
        }

        // Move button BACK UP
        t = 0;
        while (t < 1)
        {
            transform.localPosition = Vector3.Lerp(pressedPos, originalPos, t);
            t += Time.deltaTime * pressSpeed;
            yield return null;
        }

        isAnimating = false;
    }

    public void SetHighlight(bool state)
    {
        // Change colour based on highlight
        isHighlighted = state;
        Color targetColor = state ? highlightColor : idleColor;

        buttonRenderer.material.color = targetColor;
        buttonRenderer.material.SetColor("_EmissionColor", targetColor * 2f);
    }
}
