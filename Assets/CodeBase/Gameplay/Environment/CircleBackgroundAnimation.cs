using System.Collections.Generic;
using PrimeTween;
using UnityEngine;

namespace CodeBase.Gameplay.Environment
{
    public class CircleBackgroundAnimation : MonoBehaviour
    {
        [SerializeField] private List<GameObject> circles;
        [SerializeField] private List<float> duration;
        [SerializeField] private List<float> startSize;
        [SerializeField] private List<float> endSize;
        
        [SerializeField] private Ease ease_circle = Ease.InOutBack;
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                Sequence.Create()
                    .Group(Tween.Scale(circles[0].transform, endValue: startSize[0], duration: duration[0], ease_circle))
                    .Group(Tween.Scale(circles[1].transform, endValue: startSize[1], duration: duration[1], ease_circle))
                    .Group(Tween.Scale(circles[2].transform, endValue: startSize[2], duration: duration[2], ease_circle))
                    .Group(Tween.Scale(circles[3].transform, endValue: startSize[3], duration: duration[3], ease_circle));
            }

            if (Input.GetKey(KeyCode.W))
            {
                Sequence.Create()
                    .Group(Tween.Scale(circles[0].transform, endValue: endSize[0], duration: duration[0], ease_circle))
                    .Group(Tween.Scale(circles[1].transform, endValue: endSize[1], duration: duration[1], ease_circle))
                    .Group(Tween.Scale(circles[2].transform, endValue: endSize[2], duration: duration[2], ease_circle))
                    .Group(Tween.Scale(circles[3].transform, endValue: endSize[3], duration: duration[3], ease_circle));
            }
        }

        public void StartGameAnimation()
        {
            Sequence.Create()
                .Group(Tween.Scale(circles[0].transform, endValue: endSize[0], duration: duration[0], ease_circle))
                .Group(Tween.Scale(circles[1].transform, endValue: endSize[1], duration: duration[1], ease_circle))
                .Group(Tween.Scale(circles[2].transform, endValue: endSize[2], duration: duration[2], ease_circle))
                .Group(Tween.Scale(circles[3].transform, endValue: endSize[3], duration: duration[3], ease_circle));
        }

        public void EndGameAnimation()
        {
            Sequence.Create()
                .Group(Tween.Scale(circles[0].transform, endValue: startSize[0], duration: duration[0], ease_circle))
                .Group(Tween.Scale(circles[1].transform, endValue: startSize[1], duration: duration[1], ease_circle))
                .Group(Tween.Scale(circles[2].transform, endValue: startSize[2], duration: duration[2], ease_circle))
                .Group(Tween.Scale(circles[3].transform, endValue: startSize[3], duration: duration[3], ease_circle));
        }
    }
}