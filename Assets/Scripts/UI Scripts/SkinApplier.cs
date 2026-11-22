using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    private const string SkinKey = "PlayerSkinIndex";

    [Header("Assign 4 materials in order (0-3)")]
    [SerializeField] private Material[] skins = new Material[4];

    [Header("Renderer to change (drag your MeshRenderer/SkinnedMeshRenderer)")]
    [SerializeField] private Renderer targetRenderer;

    private void Awake()
    {
        // If you forgot to assign it, try to auto-find
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        ApplySavedSkin();
    }

    public void ApplySavedSkin()
    {
        int index = PlayerPrefs.GetInt(SkinKey, 0); // default skin 0
        ApplySkin(index);
    }

    public void ApplySkin(int index)
    {
        if (skins == null || skins.Length == 0 || targetRenderer == null) return;

        index = Mathf.Clamp(index, 0, skins.Length - 1);

        // Use .material to assign an instance (safe for runtime skin changes)
        targetRenderer.material = skins[index];
    }
}