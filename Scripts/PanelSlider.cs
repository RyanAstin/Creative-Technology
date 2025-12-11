using UnityEngine;

public class PanelSlider : MonoBehaviour, IInteractable
{
    public Transform handle;
    public float minX = -0.5f;
    public float maxX = 0.5f;
    public float smoothSpeed = 10f;

    [Header("Drag Settings")]
    [SerializeField] float dragSensitivity = 0.05f;

    [Header("Glow")]
    public Renderer handleRenderer;
    public Color glowColor = Color.cyan;           // Color when slider is being used
    public Color idleEmissionColor = Color.black;  // Color when slider is idle (dark)

    Color baseColor;

    private bool isHeld = false; 
    private Camera cam;
    private float targetX;

    void Start()
    {
        cam = Camera.main;

        if (handleRenderer != null)
        {
            handleRenderer.material.EnableKeyword("_EMISSION");

            // Start slider with black/no glow
            baseColor = idleEmissionColor;
            handleRenderer.material.SetColor("_EmissionColor", idleEmissionColor);
        }

        targetX = handle.localPosition.x;
    }

    void Update()
    {
        if (isHeld)
            DragSlider();

        SmoothMoveHandle();
    }

    public void OnInteract(PlayerInteractor interactor)
    {
        // Toggle dragging state
        isHeld = !isHeld;

        // Switch between glow and idle emission
        EnableGlow(isHeld);
    }

    void DragSlider()
    {
        float mouseDelta = Input.GetAxis("Mouse X");

        // Move opposite direction of mouse for natural feel
        targetX -= mouseDelta * dragSensitivity;

        targetX = Mathf.Clamp(targetX, minX, maxX);
    }

    void SmoothMoveHandle()
    {
        Vector3 pos = handle.localPosition;
        pos.x = Mathf.Lerp(pos.x, targetX, Time.deltaTime * smoothSpeed);
        handle.localPosition = pos;
    }

    void EnableGlow(bool state)
    {
        if (handleRenderer == null) return;

        if (state)
        {
            // Glow when slider is being dragged
            handleRenderer.material.SetColor("_EmissionColor", glowColor * 2f);
        }
        else
        {
            // Idle → black/no emission
            handleRenderer.material.SetColor("_EmissionColor", idleEmissionColor);
        }
    }

    public float GetValue()
    {
        return Mathf.InverseLerp(minX, maxX, targetX);
    }
}
