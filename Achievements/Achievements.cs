using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Achievements : MonoBehaviour
{
    public void CheckAchievements(int score, float time, int destroyed)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_killer, destroyed, (bool success) => {

        });
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_murderer, destroyed, (bool success) => {

        });
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_destroyer, destroyed, (bool success) => {

        });
        if(time >= 900)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_survivor, (bool success) =>
            {

            });
        }
        if(score >= 100000)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_score_saver, (bool success) =>
            {

            });
        }
        if(score >= 1000000)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_score_lover, (bool success) =>
            {

            });
        }
        if(FindObjectOfType<GameManager>().endlessMode)
        {
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_upgrader, (bool success) =>
            {

            });
        }
    }

}
