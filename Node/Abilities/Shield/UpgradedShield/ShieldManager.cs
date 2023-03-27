using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public int shieldsLeft;
    public ShieldStats[] shields;
    public Transform parentNode;
    public void CheckCount()
    {
        if (shieldsLeft - 1 <= 0)
        {
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
            transform.parent = parentNode;
        }
        else
        {
            shieldsLeft -= 1;
        }
    }
    public void ReviveShield()
    {
        shieldsLeft = 5;
        foreach(ShieldStats obj in shields)
        {
            obj.gameObject.SetActive(true);
            obj.shieldStrength = 7500;
        }
    }
}
