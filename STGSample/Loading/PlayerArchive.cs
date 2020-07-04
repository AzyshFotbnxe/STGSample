using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STGSample.Loading
{
    public class PlayerArchive
    {
        public float Health;
        public float Attack;
        public float Defense;
        public float FireRate;
        public int Weapon;
        public int Balance;
        public PlayerArchive() {
            Health = 100;
            Attack = 10;
            FireRate = 1;
            Defense = 1;
            Weapon = 1;
            Balance = 0;
        }
        public PlayerArchive(float health = 100, float attack = 10, float fireRate = 1, float defense = 1, int weapon = 1)
        {
            Health = health;
            Attack = attack;
            FireRate = fireRate;
            Defense = defense;
            Weapon = weapon;
            Balance = 0;
        }
    }
}
