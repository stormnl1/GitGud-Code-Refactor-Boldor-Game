using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxSensor : MonoBehaviour
{

    private int count = 0;
    public TextMeshPro countText;
    public GameObject crateRamp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("specialCrate"))
        {
            count += 1;
            countText.text = "Crates deposited: " + count;
        }

        if (count == 5)
        {
            crateRamp.SetActive(true);
        }

    }
}
