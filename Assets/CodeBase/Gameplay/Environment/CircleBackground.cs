using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Environment
{
    public class CircleBackground : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<CircleBackground> { }
    }
}