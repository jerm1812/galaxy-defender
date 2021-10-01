using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBeta
{
    // Different game objects
    public enum Objects
    {
        Unkown, Player, Enemy, Bullet, Lazer, Missle, Mine, Star, Moon, Asteroid
    }

    // Weapons
    public enum Weapons
    {
        Gun, Mine, Lazer
    }

    // Aggression of enemy
    public enum Aggression
    {
        Passive, Aggressive
    }

    // Explosions
    public enum Explosions
    {
        ShipExplosion, MineExplosion, MissleExplosion, Disintegrate
    }
}
