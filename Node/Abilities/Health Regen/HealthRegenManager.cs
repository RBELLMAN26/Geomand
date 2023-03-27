using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenManager : MonoBehaviour
{
    public Transform parentNode;
    public void ReturnToNode()
    {
        gameObject.SetActive(false);
        transform.parent = parentNode;
    }
}
