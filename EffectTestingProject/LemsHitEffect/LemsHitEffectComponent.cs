using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;
using Nez;

namespace EffectTestingProject.LemsHitEffect
{
    public class LemsHitEffectComponent : RenderableComponent, IUpdatable
    {
        private float lifeTimer = 0f;
        public const float LIFETIME = 0.3f;

        public float circleRadius = 400f;
        public float circleThickness = 200f;
        public int circleResolution = 2000;
        public Color color = Color.White;

        public float lineLength = 1200f;
        public float lineThickness = 60f;
        public float linesPerSide = 12;
        public float maxLineTheta = (float)Math.PI / 24f;

        private float[] lineAngles;
        

        public override RectangleF Bounds => new RectangleF(this.Entity.Position, new Vector2(NezGame.designWidth, NezGame.designHeight));

        public void Start(int numberOfLines = 3)
        {
            this._isVisible = true;
            lifeTimer = 0f;
            lineAngles = new float[numberOfLines];
            for(int i = 0; i < numberOfLines; i++)
            {
                float angle= Nez.Random.NextAngle();

                lineAngles[i] = Nez.Random.NextAngle();
            }
        }

        //private bool IsValidAngleToAdd(float angle, int totalNumAngles)
        //{
        //    if (lineAngles.Any(a => Math.Abs(angle - a) < ((float)Math.PI * 2f / totalNumAngles))) return false;
        //    return true;
        //}


        public void Update()
        {
            lifeTimer += Time.DeltaTime;
            if(lifeTimer > LIFETIME)
            {
                Enabled = false;
                this._isVisible = false;
            }
        }        

        public override void Render(Batcher batcher, Camera camera)
        {
            var lifePercent = 1 - (lifeTimer / LIFETIME);
            //circle effect
            batcher.DrawCircle(this.Entity.Position, circleRadius * (lifeTimer / LIFETIME), color, circleThickness * lifePercent, circleResolution);
            //batcher.DrawPolygon(this.Entity.Position, CreateCircle(radius * (lifeTimer / LIFETIME), 100), color, true, thickness * (1 - (lifeTimer / LIFETIME)));
            //lines
            if (lineAngles != null && lineAngles.Length > 0)
            {
                for (int i = 0; i < lineAngles.Length; i++)
                {
                    //center line
                    batcher.DrawLineAngle(this.Entity.Position, lineAngles[i], lineLength, color, lineThickness * lifePercent);

                    var maxTheta = maxLineTheta * lifePercent;
                    //left lines
                    for (int j = 1; j< linesPerSide; j++)
                    {
                        var theta = lineAngles[i] - (maxTheta * j / linesPerSide);
                        batcher.DrawLineAngle(this.Entity.Position, theta, lineLength, color, lineThickness * lifePercent);
                    }
                    //right lines
                    for (int j = 1; j < linesPerSide; j++)
                    {
                        var theta = lineAngles[i] + (maxTheta * j / linesPerSide);
                        batcher.DrawLineAngle(this.Entity.Position, theta, lineLength, color, lineThickness * lifePercent);
                    }
                }

            }
        }
    }
}
