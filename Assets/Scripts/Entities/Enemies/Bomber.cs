using CamelInvaders.Entity.AI.Enemy;
using UnityEngine;

namespace CamelInvaders.Entity.AI.Enemy
{
    public class Bomber : Enemy
    {
        private float _time;
        private float timeBetweenRockets;

        [Header("STATS")]

        [SerializeField] private float MinTimeBetweenRockets;
        [SerializeField] private float MaxTimeBetweenRockets;

        [Space]

        [Header("REFERENCES")]
        [SerializeField] private Transform RocketSpawnPoint;
        [SerializeField] private GameObject RocketGameObject;
        void Start()
        {
            timeBetweenRockets = Random.Range(MinTimeBetweenRockets,MaxTimeBetweenRockets);
            _time = 0;
        }

        // Update is called once per frame
        void Update()
        {
            CheckSight();
            if(canShoot)
            {
                _time+=Time.deltaTime;
                
                if(_time >= timeBetweenRockets)
                {
                    Shoot();
                    _time = 0;
                }
            }
        }

        private void Shoot()
        {
            Instantiate(RocketGameObject,RocketSpawnPoint.position,Quaternion.identity);
            PlaySound(ShootAudioClip);
        }
    }
}
