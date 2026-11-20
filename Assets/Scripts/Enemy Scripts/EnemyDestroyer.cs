using Unity.VisualScripting;
using UnityEngine;

public class EnemyDestroyer : Enemy
{
    protected override void OnPlayerCollision(Collision collision)
    {
        if (player.GetComponent<PlayerController>().IsInvincible == false)
        {
            gameManager.GameOver = true; // Set the GameOver variable to true when the enemy ball collides with the player
        }
    }

    //Wether an enemy type is destructible is determined by the tag 'Destructible' see PlayerController.cs for the invincibility method. 
}

