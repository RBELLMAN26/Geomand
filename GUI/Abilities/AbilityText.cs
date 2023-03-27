using TMPro;
using UnityEngine;

public class AbilityText : MonoBehaviour
{
    public Abilities ab;
    public TextMeshPro costText;
    // Start is called before the first frame update
    void Start()
    {
        ab = GetComponentInParent<Abilities>();
        costText = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        costText.text = ab.batteryCost.ToString();
    }
}
