using Raylib_cs;
using System.Numerics;

namespace HelloWorld{

    static class Program{

        public static void Main(string[] args){
            var ScreenHeight = 480;
            var ScreenWidth = 800;

            Raylib.InitWindow(ScreenWidth, ScreenHeight, "GameObject");
            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose()){

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                Raylib.EndDrawing();


                Raylib.CloseWindow();
            }

            Raylib.CloseWindow();
        }
    }

}