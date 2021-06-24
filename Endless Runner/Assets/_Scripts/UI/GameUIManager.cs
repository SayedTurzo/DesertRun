using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("Game Info UI")]
    public Text ScoreText;
    public Text AmmoText;
    public Text LifeText;
    public Text PSpeedText;
    public Text SpeedIncTimeText;
    [Space]
    [Header("Buttons")]
    public Button ShootButton;
    public Text ShootBtnText;
    [Space]
    public int enemyScoreIncrease;
    public int perSecScoreIncrease;

    float scoreSec;

    void Start()
    {
        GlobalData.gameScore = 0;

        scoreSec = 1f;
        GlobalData.enemyScoreIncrease = enemyScoreIncrease;
        GlobalData.timeScoreIncrease = perSecScoreIncrease;
    }

    void Update()
    {
        UpdateAmmoText();
        UpdateOtherTexts();

        IncreaseScorePerSec();
    }

    void IncreaseScorePerSec()
    {
        scoreSec -= Time.deltaTime;
        if(scoreSec <= 0)
        {
            GlobalData.gameScore += GlobalData.timeScoreIncrease;
            scoreSec = 1f;
        }
        
    }

    void UpdateAmmoText()
    {
        AmmoText.text = "Cactus: " + GlobalData.cactusAmmo;
        if(GlobalData.cactusAmmo <= 0)
        {
            ShootBtnText.text = "NO AMMO";
            ShootButton.interactable = false;
        }
        else
        {
            ShootBtnText.text = "SHOOT";
            ShootButton.interactable = true;
        }
    }

    void UpdateOtherTexts()
    {
        ScoreText.text = "Score: " + (int)GlobalData.gameScore; 
        LifeText.text = "Lives: " + GlobalData.playerLives;
        PSpeedText.text = "Player Speed: " + GlobalData.playerSpeed;
        SpeedIncTimeText.text = "Speed Increase in: " + (int)GlobalData.speedChangeIntervalTime + " sec";
    }
}
