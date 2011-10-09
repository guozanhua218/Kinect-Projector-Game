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

namespace MMG.Managers
{
    public class MMGStatics
    {
        public static GraphicsDevice GraphicsDevice;
        public static Texture2D blankTexture;

        public static bool debug = true;

        public static int Width;
        public static int Height;

        public MMGStatics(GraphicsDevice graphicsDevice, int width, int height)
        {
            GraphicsDevice = graphicsDevice;

            Width = width;
            Height = height;

            blankTexture = new Texture2D(graphicsDevice, 1, 1);
                blankTexture.SetData<Color>(new Color[]{Color.White});
        }
    }
}
