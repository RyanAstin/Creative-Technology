using UnityEngine;
using UnityEngine.UI;

public class MusicControlPanel : MonoBehaviour
{
    public Light[] arenaLights;
    public AudioSource musicSource;

    public Button musicToggleButton;
    public Slider volumeSlider;
    public Button lightsToggleButton;

    private bool lightsOn = true;
    private bool musicOn = true;

    void Start()
    {
        musicToggleButton.onClick.AddListener(ToggleMusic);
        lightsToggleButton.onClick.AddListener(ToggleLights);
        volumeSlider.onValueChanged.AddListener(SetVolume);

        volumeSlider.value = musicSource.volume;
    }

    void ToggleMusic()
    {
        musicOn = !musicOn;
        musicSource.mute = !musicOn;
    }

    void ToggleLights()
    {
        lightsOn = !lightsOn;

        foreach (Light l in arenaLights)
            l.enabled = lightsOn;
    }

    void SetVolume(float v)
    {
        musicSource.volume = v;
    }
}
