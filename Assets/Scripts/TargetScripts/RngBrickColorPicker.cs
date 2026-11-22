using UnityEngine;

public class RngBrickColorPicker : MonoBehaviour
{
    [SerializeField] private Material[] materials;

    [SerializeField] private int MaterialIndex;

    void Start()
    {
        MaterialIndex = Random.Range(0, materials.Length);
         GetComponent<MeshRenderer>().material = materials[MaterialIndex];
    }
}
