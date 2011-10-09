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

namespace MMG.KinectManager
{
    public class DepthTexture
    {
        protected Texture2D depthImage;

        protected int width;
        protected int height;


        public Texture2D DepthImage
        {
            get { return depthImage; }
            set { depthImage = value; }
        }



        public DepthTexture(Texture2D texture, int width, int height)
        {
            depthImage = texture;
            this.width = width;
            this.height = height;
        }
        public List<Color> compareObject(Vector2[] points)
        {
            float scaleWidth = depthImage.Width * 1.0f / width;
            float scaleHeight = depthImage.Height * 1.0f / height;

            List<Color> returnValue = new List<Color>();

            Color[] colorData = new Color[depthImage.Width * depthImage.Height];
            depthImage.GetData<Color>(colorData);

            foreach (Vector2 v in points)
            {
                int x = (int)(v.X * scaleWidth);
                int y = (int)(v.Y * scaleHeight);

                if (!between(0, depthImage.Height, y) || !between(0, depthImage.Width, x))
                {
                    returnValue.Add(Color.Black);
                }
                else
                {
                    returnValue.Add(colorData[(y * depthImage.Width + x)]);
                }
            }

            return returnValue;
        }

        public List<Color> compareObjectOld(Vector2[] points)
        {
            List<Color> returnValue = new List<Color>();

            float scaleWidth = depthImage.Width * 1.0f / width;
            float scaleHeight = depthImage.Height * 1.0f / height;

            float minX = points[0].X;
            float maxX = points[0].X;

            foreach (Vector2 v in points)
            {
                if (v.X < minX)
                    minX = v.X;
                if (v.X > maxX)
                    maxX = v.X;
            }
            
            float tmpCounter = 0;
            Vector2? tmpVector;

            //ac
            Color[] colorData = new Color[depthImage.Width * depthImage.Height];
            depthImage.GetData<Color>(colorData);

            for (int i = (int)(minX * scaleWidth); i < (maxX * scaleWidth); i++)
            {
                int[] lines = new int[2];
                tmpCounter = 0;
                for(int j = 0; j < points.Length; j++)
                {
                    if(between(points[j%points.Length].X, points[(j + 1)%points.Length].X, i))
                    {
                        if(tmpCounter <= 0)
                        {
                            lines[0] = j;
                            tmpCounter++;
                        }
                        else
                            lines[1] = j;
                    }
                }
                Vector2[] xLine = new Vector2[] {new Vector2(i ,0), new Vector2(i ,10)};

                Vector2[] tmpLine;

                tmpLine = new Vector2[] {points[lines[0]%points.Length], points[(lines[0] + 1)%points.Length]};
                tmpVector = findIntersect(xLine, tmpLine);
                float minY = ((Vector2)(tmpVector)).Y;

                tmpLine = new Vector2[] {points[lines[1]%points.Length], points[(lines[1] + 1)%points.Length]};
                tmpVector = findIntersect(xLine, tmpLine);
                float maxY = ((Vector2)(tmpVector)).Y;

                if(minY > maxY)
                {
                    tmpCounter = minY;
                    minY = maxY;
                    maxY = tmpCounter;
                }
                
                for (int j = (int)(minY * scaleHeight); j < (maxY * scaleHeight); j++)
                {
                    if (!between(0, depthImage.Height, j) || !between(0, depthImage.Width, i))
                    {
                        returnValue.Add(Color.Black);
                        break;
                    }
                    if (j * depthImage.Width + i < colorData.Length)
                    {
                        //Console.Out.WriteLine(i + ", " + j);
                        returnValue.Add(colorData[j * depthImage.Width + i]);
                    }
                }
            }

            return returnValue;
        }

        public static bool between(float min, float max, float number)
        {
            return (min > number) ^ (max > number);
        }

        public static Vector2? findIntersect(Vector2[] line1, Vector2[] line2)
        {
            float[] gradient = new float[2];
            float[] yIntercept = new float[2];
            Vector2 intersect = new Vector2();
            
            gradient[0] = getGradient(line1);
            gradient[1] = getGradient(line2);

            if(gradient[0] == gradient[1])
                return null;
            else if (float.IsInfinity(gradient[0]))
            {
                intersect.X = line1[0].X;
                yIntercept[1] = line2[0].Y - gradient[1] * line2[0].X;
                intersect.Y = (intersect.X * gradient[1]) + yIntercept[1];

            }
            else if (float.IsInfinity(gradient[1]))
            {
                intersect.X = line2[0].X;
                yIntercept[0] = line1[0].Y - gradient[0] * line1[0].X;
                intersect.Y = (intersect.X * gradient[0]) + yIntercept[0];
            }
            else
            {
                yIntercept[0] = line1[0].Y - gradient[0] * line1[0].X;
                yIntercept[1] = line2[0].Y - gradient[1] * line2[0].X;

                intersect.X = (yIntercept[0] - yIntercept[1]) / (gradient[1] - gradient[0]);
                intersect.Y = (intersect.X * gradient[0]) + yIntercept[0];
            }

            return intersect;
        }

        public static float getGradient(Vector2[] line)
        {
            return (line[1].Y - line[0].Y) / (line[1].X - line[0].X);
        }
    }


}
