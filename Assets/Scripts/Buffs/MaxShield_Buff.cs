using CamelInvaders.Entity.Buffs;
using UnityEngine;

public class MaxShield_Buff : Buff_Base
{
    [SerializeField] private float AmountToIncreaseMaxShield;
    
    protected override void ApplyBuff(IBuffable buffable)
    {
        buffable.IncreaseMaxShield(AmountToIncreaseMaxShield);
    }
}
