using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
public class ScoreStats : MonoBehaviour
{
    private string mStatus;
    public void Load_Scores()
    {
        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard_destroyed_enemies,
            LeaderboardStart.PlayerCentered,
            100,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (data) =>
            {
                mStatus = "Leaderboard data valid: " + data.Valid;
                print(mStatus);
                mStatus += "\n approx:" + data.ApproximateCount + " have " + data.Scores.Length;
                print(mStatus);
            });
    }
}
