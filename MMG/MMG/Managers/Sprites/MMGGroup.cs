using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MMG.Managers.Sprites
{
    public class MMGGroup : MMGBasic
    {
        public List<MMGBasic> members;


        public MMGGroup()
            : base()
        {
            members = new List<MMGBasic>();
        }

        public virtual void loadContent(ContentManager content)
        {
            foreach (MMGBasic b in members)
            {
                b.loadContent(content);
            }
        }
        public virtual void preupdate(GameTime time)
        {
            foreach (MMGBasic b in members)
            {
                b.preupdate(time);
            }
        }
        public virtual void update(GameTime time)
        {
            foreach (MMGBasic b in members)
            {
                b.update(time);
            }
        }
        public virtual void draw(GameTime time, SpriteBatch spriteBatch)
        {
            foreach (MMGBasic b in members)
            {
                b.draw(time, spriteBatch);
            }
        }

        public virtual void debug(GameTime time, BasicEffect effect)
        {
            foreach (MMGBasic b in members)
            {
                b.debug(time, effect);
            }
        }


        public void add(MMGBasic basic)
        {
            members.Add(basic);
        }

        public void remove(MMGBasic basic)
        {
            members.Remove(basic);
        }
    }
}
