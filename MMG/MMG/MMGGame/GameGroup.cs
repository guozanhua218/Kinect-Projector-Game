using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MMG.Managers.Sprites;

namespace MMG.MMGGame
{
    class GameGroup : MMGGroup
    {
        MMGSprite b;

        public GameGroup()
            : base()
        {

            b = new Test();
                add(b);
                b.x = 200;
                b.scale = 4;
                b.orgin.X = 0;
            b = new Test();
                add(b);
                b.x = 400;
                b.scale = 2;
                b.orgin.Y = 0;
            b = new Test();
                add(b);
             
        }

        public override void update(Microsoft.Xna.Framework.GameTime time)
        {
            b.play("1", time, false);

            base.update(time);
        }
    }
}
