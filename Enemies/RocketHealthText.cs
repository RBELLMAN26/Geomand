using TMPro;
using UnityEngine;

public class RocketHealthText : MonoBehaviour
{
    public RocketStats rocket;
    public TextMeshPro healthText;
    public int healthlevel = 1;
    public int nextLevel;
    public string healthletter; //Goes Through All Letters starting A = 0, B = 00;
    // Start is called before the first frame update
    void Start()
    {
        rocket = GetComponentInParent<RocketStats>();
        healthText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        nextLevel = (int)Mathf.Pow(10.0f, (float)healthlevel);

        if (rocket.health.ToString().Length > 2)
        {
            CheckLevel();
            healthlevel = rocket.health.ToString().Length - 1;
            int firsttwo = rocket.health / (int)Mathf.Pow(10.0f, (float)healthlevel - 1);
            healthText.text = firsttwo.ToString() + healthletter;

        }
        else
        {
            healthText.text = rocket.health.ToString();
        }
    }

    void CheckLevel()
    {
        if (rocket.health.ToString().Length > (int)Mathf.Pow(10.0f, (float)healthlevel).ToString().Length)
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
                        healthletter = "A";
                        break;
                    }


            }
        }
    }
}
