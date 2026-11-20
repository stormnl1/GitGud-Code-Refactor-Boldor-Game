using UnityEngine;

public class EnemyPusher : Enemy
{
    [SerializeField] private float bounceForce = 10f; // Force applied to the player when colliding with the enemy ball

    protected override void OnPlayerCollision(Collision collision)
    {
        Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized; // Calculate the direction away from the player
         playerRb.AddForce(awayFromPlayer * bounceForce, ForceMode.Impulse); // Apply an impulse force to the player to push them away
    }
}
