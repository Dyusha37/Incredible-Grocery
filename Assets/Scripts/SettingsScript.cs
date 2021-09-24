using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject soundsButton;
    [SerializeField] GameObject musicButton;
    [SerializeField] Text soundText;
    [SerializeField] Text musicText;
    [SerializeField] Sprite itemOn;
    [SerializeField] Sprite itemOff;
    [SerializeField] AudioMixerGroup mixer;
    [SerializeField] AudioSource buttonClick;
    PlayerPrefsController playerPrefs;
    bool soundsOn;
    bool musicOn;

    void Start()
    {
        playerPrefs = FindObjectOfType<PlayerPrefsController>();
        soundsOn = playerPrefs.GetSound();
        musicOn = playerPrefs.GetMusic();
        SetMusic();
        SetSound();
    }
    public void ShowSettings()
    {
        buttonClick.Play();
        settingsMenu.SetActive(true);
        SetImage(soundsButton, soundText, soundsOn);
        SetImage(musicButton, musicText, musicOn);
        Time.timeScale = 0;
    }
    public void CloseSetings()
    {
        buttonClick.Play();
        settingsMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void MusicClick()
    {
        buttonClick.Play();
        musicOn = !musicOn;
        playerPrefs.SetMusic(musicOn);
        SetImage(musicButton, musicText,musicOn);
        SetMusic();
    }
    
    public void SoundClick()
    {
        buttonClick.Play();
        soundsOn = !soundsOn;
        playerPrefs.SetSound(soundsOn);
        SetImage(soundsButton, soundText,soundsOn);
        SetSound();
    }
    public void SaveClick()
    {
        buttonClick.Play();
        playerPrefs.SaveSetting();
    }
    private void SetMusic()
    {
        int play;
        play = musicOn ? 0 : -80;
        mixer.audioMixer.SetFloat("MusicVolume", play);
    }
    private void SetSound()
    {
        int play;
        play = soundsOn ? 0 : -80;
        mixer.audioMixer.SetFloat("SoundsVolume", play);
    }
    private void SetImage(GameObject button, Text buttonText,bool param)
    {
        buttonText.text = param ? "On" : "Off";
        button.GetComponent<Image>().sprite = param ? itemOn : itemOff;
    }

}
