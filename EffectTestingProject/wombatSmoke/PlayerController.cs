using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;
using EffectTestingProject.LemsHitEffect;
using Nez.Tiled;
using EffectTestingProject.Input;

namespace EffectTestingProject.wombatSmoke
{
    public class PlayerController : Component, IUpdatable
    {
        BoxCollider collider;
        TiledMapMover mover;
        TiledMapMover.CollisionState collisionState = new TiledMapMover.CollisionState();
        Vector2 velocity;
        float moveSpeed = 100f;
        const float gravity = 900f;
        readonly float jumpHeight = 15 * 3.5f; //tilesize * tiles high
        InputHandler input;

        bool isGrounded => collisionState.Below;

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            //initialize smoke component pool
            var amt = 200;

            Pool<SmokeComponent>.WarmCache(amt);
            var smokeEntity = new Entity();
            this.Entity.Scene.AddEntity(smokeEntity);
            //loop & add all components to this entity
            for(int i = 0; i < amt; i++)
            {
                var sc = Pool<SmokeComponent>.Obtain();
                smokeEntity.AddComponent(sc);
                sc.Enabled = false;
                Pool<SmokeComponent>.Free(sc);
            }

            mover = Entity.GetComponent<TiledMapMover>();
            collider = Entity.GetComponent<BoxCollider>();


            velocity = Vector2.Zero;
            input = InputManager.Instance.GetInput(0); // just grab player 1 for now
        }
        public void Update()
        {             
            velocity.X = moveSpeed * input.XInput;
            velocity.Y += gravity * Time.DeltaTime;
            if (input.JumpButton.IsPressed)
            {
                if (isGrounded) Jump();
            }
            mover.Move(velocity * Time.DeltaTime , collider, collisionState);
            //don't let gravity build while you're grounded
            if (isGrounded) velocity.Y = 0f;

            //instantiate some smokes
            for(int i = 0; i < 3; i++)
            {
                var sc = Pool<SmokeComponent>.Obtain();
                sc.Enable(this.Entity.Position + new Vector2(-15f, 0f) + new Vector2(Nez.Random.Range(-3f, 10f), Nez.Random.Range(-10f, 10f)));
            }
        }
        void Jump()
        {
            velocity.Y = -Mathf.Sqrt(2 * jumpHeight * gravity);
        }
    }
}
