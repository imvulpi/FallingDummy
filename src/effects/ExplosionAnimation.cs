    using FallingDummy.src.commons.animations;
using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

public partial class ExplosionAnimation : IAnimation
{
    public event EventHandler AnimationEnd;
    [Export] public bool IsRunning = false;
    [Export] public CompressedTexture2D CompressedTexture;
    public Vector2 TextureSize { get; set; } = Vector2.Zero;
    [Export] public int ExplosionsNumber { get; set; } = 8;
    [Export] public float RotationSpeed { get; set; } = 45;
    [Export] public float ScaleSpeed { get; set; } = 0.01f;
    [Export] public float SameScalePercentage { get; set; } = 70;
    [Export] public Vector2 MaxScale { get; set; } = new Vector2(1,1);
    [Export] public Vector2 ScaleModifier { get; set; } = Vector2.One;
    [Export] public Vector2 InitialScale {  get; set; } = Vector2.Zero;
    [Export] public Vector2 PositionModifier { get; set; } = Vector2.One;
    [Export] public Vector2 MaxPositionSpace { get; set; } = new Vector2(100,100);
    private List<Sprite2D> Sprites { get; set; }
    private Random Random { get; set; } = new Random();
    private Node AttachNode { get; set; }
    public IAnimation Setup(Node node)
    {
        TextureSize = new Vector2(CompressedTexture.GetWidth(), CompressedTexture.GetHeight());
        Sprites = new List<Sprite2D>();
        return this;
    }

    public void Update(double delta)
    {
        if (IsRunning)
        {
            foreach (var sprite in Sprites)
            {
                Vector2 randomLowNumberVector = new Vector2((float)Random.NextDouble() * 0.1f, (float)Random.NextDouble() * 0.1f);
                bool? positiveRotation = (bool)sprite.GetMeta("rotation_direction").Obj;

                if (positiveRotation != null && positiveRotation == false)
                {
                    sprite.RotationDegrees -= RotationSpeed * (float)delta;
                }
                else
                {
                    sprite.RotationDegrees += RotationSpeed * (float)delta;
                }

                Vector2? maxScaleSprite = (Vector2)sprite.GetMeta("sprite_max_scale").Obj;
                if (sprite.Scale < MaxScale && (maxScaleSprite == null || sprite.Scale < maxScaleSprite))
                {
                    sprite.Scale += new Vector2(ScaleSpeed, ScaleSpeed) * (float)delta;
                }
                else
                {
                    AnimationEnd?.Invoke(this, new EventArgs());
                }
            }
        }
    }

    public void Stop(bool shouldReset = false)
    {
        IsRunning = false;
    }

    public void Start(bool shouldReset = false)
    {
        if (Sprites.Count <= 0)
        {
            for (int i = 0; i < ExplosionsNumber; i++)
            {
                var sprite = CreateExplosionSprite();
                sprite.Scale = InitialScale;
                Sprites.Add(sprite);
                GD.Print(AttachNode);
                AttachNode.AddChild(sprite);
            }
        }

        PositionSprites(Sprites, TextureSize);
        AddSpriteMetadata(Sprites);
        IsRunning = true;
    }

    public IAnimation Reset()
    {
        foreach (var sprite in Sprites)
        {
            AttachNode.RemoveChild(sprite);
        }
        Sprites.Clear();
        return this;
    }

    private Sprite2D CreateExplosionSprite()
    {
        Sprite2D sprite = new Sprite2D();
        sprite.Texture = CompressedTexture;
        return sprite;
    }

    private void PositionSprites(List<Sprite2D> sprites, Vector2 textureSize)
    {
        foreach (Sprite2D sprite in sprites)
        {
            Vector2 position = new();
            position.X += (textureSize.X / 10 * (float)Random.NextDouble()) * PositionModifier.X;
            position.Y += (textureSize.Y / 10 * (float)Random.NextDouble()) * PositionModifier.Y;

            GD.Print("position: ", position, " texture: ", textureSize);

            if (position.X > MaxPositionSpace.X)
            {
                float difference = position.X - MaxPositionSpace.X;
                position.X = (position.X-difference)/(1 + (float)Random.NextDouble());
            }else if(position.Y > MaxPositionSpace.Y)
            {
                float difference = position.Y - MaxPositionSpace.Y;
                position.Y = (position.Y - difference)/(1 + (float)Random.NextDouble());
            }

            position.X = GetRandomSign(position.X);
            position.Y = GetRandomSign(position.Y);
            
            if(sprite == null)
            {
                GD.Print("Sprite is null..");
            }

            sprite.Position = position;
        }
    }

    private void AddSpriteMetadata(List<Sprite2D> sprites)
    {
        foreach (Sprite2D sprite in sprites)
        {
            int num = Random.Next(0, 2);
            int num50 = Random.Next(70, 101);
            bool positiveSign = true;

            if (num == 0)
            {
                positiveSign = false;
            }

            Vector2 maxScaleSprite = MaxScale * ((float)num50 / 100);
            GD.Print("Scale calculations: ", maxScaleSprite.ToString() , " ", num50, " ", num50/100, " ", MaxScale);
            sprite.SetMeta("rotation_direction", positiveSign);
            sprite.SetMeta("sprite_max_scale", maxScaleSprite);
        }
    }

    private float GetRandomSign(float value)
    {
        int num = Random.Next(0, 2);
        if(num == 0)
        {
            return -value;
        }
        else
        {
            return value;
        }
    }
}
