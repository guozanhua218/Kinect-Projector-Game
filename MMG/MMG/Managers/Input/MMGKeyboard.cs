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
    public class MMGKeyboard
    {
        private static MMGKeyboard secretKeyboard;

        public static MMGKeyboard getKeyboard()
        {
            return secretKeyboard;
        }
        
        protected Keys[] current;
        protected Keys[] previous;

        protected Keys[] pressedArray;
        protected Keys[] releasedArray;

        protected List<Keys> pressed;
        protected List<Keys> released;

        protected Dictionary<Keys, float> timer;
        protected Dictionary<string, Keys> names;

        protected KeyboardState keyboard;

        public MMGKeyboard()
        {
            secretKeyboard = this;

            pressed = new List<Keys>();
            released = new List<Keys>();

            timer = new Dictionary<Keys, float>();
            names = new Dictionary<string, Keys>();

            keyboard = Keyboard.GetState();
            previous = current = keyboard.GetPressedKeys();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(float gameTime)
        {
            previous = current;

            keyboard = Keyboard.GetState();
            current = keyboard.GetPressedKeys();

            pressed.Clear();
            released.Clear();

            for (int i = 0; i < current.Length || i < previous.Length; i++)
            {
                if (i < current.Length)
                {
                    if (!previous.Contains(current[i]))
                    {
                        pressed.Add(current[i]);
                        timer.Add(current[i], 0);
                    }
                    else
                    {
                        timer[current[i]] += gameTime;
                    }
                }

                if (i < previous.Length &&!current.Contains(previous[i]))
                {
                    released.Add(previous[i]);
                    timer.Remove(previous[i]);
                }

                //Console.Out.WriteLine("s");
            

                pressedArray = pressed.ToArray();
                releasedArray = released.ToArray();
            }
        }

        public Boolean isDown(Keys key)
        {
            return current.Contains(key);
        }

        public Boolean isDown(String name)
        {
            if (names.ContainsKey(name))
                return isDown(names[name]);
            return false;
        }

        public Boolean isJustPressed(Keys key)
        {
            return pressed.Contains(key);
        }

        public Boolean isJustPressed(String name)
        {
            if (names.ContainsKey(name))
                return isJustPressed(names[name]);
            return false;
        }

        public Boolean isReleased(Keys key)
        {
            return released.Contains(key);
        }

        public Boolean isReleased(String name)
        {
            if (names.ContainsKey(name))
                return isReleased(names[name]);
            return false;
        }

        public float isDownTime(Keys key)
        {
            if (timer.ContainsKey(key))
                return timer[key];
            return 0;
        }

        public float isDownTime(String name)
        {
            if (names.ContainsKey(name))
                return isDownTime(names[name]);
            return 0;
        }

        public void nameKey(String name, Keys key)
        {
            names.Add(name, key);
        }




    }
}
