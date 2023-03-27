using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpfulGUI : MonoBehaviour
{
    GameManager gM;
    [SerializeField] float countdown = 5;
    // Start is called before the first frame update
    void Start()
    {
        gM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gM.gameStarted)
        {
            if (countdown > 0)
            {
                countdown -= Time.deltaTime;
            }
            else
            {
                countdown = 3;
                this.gameObject.SetActive(false);
            }
        }
    }
}
