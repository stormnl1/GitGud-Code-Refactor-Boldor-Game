using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed of the enemy ball
    [SerializeField] private float detectionRange = 50f; // Range within which the enemy will detect the player
    private float deathBoundary = -20f; // Y position below which the enemy will be destroyed
    private Rigidbody EnemyRb;
    public GameObject player;
    public GameManager gameManager;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
        DeathBoundary();
    }

    private void MoveTowardsPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectionRange) // Check if the player is within the detection range
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized; // Calculate the direction towards the player
            EnemyRb.AddForce(lookDirection * speed); // Move towards the player
        }
    }

    private void DeathBoundary()
    {
        if (transform.position.y < deathBoundary) // Check if the enemy has fallen below the death boundary
        {
            Destroy(gameObject); // Destroy the enemy object
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPlayerCollision(collision);
        }
    }
    protected abstract void OnPlayerCollision(Collision collision); // Abstract method to be implemented by derived classes for handling collisions
}
