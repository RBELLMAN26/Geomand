using TMPro;
using UnityEngine;

public class EnergyStats : MonoBehaviour
{
    public int energyLevel = 100;
    public TextMeshPro displayLevel;

    // Update is called once per frame
    void Update()
    {
        //energyLevel = transform.parent.GetComponent<NodeInfo>().energy;
        displayLevel.text = energyLevel.ToString();
    }
}
