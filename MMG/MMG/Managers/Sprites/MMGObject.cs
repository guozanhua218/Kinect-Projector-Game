using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MMG.Managers.Sprites
{
    public class MMGObject : MMGBasic
    {
        protected Vector2 _lastPosition;

        public float x;
        public float y;

        public float width;
        public float height;

        public Vector2 velocity;
        public Vector2 acceleration;

        public Vector2 offset;
        public Vector2 orgin;
        public float scale;
        public float rotation;

        public Color debugColor = Color.Red;

        public Vector2 Position
        {
            get { return new Vector2(x, y); }
            set { x = value.X; y = value.Y; }
        }

        public MMGObject()
        {
            init(0, 0, 0, 0);
        }

        public MMGObject(float X, float Y)
        {
            init(X, Y, 0, 0);
        }

        public MMGObject(float X, float Y, float Width, float Height)
        {
            init(X, Y, Width, Height);
        }

        private void init(float X, float Y, float Width, float Height)
        {
            x = X;
            y = Y;
            width = Width;
            height = Height;

            rotation = 0;

            _lastPosition = Position;
            velocity = new Vector2();
            acceleration = new Vector2();


            orgin = new Vector2();
            offset = new Vector2();
            scale = 1;
        }

        public virtual void loadContent(ContentManager content)
        {
        }

        public virtual void preupdate(GameTime time)
        {
            _lastPosition = Position;
        }

        public virtual void update(GameTime time)
        {
            x +=            (float)(velocity.X * time.ElapsedGameTime.TotalSeconds) / 2;
            velocity.X +=   (float)(acceleration.X * time.ElapsedGameTime.TotalSeconds);
            x +=            (float)(velocity.X * time.ElapsedGameTime.TotalSeconds) / 2;

            y +=            (float)(velocity.Y * time.ElapsedGameTime.TotalSeconds) / 2;
            velocity.Y +=   (float)(acceleration.Y * time.ElapsedGameTime.TotalSeconds);
            y +=            (float)(velocity.Y * time.ElapsedGameTime.TotalSeconds) / 2;
        }

        public virtual void draw(GameTime time, SpriteBatch spriteBatch)
        {
        }


        public virtual void debug(GameTime time, BasicEffect effect)
        {
            Vector2[] points = getPoints();
            VertexPositionColor[] pointList = new VertexPositionColor[points.Length + 1];


            effect.VertexColorEnabled = true;
            effect.TextureEnabled = false;

            for (int i = 0; i < pointList.Length; i++)
                pointList[i] = new VertexPositionColor(new Vector3(points[i % points.Length], 10), debugColor);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                MMGStatics.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>
                    (PrimitiveType.LineStrip, pointList, 0, points.Length);
            }
            return;
        }

        public Vector2[] getPoints()
        {
            Vector2[] returnValue = new Vector2[4];

            returnValue[0] = new Vector2((float)(x - ((orgin.X - offset.X) * Math.Cos(rotation) + (orgin.Y - offset.Y) * Math.Sin(-rotation)) * scale),
                                    (float)(y - ((orgin.Y - offset.Y) * Math.Cos(-rotation) + (orgin.X - offset.X) * Math.Sin(rotation)) * scale));
            returnValue[1] = new Vector2((float)(x - ((orgin.X - offset.X - width) * Math.Cos(rotation) + (orgin.Y - offset.Y) * Math.Sin(-rotation)) * scale),
                                    (float)(y - ((orgin.Y - offset.Y) * Math.Cos(-rotation) + (orgin.X - offset.X - width) * Math.Sin(rotation)) * scale));
            returnValue[2] = new Vector2((float)(x - ((orgin.X - offset.X - width) * Math.Cos(rotation) + (orgin.Y - offset.Y - height) * Math.Sin(-rotation)) * scale),
                                    (float)(y - ((orgin.Y - offset.Y - height) * Math.Cos(-rotation) + (orgin.X - offset.X - width) * Math.Sin(rotation)) * scale));
            returnValue[3] = new Vector2((float)(x - ((orgin.X - offset.X) * Math.Cos(rotation) + (orgin.Y - offset.Y - height) * Math.Sin(-rotation)) * scale),
                                    (float)(y - ((orgin.Y - offset.Y - height) * Math.Cos(-rotation) + (orgin.X - offset.X) * Math.Sin(rotation)) * scale));

            return returnValue;
        }
    }
}
