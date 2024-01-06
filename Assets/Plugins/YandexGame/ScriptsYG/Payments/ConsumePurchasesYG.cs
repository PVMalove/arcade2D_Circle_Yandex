using UnityEngine;

namespace YG
{
    public class ConsumePurchasesYG : MonoBehaviour
    {
        private void OnEnable() => YandexGame.GetDataEvent += ConsumePurchases;
        private void OnDisable() => YandexGame.GetDataEvent -= ConsumePurchases;

        private static bool consume;

        private void Start()
        {
            if (YandexGame.SDKEnabled)
                ConsumePurchases();
        }

        private void ConsumePurchases()
        {
            if (!consume)
            {
                consume = true;
                YandexGame.ConsumePurchases();
            }
        }
    }
}