using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gM;
    public int currentSelection;
    public AudioSource[] gameMusic;
    void Start()
    {
        gM = GetComponentInParent<GameManager>();
        currentSelection = Random.Range(0, gameMusic.Length);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMusicStatus();
    }

    public void CheckMusicStatus()
    {
        if (gM.gameStarted)
        {
            if (gM.audioMuted)
            {
                if (gameMusic[currentSelection].isPlaying)
                {
                    gameMusic[currentSelection].Stop();
                }

            }
            else
            {
                if (!gameMusic[currentSelection].isPlaying)
                {
                    gameMusic[currentSelection].Play();
                }
            }
        }
    }
    public void PlayGameMusic()
    {
        gM.audioMuted = false;
        /*
        if(!gameMusic[currentSelection].isPlaying)
        {
            gameMusic[currentSelection].Play();
        }
        */
        
    }
    public void PauseGameMusic()
    {
        gM.audioMuted = true;
        /*
        if (gameMusic[currentSelection].isPlaying)
        {
            gameMusic[currentSelection].Stop();
        }
        */
    }
}
