using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MMG.Managers.Sprites
{
    public class Test : MMGSprite
    {
        public Test() : base()
        {
            x = y = 50;

            offset.X = offset.Y = 8;

            //color = Color.Red;
        }

        public override void loadContent(ContentManager content)
        {
            loadGraphic(content.Load<Texture2D>("test"), 16, 16, 0);
            addAnimation("idle", new int[]{0,1,2,3}, 0.5f, true);

            base.loadContent(content);
        }

        public override void update(GameTime time)
        {
            play("idle", time, false);

            KeyboardState k = Keyboard.GetState();

            if (k.IsKeyDown(Keys.Up))
                velocity.Y = -50;
            else if (k.IsKeyDown(Keys.Down))
                velocity.Y = 50;
            else
                velocity.Y = 0;

            if (k.IsKeyDown(Keys.Left))
                velocity.X = -50;
            else if (k.IsKeyDown(Keys.Right))
                velocity.X = 50;
            else
                velocity.X = 0;

            if (k.IsKeyDown(Keys.Z))
                scale.Y += (float)time.ElapsedGameTime.TotalSeconds;
            else if (k.IsKeyDown(Keys.X))
                scale.Y -= (float)time.ElapsedGameTime.TotalSeconds;

            scale.X = scale.Y;

            if (k.IsKeyDown(Keys.Space))
                rotation += (float)(time.ElapsedGameTime.TotalSeconds * 1);

            base.update(time);
        }



    }
}
