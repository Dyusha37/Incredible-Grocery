using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] Text score;
    string scoreText;
    PlayerPrefsController playerPrefsController;
    void Start()
    {
        playerPrefsController = FindObjectOfType<PlayerPrefsController>();
        playerPrefsController.LoadGame();
        scoreText = playerPrefsController.GetMoney().ToString();
        SetScore();
    }
    private void SetScore()
    {
        score.text = "$ "+scoreText;
    }
    public void AddToScore(int add)
    {
        scoreText = playerPrefsController.AddMoney(add).ToString();
        SetScore();
    }
}
