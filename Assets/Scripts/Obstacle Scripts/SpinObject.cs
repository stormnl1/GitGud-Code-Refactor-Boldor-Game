using UnityEngine;

public class SpinObject : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 100f;
    void Update()
    {
        transform.Rotate(0, spinSpeed, 0 * Time.deltaTime);
    }
}
