using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HiScoreGUI : MonoBehaviour
{
    public int currentScore;
    public List<TextMeshPro> hiScoreNameText = new List<TextMeshPro>(); //These will be displayed as TextMeshPros when highscore is displayed will display as name
    public List<TextMeshPro> hiScoreText = new List<TextMeshPro>(); //These Will be displayed as textmeshpros when highscore is displayed.  Will Display Score
    public List<string> hiScoreName = new List<string>();
    public List<int> hiScore = new List<int>(); //Example First in last out,  First is highest, last is lowest 
    public TextMeshProUGUI testScore;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hiScore.Count; i++)
        {
            if (currentScore < hiScore[i])
            {
                if (i == hiScore.Count)
                {
                    //Will Input Highscore at end of string
                }
            }
            else
            {
                //Will Place score in between and then create score is made.

            }
        }
    }

    //Function will populate a Input field where the score is supposed to be.  Player will input a name for now until I add google highscore api and replace automatically there ID.
    public void CreateScore(int score)
    {
        currentScore = score;
        testScore.text = currentScore.ToString("00000");
        //print(currentScore.ToString());
    }
}
