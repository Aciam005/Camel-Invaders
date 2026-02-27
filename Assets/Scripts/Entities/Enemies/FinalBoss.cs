using UnityEngine;

namespace CamelInvaders.Entity.AI.Enemy
{
    public class FinalBoss : Enemy
    {
        [Header("STATS")]
        [Tooltip("Health necessary for the boss to transition to the next stage")]
        [SerializeField] private float HealthForNextStage;
        [SerializeField] private float StageOneLaserFireRate;
        [SerializeField] private float StageTwoLaserFireRate;
        [SerializeField] private Transform[] LaserSpawnPoints;

        [Space]

        [SerializeField] private float StageOneRocketFireRate;
        [SerializeField] private float StageTwoRocketFireRate;
        [SerializeField] private Transform[] RocketSpawnPoints;

        [Space(10f)]

        [SerializeField] private GameObject LaserGameObject;
        [SerializeField] private GameObject RocketGameObject;

        public enum BossStage
        {
            StageOne,
            StageTwo
        }

        private BossStage bossStage = BossStage.StageOne;
        private float _laserFireRate;
        private float _laserTime;

        private float _rocketFireRate;
        private float _rocketTime;

        [SerializeField] private AudioClip RocketShootAudioClip;

        void OnEnable()
        {
            _laserTime = 0;
            _rocketTime = 0;
        }

        private void Update()
        {
            if(_health <= HealthForNextStage)
                bossStage = BossStage.StageTwo;

            switch (bossStage)
            {
                case BossStage.StageOne : TickStageOne();
                                          break;

                case BossStage.StageTwo : TickStageTwo();
                                          break;
            }
        }

        private void TickStageOne()
        {
            _laserFireRate = StageOneLaserFireRate;
            _rocketFireRate = StageOneRocketFireRate;

            _laserTime+=Time.deltaTime;
            _rocketTime+=Time.deltaTime;

            if(_laserTime >= _laserFireRate)
            {
                ShootLaser();
                _laserTime = 0;
            }

            if(_rocketTime >= _rocketFireRate)
            {
                ShootRocket();
                _rocketTime = 0;
            }
        }

        private void TickStageTwo()
        {
            _laserFireRate = StageTwoLaserFireRate;
            _rocketFireRate = StageTwoRocketFireRate;

            _laserTime+=Time.deltaTime;
            _rocketTime+=Time.deltaTime;

            if(_laserTime >= _laserFireRate)
            {
                ShootLaser();
                _laserTime = 0;
            }

            if(_rocketTime >= _rocketFireRate)
            {
                ShootRocket();
                _rocketTime = 0;
            }
        }

        private void ShootLaser()
        {
            foreach(Transform spawnPoint in LaserSpawnPoints)
            {
                Instantiate(LaserGameObject,spawnPoint.position,Quaternion.identity);
                PlaySound(ShootAudioClip);
            }
        }

        private void ShootRocket()
        {
            foreach(Transform spawnPoint in RocketSpawnPoints)
            {
                Instantiate(RocketGameObject,spawnPoint.position,Quaternion.identity);
                PlaySound(RocketShootAudioClip);
            }
        }
    }
}
