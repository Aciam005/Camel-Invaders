using CamelInvaders.Entity;
using CamelInvaders.Entity.AI.Enemy;
using UnityEngine;

namespace CamelInvaders.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Data", menuName = "CamelInvaders/WaveScriptableObject", order = 1)]
    public class WaveScriptableObject : ScriptableObject
    {
        public Enemy[] Enemies;
        public MysteryShip MysteryShip;
        public uint Rows = 5;
        public uint Columns = 11;
    }
}

