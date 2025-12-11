using UnityEngine;

public class Target : MonoBehaviour
{
    public int points = 1;                  
    public GameObject hitParticlePrefab;    
    // Particle effect played when the target is hit.

    private Renderer rend;
    private Color baseColor;

    void Start()
    {
        rend = GetComponent<Renderer>();

        ApplyRandomColor(); // Give neon colour
        ApplyRandomSize();  // Random target scale
    }

    public void HitTarget()
    {
        // Add points to score
        ScoreManager.instance.AddScore(points);

        // Play hit particle if assigned
        if (hitParticlePrefab != null)
            Instantiate(hitParticlePrefab, transform.position, Quaternion.identity);

        // Destroy target after hit
        Destroy(gameObject);
    }

    void ApplyRandomColor()
    {
        // Generate random neon colour using HSV (full saturation)
        Color randomNeon = Color.HSVToRGB(Random.value, 1f, 1f);
        baseColor = randomNeon;

        // Apply to material
        rend.material.color = randomNeon;

        // Enable glow emission
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", randomNeon * 2f);
    }

    void ApplyRandomSize()
    {
        // Random size between small and large
        float randomScale = Random.Range(0.7f, 1.5f);
        transform.localScale = Vector3.one * randomScale;
    }

    public void SetHighlight(bool state)
    {
        // Aimed-at targets glow brighter
        if (state)
            rend.material.SetColor("_EmissionColor", baseColor * 4f);
        else
            rend.material.SetColor("_EmissionColor", baseColor * 2f);
    }
}
