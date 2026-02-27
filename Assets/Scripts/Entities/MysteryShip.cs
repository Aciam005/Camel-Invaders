using UnityEngine;

namespace CamelInvaders.Entity
{
    public class MysteryShip : MonoBehaviour,IDamageable
    {
        [Header("STATS")]
        [SerializeField] private float Speed;

        [Space(10f)]

        [SerializeField] private GameObject BuffToSpawn;
        [SerializeField] private Transform BuffSpawnpoint;

        void Update()
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
        public void TakeDamage(float amount)
        {
            Instantiate(BuffToSpawn,BuffSpawnpoint.position,Quaternion.Euler(0f,0f,180f));
            Destroy(this.gameObject);
        }
    }
}
