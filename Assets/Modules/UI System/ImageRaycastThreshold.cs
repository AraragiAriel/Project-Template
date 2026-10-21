using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageRaycastThreshold : MonoBehaviour
{
    private const float threshold = .1f;

    private Image _image;
    private Image image => _image ??= GetComponent<Image>();

    void Awake()
    {
        image.alphaHitTestMinimumThreshold = threshold;
    }
}
