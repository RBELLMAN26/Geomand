using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
public class GPGSAuthentication : MonoBehaviour
{

    public static PlayGamesPlatform platform;
    // Start is called before the first frame update
    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }
        //Social.Active
    }
}
