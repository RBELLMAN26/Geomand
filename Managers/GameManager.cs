using UnityEngine;
using System;


//Handles Score, Total Enemies Killed,  How long you played, High Scores Of Everyone
public class GameManager : MonoBehaviour
{
    public GameObject[] totalEnemies;
    public GameObject enemyHealthText;
    public float timeInMatch;
    public string convertedTime;
    public double score;
    public double bestScore = 0;
    public double destroyedEnemies = 0;
    public double destroyedEnemiesInMatch = 0;
    public float bestTime = 0.0f;
    public double costToRevive = 500;
    //public bool isPaused = true;
    public int pauseStatus = 0;
    public GameObject touchedObjects;
    public GameObject nodeObj;
    public TowerManager tM;
    public int difficulty;
    public bool audioMuted;
    public bool gameStarted;
    public bool[] finishedUpgrades = new bool[5];
    public bool endlessMode;
    public int fps;
    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.timeScale = 0;
        if(PlayerPrefs.GetInt("Muted") == 1)
        {
            audioMuted = true;
        }
        else
        {
            audioMuted = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        fps = (int)(1f / Time.unscaledDeltaTime);
        SetDifficulty();
        timeInMatch += Time.deltaTime;
        var tS = TimeSpan.FromSeconds(timeInMatch);
        convertedTime = string.Format("{0:00}:{1:00}", tS.Minutes, tS.Seconds);
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(!endlessMode)
        {
            for (int i = 0; i < finishedUpgrades.Length; i++)
            {
                if (!finishedUpgrades[i])
                {
                    break;
                }
                if (i == finishedUpgrades.Length - 1)
                {
                    endlessMode = true;
                }
            }
        } 
    }
    public void UpdateScore(int points)
    {
        score += points;
        destroyedEnemiesInMatch += 1;
    }

    public void GameOver()
    {
        tM.DisableSounds();
        GameObject.FindObjectOfType<PlayerGUI>().LaunchContinue();
        if(audioMuted)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }
        PlayerPrefs.Save();
    }
    public void Revive(bool isAd)
    {
        tM.Revive();
        nodeObj.SetActive(true);
        nodeObj.GetComponent<NodeInfo>().Revive();
        if(isAd)
        {
            nodeObj.GetComponent<NodeInfo>().energy = 100;
            nodeObj.GetComponent<NodeInfo>().rechargeHealth = 0;
            nodeObj.GetComponent<NodeInfo>().rechargeMulti = 0;
            nodeObj.GetComponent<NodeInfo>().rechargePause = 0;
            nodeObj.GetComponent<NodeInfo>().rechargeShield = 0;
            nodeObj.GetComponent<NodeInfo>().rechargeWipe = 0;
        }
        GameObject.FindObjectOfType<PlayerGUI>().SetRevive();
        GameObject.FindObjectOfType<BaseInfo>().Revive(isAd);


        foreach (MiniBlock obj in FindObjectsOfType<MiniBlock>())
        {
            Destroy(obj.gameObject);
        }
        foreach (EnemyController obj in FindObjectsOfType<EnemyController>())
        {
            Destroy(obj.gameObject);
        }
        foreach(EnemyBulletController obj in FindObjectsOfType<EnemyBulletController>())
        {
            Destroy(obj.gameObject);
        }
        foreach (RocketController obj in FindObjectsOfType<RocketController>())
        {
            Destroy(obj.gameObject);
        }
        foreach (HeartAuraController obj in FindObjectsOfType<HeartAuraController>())
        {
            Destroy(obj.gameObject);
        }
        foreach (Shield obj in FindObjectsOfType<Shield>())
        {
            Destroy(obj.gameObject);
        }
        foreach (BatteryController obj in FindObjectsOfType<BatteryController>())
        {
            Destroy(obj.gameObject);
        }
        foreach (BatteryOrb obj in FindObjectsOfType<BatteryOrb>())
        {
            Destroy(obj.gameObject);
        }
    }

    void SetDifficulty()
    {
        //This calculates every difficulty will be added at 1 minute
        if (timeInMatch > difficulty * 45 + 45)
        {
            difficulty += 1;
            //costToRevive += 250 * (difficulty);
        }
    }
}
