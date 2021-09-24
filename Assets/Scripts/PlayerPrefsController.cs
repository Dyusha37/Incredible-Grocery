using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{
    int money;
    bool soundsOn;
    bool musicOn;
    int soundInt;
    int musicInt; 
    private void Awake()
    {
        int numPlayerPrefsControllers = FindObjectsOfType<PlayerPrefsController>().Length;
        if (numPlayerPrefsControllers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void SaveProgres()
    {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
    }
    
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        else
        {
            ResetGame();
        }
    }
    public void ResetGame()
    {
        money = 500;
        SaveProgres();
    }
    public int AddMoney(int add)
    {
        money += add;
        SaveProgres();
        return money;
    }
    public int GetMoney()
    {
        return money;
    }
    public bool GetSound()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            soundsOn = PlayerPrefs.GetInt("Sound") == 1;
        }
        else
        {
            soundsOn = true;
            soundInt = 1;
            PlayerPrefs.SetInt("Sound", soundInt);
        }
        return soundsOn;
    }
    public void SetSound(bool sound)
    {
        soundsOn = sound;
        soundInt = soundsOn ? 1 : 0;
    }
    public bool GetMusic()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            musicOn = PlayerPrefs.GetInt("Music") == 1;
        }
        else
        {
            musicOn = true;
            musicInt = 1;
            PlayerPrefs.SetInt("Music", musicInt);
        }
        return musicOn;
    }
    public void SetMusic(bool music)
    {
        musicOn = music;
        musicInt = musicOn ? 1 : 0;
    }
    public void SaveSetting()
    {
        PlayerPrefs.SetInt("Sound", soundInt);
        PlayerPrefs.SetInt("Music", musicInt);
        PlayerPrefs.Save();
    }


}
