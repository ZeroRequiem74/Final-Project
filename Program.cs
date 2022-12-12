using Raylib_cs;
using System.Numerics;

namespace HelloWorld{

    static class Program{

        public static void Main(string[] args){
            var ScreenHeight = 480;
            var ScreenWidth = 800;

            Raylib.InitWindow(ScreenWidth, ScreenHeight, "GameObject");
            Raylib.SetTargetFPS(60);

            var Objects = new List<GameObject>();
            var Lasers = new List<GameObject>();
            var Random = new Random();
            var Pilot = new Pilot();
            var CountOfEachShape = 0;
            int points = 0;
            int lives = 3;

            
            Pilot.Position = new Vector2(400, 425);

            while (!Raylib.WindowShouldClose() && lives != 0){

                if (CountOfEachShape < 10){
                    // Add a new random object to the screen every iteration of our game loop
                    var whichType = Random.Next(2);

                    // Generate a random velocity for this object
                    var RandomY = Random.Next(0, 10);
                    var randomX = Random.Next(ScreenWidth);

                    // Each object will start about the center of the screen
                    var position = new Vector2(randomX, 0);
                    
                    Console.WriteLine("Creating an alien");
                    var square = new Alien();
                    square.Position = position;
                    square.Velocity = new Vector2(2, 5);
                    Objects.Add(square);

                    CountOfEachShape += 1;
                }

                if(Pilot.IsShooting()){
                    var rectangle = new Laser(Color.PURPLE, 20);
                    var laser = Pilot.Position;
                    laser.Y -= 50;
                    rectangle.Position = laser;
                    rectangle.Velocity = new Vector2(0, -5);
                    Lasers.Add(rectangle);
                }

                var randomShoot = Random.Next(0, 10);
                var randomShip = Random.Next(Objects.Count);

                if(randomShoot == 0){
                    var rectangle = new Laser(Color.PURPLE, 20);
                    var blast = Objects[randomShip].Position;
                    blast.Y += 0;
                    rectangle.Position = blast;
                    rectangle.Velocity = new Vector2(0, 5);
                    Lasers.Add(rectangle);
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                foreach (var obj in Objects) {
                    obj.Draw();
                }

                foreach (var laser in Lasers) {
                    laser.Draw();
                }

                Pilot.Draw();


                foreach (var obj in Objects.ToList()) {
                    if (obj is Alien) {
                        var ship = (Alien)obj;
                        foreach (var laser in Lasers.ToList()){
                            var blast = (Laser)laser;
                            if (Raylib.CheckCollisionRecs(ship.Rect(), blast.Rect())) {
                                Objects.Remove(ship);
                                Lasers.Remove(blast);
                                CountOfEachShape -= 1;
                                points += 1;
                            }
                        }
                    
                    }                    
                }

                foreach (var laser in Lasers.ToList()) {
                    if (laser is Laser){
                        var blast = (Laser)laser;
                        if (Raylib.CheckCollisionRecs(blast.Rect(), Pilot.Rect())) {
                            lives -= 1;
                            Lasers.Remove(blast);
                        }
                    }
                }

                var message = $"Current Points:{points}";
                Raylib.DrawText(message, 0, 30, 20, Color.BLACK);

                Raylib.EndDrawing();

                foreach(var obj in Objects.ToList()){
                    obj.Move();
                }

                foreach(var laser in Lasers.ToList()){
                    laser.Move();
                }

                Pilot.Move();

                
            }

            Raylib.CloseWindow();
        }
    }

}
