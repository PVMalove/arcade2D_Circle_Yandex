using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Environment
{
    public class Burger : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Burger> { }
    }
}