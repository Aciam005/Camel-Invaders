using UnityEngine;

namespace CamelInvaders.Entity.AI.Enemy
{
    public class Enemy : MonoBehaviour , IDamageable
    {
        public float MaxHealth;
        protected float _health;
        protected bool canShoot = false;
        [SerializeField] protected uint ChanceToDropHealthBuff = 50;

        [Header("REFERENCES")]
        [SerializeField] protected Transform AttackPoint;
        [SerializeField] protected GameObject HealBuff;

        [Space(10f)]
        [Header("AUDIO CLIPS")]
        [SerializeField] protected AudioClip ShootAudioClip;
        [SerializeField] private AudioClip HitAudioClip;
        [SerializeField] private AudioClip DeathAudioClip;

        void Awake()
        {
            _health = MaxHealth;
        }

        protected void CheckSight()
        {
            if(!Physics2D.Raycast(AttackPoint.position,Vector2.down,5.0f))
            {
                canShoot = true;
            }
        }
        public void TakeDamage(float amount)
        {
            _health-=amount;
            PlaySound(HitAudioClip);

            if(_health <= 0)    Die();
        }

        protected void PlaySound(AudioClip clip)
        {
            GameMaster.GameMaster.Instance.PlaySound(clip);
        }

        public void Die()
        {
            int roll = Random.Range(0,100);
            if(roll <= ChanceToDropHealthBuff && HealBuff != null)
                Instantiate(HealBuff , AttackPoint.position , Quaternion.Euler(0f,0f,-180f));

            PlaySound(DeathAudioClip);
            Destroy(this.gameObject);
        }
    }
}
