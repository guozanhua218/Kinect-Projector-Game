using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using MMG.Managers.Sprites;
using MMG.Managers.Input;

namespace MMG.MMGGame
{
    public class Test : MMGSprite
    {
        public Test() : base()
        {
            x = y = 33;

            offset.X = offset.Y = 2;

            orgin.X = orgin.Y = 8;
            width = height = 14;

            //color = Color.Red;
        }

        public override void loadContent(ContentManager content)
        {
            loadGraphic(content.Load<Texture2D>("test"), 16, 16, 0);
            addAnimation("0", new int[] { 0 }, 0.5f, true);
            addAnimation("1", new int[] { 0, 1, 2, 3 }, 0.5f, true);

            width = height = 12;
            base.loadContent(content);
        }

        public override void update(GameTime time)
        {
            if(_currentAnimation == null)
                play("0", time, false);

            //KeyboardState k = Keyboard.GetState();

            if (MMGKeyboard.getKeyboard().isDown(Keys.Up))
                velocity.Y = -50;
            else if (MMGKeyboard.getKeyboard().isDown(Keys.Down))
                velocity.Y = 50;
            else
                velocity.Y = 0;

            if (MMGKeyboard.getKeyboard().isDown(Keys.Left))
                velocity.X = -50;
            else if (MMGKeyboard.getKeyboard().isDown(Keys.Right))
                velocity.X = 50;
            else
                velocity.X = 0;

            if (MMGKeyboard.getKeyboard().isDown(Keys.Z))
                scale += (float)time.ElapsedGameTime.TotalSeconds;
            else if (MMGKeyboard.getKeyboard().isDown(Keys.X))
                scale -= (float)time.ElapsedGameTime.TotalSeconds;

            if (MMGKeyboard.getKeyboard().isDown(Keys.Space))
                rotation += (float)(time.ElapsedGameTime.TotalSeconds * 1);

            base.update(time);
        }



    }
}
