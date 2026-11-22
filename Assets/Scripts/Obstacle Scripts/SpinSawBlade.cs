using UnityEngine;

public class SpinSawBlade : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 100f;
    void Update()
    {
        transform.Rotate(spinSpeed, 0, 0 * Time.deltaTime);
    }
}
