using UnityEngine;

public class Spike : MonoBehaviour
{   
    [SerializeField] private GameObject player;
    [SerializeField] private GameManager gameManager;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            gameManager.GameOver = true; // Set the GameOver variable to true when the player collides with the spike
        }
    }
}
