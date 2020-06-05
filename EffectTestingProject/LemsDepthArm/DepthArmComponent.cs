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
            // go ahead and load the sprites in here, maybe not the best way to do it but this is just the fastest way to get going
            var texture = Entity.Scene.Content.Load<Texture2D>("img/depthArm");
            var sprites = Sprite.SpritesFromAtlas(texture, 32, 32);

            //set some defaults for the scale/index
            currentEndScale = baseScale;
            elbowIndex = (int)(numberOfChunks * .5f);

            //now we need to create all the sprite renderers and set up their depth and make sure they can scale properly
            armChunks = new SpriteRenderer[numberOfChunks];
            //do all but forward most to one sprite
            for (int i = 0; i < numberOfChunks - 1; i++)
            {
                armChunks[i] = new SpriteRenderer(sprites[1]);
                armChunks[i].LayerDepth = (float)(numberOfChunks - i) / (float)numberOfChunks;
                // if you have errors here you may need to poke into the sprite renderer class and make it so it can have it's own scale instead of just following entity scale
                // I made it so it follows entity scale by default but that can be turned off (as we do here)
                armChunks[i].followsParentEntityScale = false;
                armChunks[i].Scale = new Vector2(baseScale);
                Entity.AddComponent(armChunks[i]);
            }
            //most forward gets the "eye" so pull that out so it gets the right sprite
            var lastPos = numberOfChunks - 1;
            armChunks[lastPos] = new SpriteRenderer(sprites[2]);
            armChunks[lastPos].LayerDepth = 0; // no need for calculation here just make it be in the front
            armChunks[lastPos].followsParentEntityScale = false;
            armChunks[lastPos].Scale = new Vector2(baseScale);
            Entity.AddComponent(armChunks[lastPos]);
        }


        public void Update()
        {
            //calculate scale (just grow by set size each chunk)
            var scaleToGrow = currentEndScale - baseScale; // how much we need to grow total
            var scalePerChunk = scaleToGrow / (numberOfChunks - 1); // how much to grow per chunk
            var currentScale = baseScale; // start off at base so we grow from there toward currentEndScale

            // straighten elbow based on scale %
            // at lowest % it is bent to max
            // at highest % it is fully straight
            var currentElbowHeight = elbowHeightMax * (1 - ((currentEndScale - baseScale) / (maxScale - baseScale)));
            //then place the elbow in the middle but up by currentElbowHeight
            var midpoint = (targetPosition - Entity.Position) / 2f;
            var elbowLocation = new Vector2(midpoint.X, midpoint.Y - currentElbowHeight);

            //finally loop those chunks and place them in their positions
            for (int i = 0; i < numberOfChunks; i++)
            {
                // use the Nez bezier calculation to figure out where to be
                float t = (float)i / (float)(numberOfChunks - 1f); // t must be between 0 (base) and 1 (targetLocation)
                var pos = Bezier.GetPoint(Vector2.Zero, new Vector2(0, elbowLocation.Y), elbowLocation, targetPosition - Entity.Position, t);
                //also scale up each chunk according to the calculation above
                currentScale += scalePerChunk;
                armChunks[i].LocalOffset = pos;
                armChunks[i].Scale = new Vector2(currentScale);

            }
        }
    }
}
