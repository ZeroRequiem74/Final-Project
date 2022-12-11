using Raylib_cs;
using System.Numerics;

class GameObject {
    public Vector2 Position { get; set; } = new Vector2(0, 0);
    public Vector2 Velocity { get; set; } = new Vector2(0, 0);

    virtual public void Draw() {
        // Base game objects do not have anything to draw
    }

    virtual public void Move() {
        Vector2 NewPosition = Position;
        NewPosition.X += Velocity.X;
        NewPosition.Y += Velocity.Y;
        Position = NewPosition;
    }
}

class ColoredObject: GameObject {
    public Color Color { get; set; }

    public ColoredObject(Color color) {
        Color = color;
    }
}

class Laser: ColoredObject {
    public int Size { get; set; }

    public Laser(Color color, int size): base(color) {
        Size = size;
    }

    public Rectangle Rect() {
        return new Rectangle(Position.X, Position.Y, Size, Size);
    }

    override public void Draw() {
        Raylib.DrawRectangle((int)Position.X, (int)Position.Y, Size, Size, Color);
    }
}

class Alien: GameObject {

    
    Texture2D texture;

    public Alien() {
        
        var image = Raylib.LoadImage("alien.png");
        this.texture = Raylib.LoadTextureFromImage(image);
        Raylib.UnloadImage(image);
    }

    public Rectangle Rect() {
        return new Rectangle(Position.X, Position.Y, 50, 53);
    }
    public override void Move()
    {
        // Reset the velocity every frame unless keys are being pressed
        var velocity = new Vector2();
        var movementSpeed = 2;

        if (Position.X + 10 == 800){
            velocity.X = -movementSpeed;
        }

        if (Position.X - 10 == 0 ) {
            velocity.X = movementSpeed;
        }

        if (Position.Y < 240) {
            velocity.Y += movementSpeed;
        }

        if (Position.Y == 240) {
            velocity.Y = 0;
        }

        Velocity = velocity;

        base.Move();
    }

    override public void Draw() {
        Raylib.DrawTexture(this.texture, (int)Position.X, (int)Position.Y, Color.WHITE);
    }
}

class Pilot: GameObject {

    Texture2D texture;

    public Pilot() {
        
        var image = Raylib.LoadImage("Galaga.png");
        this.texture = Raylib.LoadTextureFromImage(image);
        Raylib.UnloadImage(image);
    }

    public Rectangle Rect() {
        return new Rectangle(Position.X, Position.Y, 50, 53);
    }

    public override void Move()
    {
        // Reset the velocity every frame unless keys are being pressed
        var velocity = new Vector2();
        var movementSpeed = 5;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) {
            velocity.X = movementSpeed;
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) {
            velocity.X = -movementSpeed;
        }

        Velocity = velocity;

        base.Move();
    }

    public bool IsShooting(){
        return Raylib.IsKeyDown(KeyboardKey.KEY_SPACE);
    }

    public override void Draw() {
        Raylib.DrawTexture(this.texture, (int)Position.X, (int)Position.Y, Color.WHITE);
    }
}

class Points: ColoredObject{

    string Text;

    public Points(string text, Color color): base(color) {
        Text = text;
    }

    public override void Draw() {
        Raylib.DrawText(this.Text, (int)Position.X, (int)Position.Y, 20, this.Color);
    }
    
}