using CamelInvaders.Entity.Buffs;
using UnityEngine;

public class MaxHealth_Buff : Buff_Base
{
    [SerializeField] private float AmountToIncreaseMaxHealth;
    
    protected override void ApplyBuff(IBuffable buffable)
    {
        buffable.IncreaseMaxHealth(AmountToIncreaseMaxHealth);
    }
}
