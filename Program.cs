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
                    var randomY = Random.Next(1, 3);
                    var randomX = Random.Next(ScreenWidth);

                    // Each object will start about the center of the screen
                    var position = new Vector2(randomX, 0);
                    
                    Console.WriteLine("Creating an alien");
                    var square = new Alien(Color.PURPLE, 20);
                    square.Position = position;
                    square.Velocity = new Vector2(0, randomY);
                    Objects.Add(square);

                    CountOfEachShape += 1;
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                foreach (var obj in Objects) {
                    obj.Draw();
                }

                Pilot.Draw();


                foreach (var obj in Objects.ToList()) {
                    if (obj is Alien) {
                        var shape = (Alien)obj;
                        if (Raylib.CheckCollisionRecs(Pilot.Rect(), shape.Rect())) {
                            lives -= 1;
                            Objects.Remove(obj);
                            CountOfEachShape -= 1;
                        }
                        if (Raylib.CheckCollisionRecs(Pilot.Rect(), shape.Rect())){
                            points += 1;
                        }
                    }
                }

                var message = $"Current Points:{points}";

                Raylib.EndDrawing();


                Raylib.CloseWindow();
            }

            Raylib.CloseWindow();
        }
    }

}