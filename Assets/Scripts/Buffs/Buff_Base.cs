using UnityEngine;

namespace CamelInvaders.Entity.Buffs
{
    public abstract class Buff_Base : MonoBehaviour
    {
        [SerializeField] private float Speed = 10.0f;

        private void Update()
        {
            transform.Translate(Vector3.down * (-Speed) * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            IBuffable buf = other.GetComponent<IBuffable>();
            if(buf != null)
            {
                ApplyBuff(buf);
                Destroy(this.gameObject);
            }
        }

        protected abstract void ApplyBuff(IBuffable buffable);
    }
}
