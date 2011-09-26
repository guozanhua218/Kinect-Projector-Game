using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MMG.Managers.Sprites;
using MMG.MMGGame;

namespace MMG.Managers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MMGSpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected List<MMGBasic> _members;

        public MMGSpriteManager(Game game)
            : base(game)
        {
            _members = new List<MMGBasic>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            add(new GameGroup());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (MMGBasic b in _members)
            {
                b.loadContent(Game.Content);
            }

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            foreach (MMGBasic b in _members)
            {
                b.preupdate(gameTime);
            }
            foreach (MMGBasic b in _members)
            {
                b.update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointWrap, null, null);

            foreach (MMGBasic b in _members)
            {
                b.draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void add(MMGBasic basic)
        {
            _members.Add(basic);
        }

        public void remove(MMGBasic basic)
        {
            _members.Remove(basic);
        }
    }

}
