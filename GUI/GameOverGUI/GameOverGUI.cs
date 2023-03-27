using GooglePlayGames;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.SocialPlatforms;

public class GameOverGUI : MonoBehaviour
{
    GameManager gM;
    public double score, enemies;
    public float time;
    private string mStatus;
    public GameObject leaderboardOBJ, SignedOutOBJ;
    public LeaderboardGUI leaderboard;
    public TextMeshProUGUI surviveText, scoreText;
    
    //Activate the hiscorescripts when GameOverGUI is enabled
    private void OnEnable()
    {
        gM = FindObjectOfType<GameManager>();
        //var ts = TimeSpan.FromSeconds(gM.timeInMatch);
        surviveText.text = string.Format("Survived {0} at level {1}", gM.convertedTime, gM.difficulty);
        scoreText.text = string.Format("Destroyed {0} enemies", enemies);
        if(PlayGamesPlatform.Instance.IsAuthenticated())
        {
            UpdateStats();
        }
        else
        {
            if(!SignedOutOBJ.activeSelf)
            {
                leaderboardOBJ.SetActive(false);
                SignedOutOBJ.SetActive(true);
                UpdateLocalStats();
            }
        }
        
    }
    void UpdateLocalStats()
    {
        /*
        if(gM.bestScore < score)
        {
            gM.bestScore = score;
            PlayerPrefs.SetInt("Score", (int)gM.bestScore);
        }
        if(gM.bestTime < gM.timeInMatch)
        {
            gM.bestTime = gM.timeInMatch;
            PlayerPrefs.SetFloat("Time", gM.bestTime);
        }
        */
        if(PlayerPrefs.HasKey("Enemies Destroyed"))
        {
            if (gM.destroyedEnemies > PlayerPrefs.GetInt("Enemies Destroyed"))
            {
                PlayerPrefs.SetInt("EnemiesDestroyed", (int)gM.destroyedEnemies);
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetInt("EnemiesDestroyed", (int)gM.destroyedEnemies);
            PlayerPrefs.Save();
        }
        
    }

    void UpdateStats()
    {
        GetComponent<Achievements>().CheckAchievements((int)score, time, (int)enemies);
        /*
        PlayGamesPlatform.Instance.ReportScore((long)score, GPGSIds.leaderboard_hi_score, (bool success) =>
        {
            if (success)
            {
                Debug.Log("log to leaderboard succeeded");
                //PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_hi_score);
            }
            else
            {
                Debug.Log("log to leaderboard failed");
            }
        });
        //Reason the time is multiplied is because it uploads in milliseconds when being retrieved
        PlayGamesPlatform.Instance.ReportScore((long)time * 1000, GPGSIds.leaderboard_best_time, (bool success) =>
        {
            if (success)
            {
                Debug.Log("log to leaderboard succeeded");
                //PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_best_time);
            }
            else
            {
                Debug.Log("log to leaderboard failed");
            }
        });
        */
        //ILeaderboard lb = PlayGamesPlatform.Instance.lead;
        //PlayGamesPlatform.Instance.LoadScores.
        PlayGamesPlatform.Instance.ReportScore((long)enemies, GPGSIds.leaderboard_destroyed_enemies, (bool success) =>
        {
            if (success)
            {
                Debug.Log("log to leaderboard succeeded");
                //PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_hi_score);
            }
            else
            {
                Debug.Log("log to leaderboard failed");
            }
        });
        leaderboard.LoadLeaderBoard();
        //PlayGamesPlatform.Instance.ShowLeaderboardUI();
        /*
        gM = FindObjectOfType<GameManager>();
        score = gM.score;
        hSG.CreateScore(score);
        reviveText.text = "Revive \n " + gM.costToRevive.ToString();
        var ts = TimeSpan.FromSeconds(gM.timeInMatch);
        bestTime.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
        enemiesDestroyed.text = gM.destroyedEnemiesInMatch.ToString();
        */
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        /*
        if(gM.bestScore < score)
        {
            gM.bestScore = score;
            PlayerPrefs.SetInt("Score", gM.bestScore);
        }
        if(gM.bestTime < gM.timeInMatch)
        {
            gM.bestTime = gM.timeInMatch;
            PlayerPrefs.SetFloat("Time", gM.bestTime);
        }
        PlayerPrefs.SetInt("EnemiesDestroyed", gM.destroyedEnemies + gM.destroyedEnemiesInMatch);
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        */
    }
}
