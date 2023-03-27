using TMPro;
using UnityEngine;

public class EnemyHealthText : MonoBehaviour
{
    public EnemyInfo enemy;
    public TextMeshPro healthText;
    public int healthlevel = 1;
    public int nextLevel;
    public string healthletter; //Goes Through All Letters starting A = 0, B = 00;
    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponentInParent<EnemyInfo>();
        healthText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        nextLevel = (int)Mathf.Pow(10.0f, (float)healthlevel);

        if (enemy.health.ToString().Length > 3)
        {
            CheckLevel();
            healthlevel = enemy.health.ToString().Length - 1;
            int firsttwo = enemy.health / (int)Mathf.Pow(10.0f, (float)healthlevel - 1);
            healthText.text = firsttwo.ToString() + healthletter;

        }
        else
        {
            healthText.text = enemy.health.ToString();
        }
    }

    void CheckLevel()
    {
        switch(enemy.health.ToString().Length)
        {
            case 4:
                {
                    healthletter = "a";
                    break;
                }
            case 5:
                {
                    healthletter = "b";
                    break;
                }
            case 6:
                {
                    healthletter = "c";
                    break;
                }
            case 7:
                {
                    healthletter = "d";
                    break;
                }
            case 8:
                {
                    healthletter = "e";
                    break;
                }
            case 9:
                {
                    healthletter = "f";
                    break;
                }
            case 10:
                {
                    healthletter = "g";
                    break;
                }



        }
    }
}
