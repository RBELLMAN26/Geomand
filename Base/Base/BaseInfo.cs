using TMPro;
using UnityEngine;

public class BaseInfo : MonoBehaviour
{
    public bool debugMode;
    public int health = 100;
    public TextMeshPro healthText;
    public GameObject nodeObj;
    public GameObject[] turrets = new GameObject[4];
    public Transform[] hits;
    public int defenseLevel = 6;

    // Update is called once per frame
    void Update()
    {
        healthText.text = health.ToString();
    }

    public void LoseHealth()
    {
        defenseLevel = 6;
        for (int i = 0; i < turrets.Length; i++)
        {
            if (turrets[i].activeSelf)
            {
                defenseLevel -= 1;
            }
        }
        if (nodeObj.activeSelf)
        {
            defenseLevel -= 1;
        }
        int newHealth = health - defenseLevel;

        if (newHealth > 0)
        {
            if (!debugMode)
            {
                health = newHealth;
            }

        }
        else
        {
            //print("GameOver");
            GameObject.FindObjectOfType<GameManager>().GameOver();//GameOver
        }
    }

    public void Revive(bool isAd)
    {
        if(isAd)
        {
            health = 100;
        }
        else
        {
            health = 50;
        }
        
    }
}
