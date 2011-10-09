using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MMG.Managers.Sprites;
using MMG.KinectManager;

namespace MMG.MMGGame
{
    class GameGroup : MMGGroup
    {
        MMGSprite b;

        DepthTexture depthTexture;

        public GameGroup()
            : base()
        {
            b = new Test();
                add(b);
                b.x = 200;
                b.scale = 4;
                b.orgin.X = 0;



            //b = new Test();
            //    add(b);
            //    b.x = 400;
            //    b.scale = 2;
            //    b.orgin.Y = 0;
            //b = new Test();
            //    add(b);
        }

        public override void loadContent(ContentManager content)
        {
            base.loadContent(content);

            depthTexture = new DepthTexture(content.Load<Texture2D>("depth"), 640, 480);
        }

        public override void update(GameTime time)
        {
            b.play("1", time, false);

            List<Color> tmp = depthTexture.compareObject(b.getPoints());

            foreach (Color c in tmp)
            {
                Console.Out.WriteLine(c);
            }

            //

            base.update(time);
        }

        public override void draw(GameTime time, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(depthTexture.DepthImage, new Rectangle(0, 0, 640, 480), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);

            base.draw(time, spriteBatch);
        }
    }
}
