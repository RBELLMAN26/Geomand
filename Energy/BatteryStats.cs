using TMPro;
using UnityEngine;

public class BatteryStats : MonoBehaviour
{
    public TextMeshPro energyText;
    public int amountOfEnergy;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        amountOfEnergy = Random.Range(1, 10);
        energyText.text = amountOfEnergy.ToString();
        speed = Random.Range(0.25f, 2.0f);
    }
}
