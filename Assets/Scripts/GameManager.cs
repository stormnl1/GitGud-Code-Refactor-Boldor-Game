using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour

{   
    private int targetsLeft;
    public bool GameOver = false;
    public float gameOverBoundary = -20f;
    
    [Header("References")]
    public GameObject Player;
    public TextMeshProUGUI scoreText;
    public GameObject winScreen;
    public GameObject GameOverScreen;
    public PlayerController Invincibility;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TargetsLeft();
        OnGameOver();
        GameOverBoundary();
    }

    private void TargetsLeft()
    {
        targetsLeft = GameObject.FindGameObjectsWithTag("TargetPointTrigger").Length;
        scoreText.text = "Targets left: " + targetsLeft;
        
        if (targetsLeft == 0)
        {
            winScreen.SetActive(true);
            Player.GetComponent<PlayerController>().enabled = false;
            Invincibility.IsInvincible = true;

            Cursor.lockState = CursorLockMode.None; // unlock cursor
            Cursor.visible = true; // make cursor visible
        }
    }

    public void GameOverBoundary()
    {
        if (Player.transform.position.y < gameOverBoundary)
        {
            GameOver = true;
        }
    }

    public void OnGameOver()
    {
        if (GameOver == true && targetsLeft != 0)
        {
            Player.GetComponent<PlayerController>().enabled = false; // Disable the player controller when it collides with the enemy ball
            GameOverScreen.SetActive(true); // Activate the Game Over screen
            
            Cursor.lockState = CursorLockMode.None; // unlock cursor
            Cursor.visible = true; // make cursor visible
        }
    }
}
