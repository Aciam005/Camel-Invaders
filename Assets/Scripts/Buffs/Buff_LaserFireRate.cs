using CamelInvaders.Entity.Buffs;
using UnityEngine;

public class Buff_LaserFireRate : Buff_Base
{
    [SerializeField] private float newFireRate;

    protected override void ApplyBuff(IBuffable buffable)
    {
        buffable.BuffLaserFireRate(newFireRate);
    }
}
