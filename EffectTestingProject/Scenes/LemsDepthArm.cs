using EffectTestingProject.LemsDepthArm;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectTestingProject.Scenes
{
    public class LemsDepthArm : Scene
    {
        DepthArmComponent depthArm;
        Entity armEntity;


        DepthArmComponent depthArm2;

        DepthArmComponent depthArm3;

        float moveSpeed = 500f;
        int numChunks = 500;


        public override void Initialize()
        {
            base.Initialize();
            ClearColor = Color.Black;

            armEntity = new Entity("Arm");
            armEntity.Position = new Vector2(NezGame.designWidth * 0.25f, NezGame.designHeight / 2f);
            depthArm = new DepthArmComponent();
            depthArm.numberOfChunks = numChunks; 
            armEntity.AddComponent(depthArm);
            AddEntity(armEntity);



            var armEntity2 = new Entity("Arm2");
            armEntity2.Position = new Vector2(NezGame.designWidth * 0.75f, NezGame.designHeight / 2f);
            depthArm2 = new DepthArmComponent();
            depthArm2.numberOfChunks = numChunks; 
            armEntity2.AddComponent(depthArm2);
            AddEntity(armEntity2);


            var armEntity3 = new Entity("Arm3");
            armEntity3.Position = new Vector2(NezGame.designWidth * .5f, NezGame.designHeight * 0.4f);
            depthArm3 = new DepthArmComponent();
            depthArm3.numberOfChunks = numChunks; 
            armEntity3.AddComponent(depthArm3);
            AddEntity(armEntity3);
        }


        public override void Update()
        {
            base.Update();
            if (Nez.Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                armEntity.Position += new Vector2(-1, 0) * moveSpeed * Time.DeltaTime;
            }
            if (Nez.Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                armEntity.Position += new Vector2(1, 0) * moveSpeed * Time.DeltaTime;
            }
            if (Nez.Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                armEntity.Position += new Vector2(0, -1) * moveSpeed * Time.DeltaTime;
            }
            if (Nez.Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                armEntity.Position += new Vector2(0, 1) * moveSpeed * Time.DeltaTime;
            }
            depthArm.targetPosition = Nez.Input.MousePosition;
            depthArm.currentEndScale = depthArm.baseScale + ((Nez.Input.MousePosition.Y / NezGame.designHeight) * (depthArm.maxScale - depthArm.baseScale));


            depthArm2.targetPosition = Nez.Input.MousePosition;
            depthArm2.currentEndScale = depthArm2.baseScale + ((Nez.Input.MousePosition.Y / NezGame.designHeight) * (depthArm2.maxScale - depthArm2.baseScale));


            depthArm3.targetPosition = Nez.Input.MousePosition;
            depthArm3.currentEndScale = depthArm3.baseScale + ((Nez.Input.MousePosition.Y / NezGame.designHeight) * (depthArm3.maxScale - depthArm3.baseScale));
        }
    }
}
