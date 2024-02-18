using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    [RequireComponent(typeof(RawImage))]
    public class ImageScroller : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float scrollSpeed = 0.1f;
        [SerializeField, Range(-1, 1)] private float xDirection = 1;
        [SerializeField, Range(-1, 1)] private float yDirection = 1;
        
        private RawImage image;

        private void Awake() => image = GetComponent<RawImage>();

        private void Update()
            => image.uvRect = new Rect(image.uvRect.position + 
                                       new Vector2(-xDirection * scrollSpeed, yDirection * scrollSpeed) * 
                                       Time.deltaTime, image.uvRect.size);
    }
}
