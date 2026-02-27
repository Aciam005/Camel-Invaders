using UnityEngine;
using CamelInvaders.Entity.Player;
using CamelInvaders.Entity.Projectiles;
using CamelInvaders.Entity.Buffs;

namespace CamelInvaders.GameMaster
{
    public class Barrier : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.GetComponent<Projectile>() || other.GetComponent<Buff_Base>())
            {
                Destroy(other.gameObject,1.0f);
            }

        }
    }
}
