﻿using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class Enemy : Actor
    {        
        public Enemy(float x, float y, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, icon, color)
        {
            _sprite = new Sprite("sprites/Tanks/tankBlue.png");
            _collisionRadius = 18;
            _collidable = true;
        }
        public Enemy(float x, float y, Color rayColor, char icon = ' ', ConsoleColor color = ConsoleColor.White)
            : base(x, y, rayColor, icon, color)
        {
            _sprite = new Sprite("sprites/Tanks/tankBlue.png");
            _collisionRadius = 15;
            _collidable = true;
        }

        public override void Update(float deltaTime)
        {
            Vector2 displacement = new Vector2(Target.WorldPosition.X - WorldPosition.X, Target.WorldPosition.Y - WorldPosition.Y);
            if (displacement.Magnitude > 7.5f)
                Acceleration = new Vector2(Target.WorldPosition.X - WorldPosition.X, Target.WorldPosition.Y - WorldPosition.Y).Normalized * .3f;
            if (displacement.Magnitude < 7.5f)
                Acceleration -= Acceleration * deltaTime * 4;
            LookAt(Target,deltaTime);
            base.Update(deltaTime);
            if(Game.GetCurrentScene().TestForCollisionWith(this,this.Target.Children[0].Projectiles[0]))  //When Targets Bullet Collides With This, Reset Position
            {
            Random rnd = new Random();
            int xSpawnPosition = rnd.Next(1, 31);
            int ySpawnPosition = rnd.Next(1, 23);
                _translation.m13 = xSpawnPosition;
                _translation.m23 = ySpawnPosition;
                Game.GetCurrentScene().Score.X++;
            }
        }
        public override void Draw()
        {
            if (_sprite != null && IsVisible != false)
            {
                _sprite.Draw(_globalTransform);
            }
            if (_parent != null && IsVisible != false)
            {
                Raylib.DrawLine(
                (int)(WorldPosition.X * 32),
                (int)(WorldPosition.Y * 32),
                (int)((_parent.WorldPosition.X) * 32),
                (int)((_parent.WorldPosition.Y) * 32),
                Color.YELLOW
                );
            }
            base.Draw();
        }
    }
}
