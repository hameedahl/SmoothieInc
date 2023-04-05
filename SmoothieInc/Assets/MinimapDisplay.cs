using UnityEngine;
using UnityEngine.UI;

public class MinimapDisplay : MonoBehaviour
{
    [SerializeField] private MinimapSync minimapSync;
    private RawImage minimapImage;

    private void Start()
    {
        minimapImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        if (minimapSync != null)
        {
            minimapImage.texture = minimapSync.MinimapTexture2D;
        }
    }
}
