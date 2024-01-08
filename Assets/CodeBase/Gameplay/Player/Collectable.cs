using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Gameplay.Player
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField] private Transform centerTransform;
        [SerializeField] private List<float> spawnPosX;
        [SerializeField] private Player player;
        
        private void Awake()
        {
            SpawnScore();
        }

        private void OnEnable()
        {
            player.UpdateScore += SpawnScore;
        }

        private void OnDestroy()
        {
            player.UpdateScore -= SpawnScore;
        }

        private void SpawnScore()
        {
            gameObject.SetActive(true);
            transform.localPosition = Vector3.right * spawnPosX[Random.Range(0,spawnPosX.Count)];
            centerTransform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 37) * 10f);
        }

        public void DestroyCenterObject()
        {
            gameObject.SetActive(false);
        }
    }
}
