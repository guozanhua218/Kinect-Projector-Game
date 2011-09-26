using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace MMG.Managers.Input
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MMGGamepad
    {
        private Buttons[] current;
        protected Buttons[] previous;
        protected List<Buttons> pressed;
        protected List<Buttons> released;

        protected Dictionary<Buttons, float> timer;
        protected Dictionary<string, Buttons> names;

        protected GamePadState gamepad;
        protected GamePadState lastState;
        protected bool isconnected;

        protected PlayerIndex playerIndex;

        public MMGGamepad(PlayerIndex playerNum)
        {
            playerIndex = playerNum;

            pressed = new List<Buttons>();
            released = new List<Buttons>();

            timer = new Dictionary<Buttons, float>();
            names = new Dictionary<string, Buttons>();

            lastState = gamepad = GamePad.GetState(playerIndex);
            //previous = current = gamepad.
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(float gameTime)
        {
            previous = current;
        }


        public void nameKey(String name, Buttons button)
        {
            names.Add(name, button);
        }




    }
}
