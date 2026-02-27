using CamelInvaders.Entity.AI.Enemy;
using UnityEngine;

namespace CamelInvaders.Entity.AI.Enemy
{
    public class Fighter : Enemy
    {
        private float _time;
        private float timeBetweenLasers;
        [Header("STATS")]

        [SerializeField] private float MinTimeBetweenLasers;
        [SerializeField] private float MaxTimeBetweenLasers;

        [Space]

        [Header("REFERENCES")]
        [SerializeField] private Transform LaserSpawnPoint;
        [SerializeField] private GameObject LaserGameObject;
        
        void OnEnable()
        {
            timeBetweenLasers = Random.Range(MinTimeBetweenLasers,MaxTimeBetweenLasers);
            _time = 0;
        }

        void Update()
        {
            CheckSight();
            if(canShoot)
            {
                _time+=Time.deltaTime;
                
                if(_time >= timeBetweenLasers)
                {
                    Shoot();
                    _time = 0;
                }
            }
        }

        private void Shoot()
        {
            Instantiate(LaserGameObject,LaserSpawnPoint.position,Quaternion.identity);
            PlaySound(ShootAudioClip);
        }
    }
}
