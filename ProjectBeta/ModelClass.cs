using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBeta
{
    public class ModelClass
    {
        //  Objects
        public PlayerObject player;
        public List<EnemyClass> enemies;
        public List<Explosion> explosions;
        public List<StarClass> stars;

        // Random object
        private Random random;

        // Delegate
        public delegate void GuidenceSystem(EnemyClass enemy);
        public GuidenceSystem MoveEnemy;

        // Constructor
        public ModelClass()
        {
            player = new PlayerObject(new PointStruct(20, 20)); // Creates a player
            enemies = new List<EnemyClass>(); // Creates enemies list
            explosions = new List<Explosion>(); // Creates explosions list
            stars = new List<StarClass>();
            random = new Random(); // Creates random instance
            CreateEnemy(3); // Creates three enemies
            CreateStars(10); // Creates stars
        }

        // Creates stars 
        public void CreateStars(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int x = random.Next(1, Math.Min(Console.BufferWidth, Console.WindowWidth));
                int y = random.Next(1, Math.Min(Console.BufferHeight, Console.WindowHeight)-1);
                stars.Add(new StarClass(new PointStruct(x, y)));
            }
        }

        // Updates current level
        public void UpdateLevel(int level)
        {
            if (level == 2) CreateEnemy();
        }

        // Star Collision
        public void StarCollision(GameObject gameObject)
        {
            foreach (var star in stars)
            {
                if (gameObject.position.y == star.position.y) // Y cord matches
                {
                    if (gameObject.position.x + gameObject.sprite.Length - 1>= star.position.x && star.position.x >= gameObject.position.x)
                    {
                        star.colideWith = gameObject.ID;
                        star.currentCollision = true;
                        star.displayed = false;
                        continue;
                    }
                }
                if (gameObject.ID == star.colideWith)
                {
                    star.currentCollision = false;
                }
            }
        }

        //// Star Collision
        //public void StarCollision(GameObject gameObject)
        //{
        //    foreach (var star in stars)
        //    {

        //        foreach (var enemy in enemies)
        //        {
        //            if (enemy.position.y == star.position.y) // Y cord matches
        //            {
        //                //if ((gameObject.position.x + gameObject.sprite.Length - 1 >= star.position.x && star.position.x >= gameObject.position.x) ||
        //                //    (player.position.x + player.sprite.Length - 1 >= star.position.x && star.position.x >= player.position.x)) // X cord matches
        //                if (enemy.position.x + enemy.sprite.Length - 1 >= star.position.x && star.position.x >= enemy.position.x)
        //                {
        //                    star.currentCollision = true;
        //                    star.displayed = false;
        //                    continue;
        //                }
        //            }
        //        }

        //        if (gameObject.position.y == star.position.y || player.position.y == star.position.y) // Y cord matches
        //        {
        //            if ((gameObject.position.x + gameObject.sprite.Length - 1 >= star.position.x && star.position.x >= gameObject.position.x) ||
        //                (player.position.x + player.sprite.Length - 1 >= star.position.x && star.position.x >= player.position.x)) // X cord matches
        //            {
        //                star.currentCollision = true;
        //                star.displayed = false;
        //                continue;
        //            }
        //        }
        //        star.currentCollision = false;
        //    }
        //}

        // Creates enemies
        public void CreateEnemy(int amount = 1, Aggression state = Aggression.Passive)
        {
            for (int i = 0; i < amount; i++)
            {
                bool collision = true;

                do
                {
                    // Creating ship
                    int x = random.Next(1, Math.Min(Console.BufferWidth, Console.WindowWidth));
                    int y = random.Next(1, Math.Min(Console.BufferHeight, Console.WindowHeight));
                    EnemyClass ship = new EnemyClass(new PointStruct(x, y), state);

                    // Checks for collision
                    var detect = DetectCollision(ship);

                    if (!detect.hit)
                    {
                        enemies.Add(ship);
                        collision = false;
                    }
                } while (collision);

            }
        }

        // Collision detection
        public (bool hit, GameObject gameObject) DetectCollision(GameObject currentObject)
        {
            foreach (var ship in enemies) // For each enemy ship
            {
                if (ship.ID != currentObject.ID) // Ship in list not the object sent in
                {
                    if (currentObject.position.y == ship.position.y) // Y cord matches
                    {
                        if (currentObject.position.x + currentObject.sprite.Length >= ship.position.x && currentObject.position.x <= ship.position.x + ship.sprite.Length) // X cord matches
                        {
                            return (true, ship);
                        }
                    }
                }
            }

            return (false, currentObject);
        }

        // Determines what state the enemy should move
        public void DetermineEnemyMovement(EnemyClass enemy)
        {
            if (enemy.state == Aggression.Passive) MoveEnemy = RandomMovement;
            else MoveEnemy = TargetedMovement;
            MoveEnemy(enemy);
        }

        // Randomly moves around
        public void RandomMovement(EnemyClass enemy)
        {
            int x = random.Next(0, 3) - 1;
            int y = random.Next(0, 3) - 1;

            enemy.position.Move(enemy, x, y);
        }

        // Moves towards player
        public void TargetedMovement(EnemyClass enemy)
        {
            int x_distance = player.position.x - enemy.position.x;
            int x_move = x_distance < -2 ? -2 : x_distance > 2 ? 2 : x_distance;
            int y_distance = player.position.y - enemy.position.y;
            int y_move = y_distance < -2 ? -2 : y_distance > 2 ? 2 : y_distance;

            // randomize movement a little
            x_move += random.Next(0, 3) - 1;
            y_move += random.Next(0, 3) - 1;

            enemy.position.Move(enemy, x_move, y_move);
        }

        // Removes enemy
        public void RemoveEnemy(GameObject currentObject)
        {
            enemies.RemoveAt(enemies.FindIndex(e => e.ID == currentObject.ID));
        }

        // Determines player movement
        public void ProcessPlayerMovement(ConsoleKeyInfo key)
        {
            char action = key.KeyChar;

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    action = 'w';
                    break;
                case ConsoleKey.DownArrow:
                    action = 's';
                    break;
                case ConsoleKey.RightArrow:
                    action = 'd';
                    break;
                case ConsoleKey.LeftArrow:
                    action = 'a';
                    break;
                default:
                    break;
            }

            switch (action)
            {
                case 'a':
                    player.MoveLeft();
                    break;
                case 'd':
                    player.MoveRight();
                    break;
                case 'w':
                    player.MoveUp();
                    break;
                case 's':
                    player.MoveDown();
                    break;
                case '1':
                case '2':
                case '3':
                case '4':
                case '6':
                case '7':
                case '8':
                case '9':
                    player.DetermineShot(player.position, action);
                    break;
                case '5':
                    player.CycleWeapon(player);
                    break;
                default:
                    break;
            }
        }
    }
}
