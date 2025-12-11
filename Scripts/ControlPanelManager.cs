using UnityEngine;

public class ControlPanelManager : MonoBehaviour
{
    [Header("Music Control")]
    public AudioSource musicSource; 
    // The music player in the scene. We adjust volume & mute status.

    [Header("Lights")]
    public Light[] arenaLights; 
    // All lights controlled by the panel. Clicking the button toggles them.

    [Header("Slider")]
    public PanelSlider volumeSlider;  
    // Reference to the physical slider object used to control volume.

    [Header("Game Control")]
    public TargetSpawner spawner;
    public GameTimer timer;
    // The game systems the panel can start/reset.

    void Update()
    {
        // Continuously updates the music volume based on slider position.
        if (volumeSlider != null)
        {
            musicSource.volume = volumeSlider.GetValue();
        }
    }

    public void ToggleMusic()
    {
        // Simple mute toggle for background music.
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleLights()
    {
        // Turn all arena lights on/off at once.
        foreach (Light l in arenaLights)
            l.enabled = !l.enabled;
    }

    public void StartGame()
    {
        // Reset score first, then start timer and spawning.
        ScoreManager.instance.ResetScore();
        timer.StartTimer();
    }
}
