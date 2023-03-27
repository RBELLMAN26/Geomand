using TMPro;
using UnityEngine;

public class EnemyBossText : MonoBehaviour
{
    public HitPoint hit;
    public TextMeshPro healthText;
    public int healthlevel = 1;
    public int nextLevel;
    public string healthletter; //Goes Through All Letters starting A = 0, B = 00;
    // Start is called before the first frame update
    void Awake()
    {
        hit = GetComponentInParent<HitPoint>();
        healthText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

        nextLevel = (int)Mathf.Pow(10.0f, (float)healthlevel);
        if (hit.health.ToString().Length > 2)
        {
            CheckLevel();
            healthlevel = hit.health.ToString().Length - 1;
            int firsttwo = hit.health / (int)Mathf.Pow(10.0f, (float)healthlevel - 1);
            healthText.text = firsttwo.ToString() + healthletter;

        }
        else
        {
            healthText.text = hit.health.ToString();
        }
    }

    void CheckLevel()
    {
        if (hit.health.ToString().Length > (int)Mathf.Pow(10.0f, (float)healthlevel).ToString().Length)
        {
            switch (healthletter)
            {
                case "":
                    {
                        healthletter = "a";
                        break;
                    }
                case "a":
                    {
                        healthletter = "b";
                        break;
                    }
                case "b":
                    {
                        healthletter = "c";
                        break;
                    }
                case "c":
                    {
                        healthletter = "d";
                        break;
                    }
                case "d":
                    {
                        healthletter = "e";
                        break;
                    }
                case "e":
                    {
                        healthletter = "f";
                        break;
                    }
                case "f":
                    {
                        healthletter = "g";
                        break;
                    }
                case "g":
                    {
                        healthletter = "h";
                        break;
                    }
                case "h":
                    {
                        healthletter = "i";
                        break;
                    }
                case "i":
                    {
                        healthletter = "j";
                        break;
                    }
                case "j":
                    {
                        healthletter = "k";
                        break;
                    }
                case "k":
                    {
                        healthletter = "l";
                        break;
                    }
                case "l":
                    {
                        healthletter = "m";
                        break;
                    }
                case "m":
                    {
                        healthletter = "n";
                        break;
                    }
                case "n":
                    {
                        healthletter = "o";
                        break;
                    }
                case "o":
                    {
                        healthletter = "p";
                        break;
                    }
                case "p":
                    {
                        healthletter = "q";
                        break;
                    }
                case "q":
                    {
                        healthletter = "r";
                        break;
                    }
                case "r":
                    {
                        healthletter = "s";
                        break;
                    }
                case "s":
                    {
                        healthletter = "t";
                        break;
                    }
                case "t":
                    {
                        healthletter = "u";
                        break;
                    }
                case "u":
                    {
                        healthletter = "v";
                        break;
                    }
                case "v":
                    {
                        healthletter = "w";
                        break;
                    }
                case "w":
                    {
                        healthletter = "x";
                        break;
                    }
                case "x":
                    {
                        healthletter = "y";
                        break;
                    }
                case "y":
                    {
                        healthletter = "z";
                        break;
                    }
                case "z":
                    {
                        healthletter = "b";
                        break;
                    }


            }
        }
    }
}
