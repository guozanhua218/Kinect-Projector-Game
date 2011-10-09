using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace MMG.Managers.Sprites
{
    public interface MMGBasic
    {
        void loadContent(ContentManager content);
        void preupdate(GameTime time);
        void update(GameTime time);
        void draw(GameTime time, SpriteBatch spriteBatch);
        void debug(GameTime time, BasicEffect effect);
    }
}
