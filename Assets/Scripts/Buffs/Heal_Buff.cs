using UnityEngine;

namespace CamelInvaders.Entity.Buffs
{
    public class Heal_Buff : Buff_Base
    {
        [SerializeField] private float AmountToHeal;

        protected override void ApplyBuff(IBuffable buffable)
        {
            buffable.Heal(AmountToHeal);
        }
    }
}
