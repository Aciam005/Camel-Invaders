using UnityEngine;

namespace CamelInvaders.Entity.Projectiles
{
    public class Splat : MonoBehaviour
    {
        [SerializeField] private AudioClip SplatClip;
        void OnEnable()
        {
            GameMaster.GameMaster.Instance.PlaySound(SplatClip);
            Destroy(this.gameObject,0.2f);
        }
    }
}
