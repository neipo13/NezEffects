using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Splines;
using Nez.Sprites;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectTestingProject.LemsDepthArm
{
    public class DepthArmComponent : Component, IUpdatable
    {
        public Vector2 targetPosition;
        SpriteRenderer[] armChunks;
        public int numberOfChunks = 10;

        public float baseScale = 3f;
        public float maxScale = 10f;

        public float currentEndScale;


        public float elbowHeightMax = 150f;
        public int elbowIndex;



        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            var texture = Entity.Scene.Content.Load<Texture2D>("img/depthArm");
            var sprites = Sprite.SpritesFromAtlas(texture, 32, 32);
            currentEndScale = baseScale;
            elbowIndex = (int)(numberOfChunks * .5f);

            armChunks = new SpriteRenderer[numberOfChunks];
            //do all but forward most to one sprite
            for (int i = 0; i < numberOfChunks - 1; i++)
            {
                armChunks[i] = new SpriteRenderer(sprites[1]);
                armChunks[i].LayerDepth = (float)(numberOfChunks - i) / (float)numberOfChunks;
                armChunks[i].followsParentEntityScale = false;
                armChunks[i].Scale = new Vector2(baseScale);
                Entity.AddComponent(armChunks[i]);
            }
            //most forward gets the "eye"
            var lastPos = numberOfChunks - 1;
            armChunks[lastPos] = new SpriteRenderer(sprites[2]);
            armChunks[lastPos].LayerDepth = 0;
            armChunks[lastPos].followsParentEntityScale = false;
            armChunks[lastPos].Scale = new Vector2(baseScale);
            Entity.AddComponent(armChunks[lastPos]);
        }


        public void Update()
        {

            //scale
            var scaleToGrow = currentEndScale - baseScale;
            var scalePerChunk = scaleToGrow / (numberOfChunks - 1);
            var currentScale = baseScale;

            // straighten elbow based on scale %
            // at lowest % it is bent to max
            // at highest % it is fully straight
            var currentElbowHeight = elbowHeightMax * (1 - ((currentEndScale - baseScale) / (maxScale - baseScale)));
            var midpoint = (targetPosition - Entity.Position) / 2f;
            var elbowLocation = new Vector2(midpoint.X, midpoint.Y - currentElbowHeight);

            for (int i = 0; i < numberOfChunks; i++)
            {
                float t = (float)i / (float)(numberOfChunks - 1f);
                var pos = Bezier.GetPoint(Vector2.Zero, new Vector2(0, elbowLocation.Y), elbowLocation, targetPosition - Entity.Position, t);
                currentScale += scalePerChunk;
                armChunks[i].LocalOffset = pos;
                armChunks[i].Scale = new Vector2(currentScale);

            }
        }
    }
}
