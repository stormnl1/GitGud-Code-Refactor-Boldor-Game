using UnityEngine;

public class BouncePad : MonoBehaviour
{

    [SerializeField] private float bounceForce = 10f; // Force applied to the player when colliding with the bounce pad
    [SerializeField] private GameObject player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            playerRb.AddForce(transform.up * bounceForce, ForceMode.Impulse); // Apply an impulse force to the player to push them away
        }
    }
}
