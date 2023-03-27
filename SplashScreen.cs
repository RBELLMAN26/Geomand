using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScreen : MonoBehaviour
{

    public bool Finished;
    // Use this for initialization
    void Start()
    {

        StartCoroutine(PlaySplash());

    }

    private IEnumerator PlaySplash()
    {
        yield return new WaitForSecondsRealtime(1);
        //SceneManager.LoadScene("Main Game");
        SceneManager.LoadSceneAsync("Main Game");
        /*
        string VideoPath = Application.streamingAssetsPath + "/Video/SplashScreen.mp4";
        print(VideoPath);
        Handheld.PlayFullScreenMovie("Video/SplashScreen.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFit);
        //if (Handheld.PlayFullScreenMovie.Finished)
        //yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        SceneManager.LoadScene("Main Game");
        */
    }

}
