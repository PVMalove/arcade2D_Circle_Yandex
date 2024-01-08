using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

namespace CodeBase.Gameplay
{
    public class BackgroundAnimation : MonoBehaviour
    {
        [SerializeField] private List<GameObject> circles;
        [SerializeField] private List<float> duration;
        
        [SerializeField] private Ease ease_circle = Ease.InOutBack;
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Sequence.Create()
                    .Group(Tween.Scale(circles[0].transform, endValue: 6f, duration: duration[0], ease_circle))
                    .Group(Tween.Scale(circles[1].transform, endValue: 8.5f, duration: duration[1], ease_circle))
                    .Group(Tween.Scale(circles[2].transform, endValue: 12.5f, duration: duration[2], ease_circle))
                    .Group(Tween.Scale(circles[3].transform, endValue: 20f, duration: duration[3], ease_circle));
            }

            if (Input.GetKey(KeyCode.W))
            {
                Sequence.Create()
                    .Group(Tween.Scale(circles[0].transform, endValue: 1.7f, duration: duration[0], ease_circle))
                    .Group(Tween.Scale(circles[1].transform, endValue: 6.2f, duration: duration[1], ease_circle))
                    .Group(Tween.Scale(circles[2].transform, endValue: 10.2f, duration: duration[2], ease_circle))
                    .Group(Tween.Scale(circles[3].transform, endValue: 14.2f, duration: duration[3], ease_circle));
            }
        }

        public void ON()
        {
            Sequence.Create()
                .Group(Tween.Scale(circles[0].transform, endValue: 6f, duration: duration[0], ease_circle))
                .Group(Tween.Scale(circles[1].transform, endValue: 8.5f, duration: duration[1], ease_circle))
                .Group(Tween.Scale(circles[2].transform, endValue: 12.5f, duration: duration[2], ease_circle))
                .Group(Tween.Scale(circles[3].transform, endValue: 20f, duration: duration[3], ease_circle));
        }
        
        public void OFF()
        {
            Sequence.Create()
                .Group(Tween.Scale(circles[0].transform, endValue: 1.7f, duration: duration[0], ease_circle))
                .Group(Tween.Scale(circles[1].transform, endValue: 6.2f, duration: duration[1], ease_circle))
                .Group(Tween.Scale(circles[2].transform, endValue: 10.2f, duration: duration[2], ease_circle))
                .Group(Tween.Scale(circles[3].transform, endValue: 14.2f, duration: duration[3], ease_circle));
        }
    }
}