using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBeta
{
    public class PlayerObject : GameObject
    {
        // List of bullets
        public List<BulletClass> bullets;

        // Current weapon
        public Weapons currentWeapon;

        // Constructor
        public PlayerObject(PointStruct position, string sprite = "<=>", Objects type = Objects.Player) : base(position, sprite, type)
        {
            bullets = new List<BulletClass>();
            currentWeapon = Weapons.Gun;
        }

        // Cycle weapon
        public void CycleWeapon(PlayerObject playerObject)
        {
            int weapon = (int)currentWeapon;
            if (weapon == 2)
            {
                currentWeapon = Weapons.Gun;
            }
            else
            {
                weapon++;
                currentWeapon = (Weapons)weapon;
            }
        }

        // Determine shot
        public void DetermineShot(PointStruct position, char number)
        {
            if (currentWeapon == Weapons.Gun) ShootGun(position, number);
            if (currentWeapon == Weapons.Mine) DropMine(position, number);
        }

        // Switches weapons
        public string SwapWeapon(PlayerObject playerObject)
        {
            string weapon = null;
            int weaponI = (int)currentWeapon;
            if (weaponI == 1) currentWeapon = Weapons.Gun;
            else weaponI++; currentWeapon = (Weapons)weaponI;
            return weapon;
        }

        private void DropMine(PointStruct position, char number)
        {
            int mineX = 0;
            int mineY = 0;

            switch (number)
            {
                case '1':
                    mineX = position.x - 1;
                    mineY = position.y + 1;
                    break;
                case '2':
                    mineX = position.x + 1;
                    mineY = position.y + 1;
                    break;
                case '3':
                    mineX = position.x + this.sprite.Length;
                    mineY = position.y + 1;
                    break;
                case '4':
                    mineX = position.x - 1;
                    break;
                case '6':
                    mineX = position.x + this.sprite.Length;
                    break;
                case '7':
                    mineX = position.x - 1;
                    mineY = position.y - 1;
                    break;
                case '8':
                    mineX = position.x + 1;
                    mineY = position.y - 1;
                    break;
                case '9':
                    mineX = position.x + this.sprite.Length;
                    mineY = position.y - 1;
                    break;
                default:
                    break;
            }

            // Bullet creation
            bullets.Add(new BulletClass(new PointStruct(mineX, mineY), 0, 0, Weapons.Gun, sprite:"@"));

        }

        // Create bullet
        public void ShootGun(PointStruct position, char number)
        {
            int xVel = 0;
            int yVel = 0;

            int bulletX = 0;
            int bulletY = position.y;

            // Determines bullet direction
            switch (number)
            {
                case '1':
                    xVel = -1;
                    yVel = 1;
                    bulletX = position.x - 1;
                    bulletY = position.y + 1;
                    break;
                case '2':
                    yVel = 1;
                    bulletX = position.x + 1;
                    bulletY = position.y + 1;
                    break;
                case '3':
                    xVel = 1;
                    yVel = 1;
                    bulletX = position.x + this.sprite.Length;
                    bulletY = position.y + 1;
                    break;
                case '4':
                    xVel = -1;
                    bulletX = position.x - 1;
                    break;
                case '6':
                    xVel = 1;
                    bulletX = position.x + this.sprite.Length;
                    break;
                case '7':
                    xVel = -1;
                    yVel = -1;
                    bulletX = position.x - 1;
                    bulletY = position.y - 1;
                    break;
                case '8':
                    yVel = -1;
                    bulletX = position.x + 1;
                    bulletY = position.y - 1;
                    break;
                case '9':
                    xVel = 1;
                    yVel = -1;
                    bulletX = position.x + this.sprite.Length;
                    bulletY = position.y - 1;
                    break;
                default:
                    break;

            }

            // Bullet creation
            bullets.Add(new BulletClass(new PointStruct(bulletX, bulletY), xVel, yVel, Weapons.Gun));
        }

        // Removes bullet
        public void RemoveBullet(GameObject bullet)
        {
            bullets.RemoveAt(bullets.FindIndex(b => b.ID == bullet.ID));
        }
    }
}
