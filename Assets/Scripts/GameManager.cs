using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour

{   
    [Header("References")]
    public GameObject Player;
    public TextMeshProUGUI scoreText;

    private int targetsLeft;

    // Update is called once per frame
    void Update()
    {
        TargetsLeft();
    }

    private void TargetsLeft()
    {
        targetsLeft = GameObject.FindGameObjectsWithTag("TargetPointTrigger").Length;
        scoreText.text = "Targets left: " + targetsLeft;
    }
}