using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    [Header("Laser Settings")]
    public LineRenderer lr;            
    public float laserLength = 50f;    
    public float laserWidth = 0.03f;   
    public float laserDuration = 0.05f;

    [Header("References")]
    public Camera cam;                 
    public Transform laserOrigin;      
    // The physical gun barrel / laser emitter position.

    [Header("Audio")]
    public AudioSource laserAudio;     
    public AudioClip laserSound;       
    // Played every time the player fires.

    private bool laserVisible = false;
    private float timer = 0f;

    void Start()
    {
        // --- Safety checks ---
        if (lr == null)
        {
            Debug.LogError("LaserShooter: LineRenderer not assigned!");
            enabled = false;
            return;
        }

        if (cam == null)
        {
            Debug.LogError("LaserShooter: Camera not assigned!");
            enabled = false;
            return;
        }

        if (laserOrigin == null)
        {
            Debug.LogError("LaserShooter: laserOrigin not assigned!");
            enabled = false;
            return;
        }

        // Setup for the line renderer
        lr.positionCount = 2;
        lr.startWidth = laserWidth;
        lr.endWidth = laserWidth;
        lr.useWorldSpace = true;

        // Create a basic glowing red laser material
        lr.material = new Material(Shader.Find("Unlit/Color"));
        lr.material.color = Color.red;
        lr.material.EnableKeyword("_EMISSION");
        lr.material.SetColor("_EmissionColor", Color.red * 3f);

        lr.enabled = false; // Hidden until player fires
    }

    void Update()
    {
        // Fire laser when left click is pressed
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // Timer used to hide the laser after a short duration
        if (laserVisible)
        {
            timer += Time.deltaTime;
            if (timer >= laserDuration)
            {
                lr.enabled = false;
                laserVisible = false;
            }
        }
    }

    void Shoot()
    {
        // Reset laser visibility timer
        timer = 0f;
        laserVisible = true;

        lr.enabled = true;

        // Laser starts from the physical gun barrel
        Vector3 startPos = laserOrigin.position;

        // End point 50 units forward unless we hit something
        Vector3 endPos = startPos + cam.transform.forward * laserLength;

        // Raycast to detect what the laser hits
        if (Physics.Raycast(startPos, cam.transform.forward, out RaycastHit hit, laserLength))
        {
            endPos = hit.point; // Laser ends at collision point

            // If we hit a target, apply hit logic
            if (hit.collider.TryGetComponent<Target>(out Target target))
            {
                target.HitTarget();
            }
        }

        // Update line renderer positions
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);

        // Debug ray for testing inside editor
        Debug.DrawLine(startPos, endPos, Color.green, 1f);

        // Play firing sound if assigned
        if (laserAudio != null && laserSound != null)
        {
            laserAudio.PlayOneShot(laserSound);
        }
    }
}
