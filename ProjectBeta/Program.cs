using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectBeta
{
    class Program
    {
        // Initializing classes
        static public ViewClass view = new ViewClass();
        static public ModelClass model = new ModelClass();

        // Game status
        static private bool gameRunning = true;

        // Scoreboard
        static private int _level = 1;
        static private int _score = 0;
        static private Weapons _weapon = Weapons.Gun;
        static public int level
        {
            get { return _level; }
            set
            {
                _level++;
                model.UpdateLevel(level);
                view.UpdateLevel(level);
            }
        }
        static public int score
        {
            get { return _score; }
            private set
            {
                _score++;
                view.UpdateScore(score);
            }
        }
        static public Weapons weapon
        {
            get { return _weapon; }
            set
            {
                _weapon = model.player.currentWeapon;
                view.DisplayWeapon(weapon);
            }
        }

        // Runs on start
        static void Main(string[] args)
        {
            // Disables cursor
            Console.CursorVisible = false;

            // Starts threads
            Thread playerThread = new Thread(ProcessPlayer);
            Thread bulletThread = new Thread(ProcessBullets);
            Thread explosion = new Thread(ProcessExplosions);
            Thread stars = new Thread(ProcessStars);
            playerThread.Start();
            bulletThread.Start();
            explosion.Start();
            stars.Start();

            // Displays enemy ships
            foreach (var ship in model.enemies) view.Display(ship);
            foreach (var star in model.stars) view.Display(star);
            // Displays scoreboard
            view.UpdateScore(score);
            view.UpdateLevel(level);

            // Enemy ship processing
            do
            {
                Thread.Sleep(500);
                // Number of enemy ships
                int number = model.enemies.Count;

                // If no more enemies
                if (number == 0)
                { // Create new level
                    model.CreateEnemy(level, Aggression.Aggressive); // Adds aggressive enemies equal to the level
                    model.CreateEnemy((level - 1) * 2); // Adds passive enemies equal to the level - 1 * 2
                    level++;
                }

                // Loop through all enemies
                for (int i = number - 1; i >= 0; i--)
                {
                    // Get enemy
                    EnemyClass enemy = model.enemies.ElementAt(i);

                    // Process enemies
                    view.RemoveObject(enemy); // Remove display
                    model.DetermineEnemyMovement(enemy); // Move enemy
                    model.StarCollision(enemy); // Star collision

                    var collision = model.DetectCollision(enemy); // Collision check
                    // If enemy hit
                    if (collision.hit)
                    {
                        model.explosions.Add(new Explosion(collision.gameObject.position, collision.gameObject.sprite.Length));
                        view.RemoveObject(enemy);
                        model.RemoveEnemy(enemy);
                    }
                    else view.Display(enemy); // Display enemy
                }
            } while (gameRunning);

            // Game over message
            view.GameOver();
        }

        // Player processing
        private async static void ProcessPlayer()
        {
            // Displays player
            view.Display(model.player);

            // Player loop
            do
            {
                ConsoleKeyInfo input = await view.GetInput(); // Gets players input
                if (weapon != model.player.currentWeapon) weapon = model.player.currentWeapon;
                view.RemoveObject(model.player); // Erases players ship
                model.ProcessPlayerMovement(input); // Processes movement
                model.StarCollision(model.player); // Star collision

                // Detects collision
                var collision = model.DetectCollision(model.player);
                if (collision.hit) // If player was hit
                {
                    model.explosions.Add(new Explosion(collision.gameObject.position, collision.gameObject.sprite.Length)); // Explodes player
                    view.RemoveObject(collision.gameObject); // Removes player 
                    gameRunning = false; // Sets game to no longer running
                }
                else view.Display(model.player); // Else display player
            } while (gameRunning);
            // End player loop
        }

        // Process bullets
        private async static void ProcessBullets()
        {
            // Bullet loop
            do
            {
                // Count bullet
                int number = model.player.bullets.Count;

                // If there are bullets
                if (number != 0)
                {
                    // For each bullet
                    for (int i = number - 1; i >= 0; i--)
                    {
                        // Bullet should be removed
                        bool remove = false;

                        // Get bullet
                        BulletClass bullet = model.player.bullets.ElementAt(i);
                        view.RemoveObject(bullet); // Removes from display

                        model.StarCollision(bullet);

                        // Bullet collision
                        var collision = model.DetectCollision(bullet);
                        if (collision.hit) // If bullet hit something
                        {
                            model.explosions.Add(new Explosion(collision.gameObject.position, collision.gameObject.sprite.Length)); // Adds explosion
                            model.RemoveEnemy(collision.gameObject); // Removes enemy from list
                            view.RemoveObject(collision.gameObject); // Undraws enemy
                            remove = true;
                            score += 1;
                        }
                        else remove = bullet.position.Move(bullet, bullet.xVel, bullet.yVel); // Else if bullet off map

                        // Removes bullet
                        if (remove) { model.player.bullets.RemoveAt(i); view.RemoveObject(bullet); }
                        else view.Display(bullet); // Else displays bullet
                    }
                }
                await Task.Delay(50); // Wait 50ms
            } while (gameRunning);
            // End bullet loop
        }

        // Process explosions
        private async static void ProcessExplosions()
        {
            // Explosion loop
            do
            {
                // Explosion Count
                int number = model.explosions.Count;

                // If there are explosions
                if (number != 0)
                {
                    // For each explosion
                    for (int i = number - 1; i >= 0; i--)
                    {
                        // Get bullet
                        Explosion explosion = model.explosions.ElementAt(i);

                        // Display explosion
                        view.ExplodeObject(explosion);

                        // If explosions are done
                        if (explosion.tick == 4) { model.explosions.RemoveAt(i); }
                        else explosion.tick++; // Else increase explosion tick
                    }
                }
                await Task.Delay(100); // Wait 100ms
            } while (gameRunning);
            // End explosion loop
        }

        private static void ProcessStars()
        {
            do
            {
                Task.Delay(1);
                foreach (var star in model.stars)
                {
                    if (!star.displayed && !star.currentCollision)
                    {
                        view.Display(star);
                        star.displayed = true;
                    }
                }
            } while (gameRunning);
        }
    }
}
