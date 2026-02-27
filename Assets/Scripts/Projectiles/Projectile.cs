using UnityEngine;

namespace CamelInvaders.Entity.Projectiles
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject SplatPrefab;
        [SerializeField] private Vector3 Direction;
        [SerializeField] private uint Speed;
        [SerializeField] private float Damage;

        private void Update()
        {
            transform.Translate(Direction * Speed * Time.deltaTime);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            IDamageable d;      

            if( other.TryGetComponent<IDamageable>(out d) )
            {
                d.TakeDamage(Damage);
                Instantiate(SplatPrefab,transform.position,Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}

