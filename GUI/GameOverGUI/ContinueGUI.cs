//using UnityEngine.Monetization;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

//ContinueGUI Will Display A 10 Second Timer For Continue Or Go To GameOver Screen
//Will be a continue with ads and with cost of score
//Another will be a give up
//Will show your Current Time, Score, Kills

public class ContinueGUI : MonoBehaviour, IUnityAdsListener
{
    //public float continueTimer = 10;
    public double score, enemies;
    public int time;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI bestTime;
    public TextMeshProUGUI enemiesDestroyed;
    public TextMeshProUGUI difficultyText;
    //public TextMeshProUGUI continueText;
    public GameObject GameOverGUI;
    public GameManager gM;

    public BaseInfo bI;
    public TextMeshProUGUI reviveText, adText;
    public Button adButton, reviveBtn;
    public bool usedAd = false;
    string gameId = "3386860";
    bool testMode = false;
    string myPlacementId = "rewardedVideo";
    public bool startTimer;
    bool revivedOnce = false;
    //Activate the hiscorescripts when GameOverGUI is enabled
    void OnEnable()
    {
        //startTimer = true;
        gM = FindObjectOfType<GameManager>();
        score = gM.score;
        //time = Mathf.RoundToInt(gM.timeInMatch);
        enemies = gM.destroyedEnemiesInMatch;
        bestScore.text = score.ToString();
        difficultyText.text = gM.difficulty.ToString();
        //var ts = TimeSpan.FromSeconds(gM.timeInMatch);
        //bestTime.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
        bestTime.text = gM.convertedTime;
        enemiesDestroyed.text = gM.destroyedEnemiesInMatch.ToString();
        gM.costToRevive = Mathf.RoundToInt((int)gM.score / 2);
        reviveText.text = "Revive \n " + gM.costToRevive.ToString();
        adText.text = "100 Health \n+" + (Mathf.RoundToInt((float)gM.costToRevive / 2)).ToString() + " Score";
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, false);
    }

    public void ContinueWithAds()
    {
        if (!usedAd)
        {
            /*
            if (Advertisement.IsReady(myPlacementId))
            {
                
                Advertisement.Show(myPlacementId);

    
                startTimer = false;
                continueTimer = 10;
            }
            */
            StartCoroutine(PlayAd());
        }
    }
    public void ContinueWithoutAds()
    {
        if (gM.score >= gM.costToRevive && !revivedOnce)
        {
            gM.score -= gM.costToRevive;
            //gM.costToRevive *= (gM.difficulty * 2);
            gM.Revive(false);
            startTimer = false;
            reviveBtn.interactable = false;
            adButton.interactable = false;
            //continueTimer = 10;
        }
    }
    public void SetGameOver()
    {
        GameOverGUI.GetComponent<GameOverGUI>().score = score;
        GameOverGUI.GetComponent<GameOverGUI>().time = time;
        GameOverGUI.GetComponent<GameOverGUI>().enemies = enemies;
        GameOverGUI.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator PlayAd()
    {
        while (!Advertisement.IsReady(myPlacementId))
        {
            yield return new WaitForEndOfFrame();
            //Advertisement.GetPlacementState(myPlacementId);
        }
        Advertisement.Show(myPlacementId);
        startTimer = false;
        //continueTimer = 10;

    }
    
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, show the ad:

        throw new System.NotImplementedException();
    }
    
    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Ad Error - " + message);
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }
    
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult)
        {
            case ShowResult.Finished:
                {
                    // Reward the user for watching the ad to completion.
                    gM.Revive(true);
                    gM.score += Mathf.RoundToInt((float)gM.costToRevive / 2);
                    reviveBtn.interactable = false;
                    adButton.interactable = false;
                    usedAd = true;
                    break;
                }
            case ShowResult.Skipped:
                {
                    //There is no skip option
                    break;
                }
            case ShowResult.Failed:
                {
                    Debug.LogError("The ad did not finish due to an error.");
                    break;
                }
        }
        
        //throw new System.NotImplementedException();
    }

    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
