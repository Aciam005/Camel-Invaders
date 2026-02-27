using CamelInvaders.Entity.Buffs;
using UnityEngine;

namespace CamelInvaders.Entity.Player
{
    public class Player : MonoBehaviour , IDamageable , IBuffable
    {
        private Vector2 InputVector = Vector2.zero;
        private float timeSinceLastLaser;
        private float timeSinceLastRocket;
        private float timeSinceLastDamageTaken;
        private float _health;
        private float _shield;

        [Header("STATS")]
        public float Speed;
        public float MaxHealth;
        public float MaxShield;
        public float LaserFireRate = 0.1f;
        public float RocketFireRate = 0.1f;
        public float ShieldRechargeTime = 2.0f;
        public float ShieldRechargeRate = 2.0f;

        [Space(10f)]

        [Header("REFERENCES")]
        [SerializeField] private GameObject LaserPrefab;
        [SerializeField] private GameObject RocketPrefab;
        [SerializeField] private Transform LaserSpawnPoint;
        [SerializeField] private Transform RocketSpawnPoint;
        [SerializeField] private GameObject ShieldGameObject;
        private Camera cam;

        [Space(10f)]
        [Header("AUDIO CLIPS")]
        [SerializeField] private AudioClip LaserShootAudioClip;
        [SerializeField] private AudioClip RocketShootAudioClip;
        [SerializeField] private AudioClip HitAudioClip;
        [SerializeField] private AudioClip BuffAudioClip;

        void OnEnable()
        {
            cam = Camera.main;
            timeSinceLastDamageTaken = 0f;

            GameMaster.GameMaster.Instance.UpdateMaxHealthUI(MaxHealth);
            SetHealth(MaxHealth);

            GameMaster.GameMaster.Instance.UpdateMaxShieldUI(MaxShield);
            SetShield(MaxShield);
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                GameMaster.GameMaster.Instance.HandleGamePause();


            if(GameMaster.GameMaster.Instance.IsGamePaused()) return;
            HandleInput();
            HandleMovement();
            HandleCombat();
        }

        private void HandleInput()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            InputVector.x = x;
            InputVector.y = y;

            InputVector.Normalize();

            if(Input.GetKey(KeyCode.Space) && timeSinceLastLaser >= LaserFireRate)
            {
                ShootLaser();
                timeSinceLastLaser = 0;
            }

            if(Input.GetKey(KeyCode.R) && timeSinceLastRocket >= RocketFireRate)
            {
                ShootRocket();
                timeSinceLastRocket = 0;
            }
        }

        private void ShootLaser()
        {
            Instantiate(LaserPrefab,LaserSpawnPoint.position,Quaternion.identity);
            PlaySound(LaserShootAudioClip);
        }
        private void ShootRocket()
        {
            Instantiate(RocketPrefab,RocketSpawnPoint.position,Quaternion.Euler(0,0,180f));
            PlaySound(RocketShootAudioClip);
        }   

        private void HandleMovement()
        {
            Vector3 leftEdge = cam.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightEdge = cam.ViewportToWorldPoint(Vector3.right);
            Vector3 middleEdge = cam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0.5f));

            transform.Translate(new Vector3(InputVector.x ,InputVector.y,0) * Speed * Time.deltaTime);

            Vector3 position = transform.position;
            position.x = Mathf.Clamp(transform.position.x,leftEdge.x + transform.localScale.x,rightEdge.x - transform.localScale.x);
            position.y = Mathf.Clamp(position.y,leftEdge.y + transform.localScale.x , middleEdge.y - transform.localScale.y - 5.0f);
            transform.position = position;
        }
    
        private void HandleCombat()
        {
            float deltaTime = Time.deltaTime;

            timeSinceLastLaser += deltaTime;
            timeSinceLastRocket += deltaTime;
            timeSinceLastDamageTaken += deltaTime;

            if(timeSinceLastDamageTaken >= ShieldRechargeTime)
            {
               SetShield(_shield + ShieldRechargeRate * deltaTime);
                
            }

            if(_shield <= 0)
            {
                ShieldGameObject.SetActive(false);
            }else
            {
                ShieldGameObject.SetActive(true);
            }
        }

        private void PlaySound(AudioClip clip)
        {
            GameMaster.GameMaster.Instance.PlaySound(clip);
        }
        private void SetHealth(float amount)
        {
            _health = amount;
            _health = Mathf.Clamp(_health , 0,MaxHealth);
            UpdateHealthUI();
        }

        private void SetShield(float amount)
        {
            _shield = amount;
            _shield = Mathf.Clamp(_shield,0,MaxShield);
            UpdateShieldUI();
        }

        public void TakeDamage(float amount)
        {
            Debug.Log("PLAYER OOF");
            timeSinceLastDamageTaken = 0f;
            PlaySound(HitAudioClip);

            if(_shield >= amount)
            {
                SetShield(_shield -= amount);
            }else
            {
                SetHealth(_health - amount + _shield);
                SetShield(0);

                if(_health <= 0)
                {
                    Die();
                }
            }

        }

        private void UpdateHealthUI()
        {
            GameMaster.GameMaster.Instance.UpdateHealthUI(_health);
        }
        private void UpdateShieldUI()
        {
            GameMaster.GameMaster.Instance.UpdateShieldUI(_shield);
        }

        private void Die()
        {
            GameMaster.GameMaster.Instance.PlayersLose();
            Destroy(this.gameObject);
        }

        #region Buff Logic
        public void Heal(float amount)
        {
            SetHealth(_health + amount);
            PlaySound(BuffAudioClip);
        }

        public void IncreaseMaxHealth(float amount)
        {
            MaxHealth += amount;
            GameMaster.GameMaster.Instance.UpdateMaxHealthUI(MaxHealth);
            SetHealth(MaxHealth);
            PlaySound(BuffAudioClip);
        }

        public void IncreaseMaxShield(float amount)
        {
            MaxShield+=amount;
            SetShield(MaxShield);
            GameMaster.GameMaster.Instance.UpdateMaxShieldUI(MaxShield);
            PlaySound(BuffAudioClip);
        }

        public void BuffLaserFireRate(float newFireRate)
        {
            LaserFireRate = newFireRate;
            PlaySound(BuffAudioClip);
        }

        public void BuffRocketFireRate(float newFireRate)
        {
            RocketFireRate = newFireRate;
            PlaySound(BuffAudioClip);
        }
        #endregion
    }
}
