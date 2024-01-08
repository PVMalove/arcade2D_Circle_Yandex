using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Core.Services.InputService;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab, collectableParticlePrefab;
        [SerializeField] private float rotateSpeed;

        [SerializeField] private float startRadius;
        [SerializeField] private float moveTime;

        [SerializeField] private List<float> rotateRadius;

        private int level;
        private float currentRadius;
        private float lastRadius;
        private bool canClick;

        private IInputService input;
        
        public event Action UpdateScore;
        
        [Inject]
        private void Construct(IInputService input)
        {
            this.input = input;
        }
        
        private void Awake()
        {
            canClick = true;
            level = 0;
            currentRadius = startRadius;
        }

        private void Update()
        {
            if(canClick && input.GetInputClick())
            {
                StartCoroutine(ChangeRadius());
            }
        }

        private void FixedUpdate()
        {
            float rotateValue = rotateSpeed * Time.fixedDeltaTime * startRadius / currentRadius;
            transform.RotateAround(Vector3.zero, Vector3.forward, rotateValue); 
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Obstacle"))
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                return;
            }

            if(collision.CompareTag("Collectable"))
            {
                Destroy(Instantiate(collectableParticlePrefab, transform.position, Quaternion.identity),1f);
                collision.gameObject.GetComponent<Collectable>().DestroyCenterObject();
                UpdateScore?.Invoke();
            }
        }

        private IEnumerator ChangeRadius()
        {
            canClick = false;
            float moveStartRadius = rotateRadius[level];
            float moveEndRadius = rotateRadius[(level + 1) % rotateRadius.Count];
            float moveOffset = moveEndRadius - moveStartRadius;
            float speed = 1 / moveTime;
            float timeElasped = 0f;
            while(timeElasped < 1f)
            {
                timeElasped += speed * Time.fixedDeltaTime;
                currentRadius = moveStartRadius + timeElasped * moveOffset;
                if(!Mathf.Approximately(currentRadius, lastRadius)) 
                    ApplyRadius();
                yield return new WaitForFixedUpdate();
            }

            canClick = true;
            level = (level + 1) % rotateRadius.Count;
            currentRadius = rotateRadius[level];
        }
        
        private void ApplyRadius()
        {
            Vector3 direction = (transform.position - Vector3.zero).normalized;
            transform.position = Vector3.zero + direction * currentRadius;
            lastRadius = currentRadius;
        }
    }
}
