using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    public GameManager gM;
    public GameObject pauseMenu, TouchArea, ContinueGUI, KnowledgeGUI, AboutGUI;
    public TMP_Text scoreText;
    public int scorelevel = 1;
    public int nextLevel;

    // Update is called once per frame
    void Update()
    {
        /*
        nextLevel = (int)Mathf.Pow(10.0f, (float)scorelevel);

        if (tI.currentHealth.ToString().Length > 2)
        {
            CheckLevel();
            healthlevel = tI.currentHealth.ToString().Length - 1;
            int firsttwo = tI.currentHealth / (int)Mathf.Pow(10.0f, (float)healthlevel - 1);
            healthText.text = firsttwo.ToString() + healthletter;

        }
        else
        {
            healthText.text = tI.currentHealth.ToString();
        }
        */
        if (gM.score.ToString().Length > 3)
        {
            int firsttwo = (int)gM.score / (int)Mathf.Pow(10.0f, (int)gM.score.ToString().Length - 2);
            scoreText.text = gM.score.ToString();
            scoreText.text = string.Format("{0}({1})", gM.score.ToString(), firsttwo.ToString() + GetLetter(gM.score.ToString().Length));
        }
        else
        {
            scoreText.text = gM.score.ToString("000");
            scoreText.text = string.Format("{0}({1})", gM.score.ToString("000"), GetLetter(gM.score.ToString().Length));
        }
        
        
    }
    string GetLetter(int value)
    {
        switch(value)
        {
            case 4:
                {
                    return "a";
                }
            case 5:
                {
                    return "b";
                }
            case 6:
                {
                    return "c";
                }
            case 7:
                {
                    return "d";
                }
            case 8:
                {
                    return "e";
                }
            case 9:
                {
                    return "f";
                }
            case 10:
                {
                    return "g";
                }
        }
        return "";
    }
    public void CheckPause(int Pause)
    {
        if (Pause == 0)
        {
            //pauseMenu.SetActive(System.Convert.ToBoolean(1));
            pauseMenu.GetComponent<PauseGUI>().Paused();
            TouchArea.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            //pauseMenu.SetActive(System.Convert.ToBoolean(0));
            pauseMenu.GetComponent<PauseGUI>().UnPaused();
            TouchArea.SetActive(true);
            Time.timeScale = 1;
        }

        gM.pauseStatus = Pause;
        print("Pause");
    }

    public void LaunchContinue()
    {
        print("GameOver");
        TouchArea.SetActive(false);
        ContinueGUI.SetActive(true);
        Time.timeScale = 0;

    }
    public void SetRevive()
    {
        TouchArea.SetActive(true);
        ContinueGUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void OpenKnowledge()
    {
        KnowledgeGUI.SetActive(true);
    }
    public void OpenAbout()
    {

    }
}
