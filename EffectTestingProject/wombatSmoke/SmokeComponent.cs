using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectTestingProject.wombatSmoke
{
    public class SmokeComponent : Component, IUpdatable
    {
        public float lifetimer = 0f;
        public float LIFETIME = 0f;

        public SpriteRenderer sprite;
        public Color color = Color.Black;

        public float speed = 70f;

        public SmokeComponent(): base()
        {
            this.Enabled = false;
            init();
        }

        private void init()
        {
            lifetimer = 0;
            LIFETIME = Nez.Random.Range(0.5f, 1.5f);
            speed += Nez.Random.Range(-15f, 15f);
        }


        public void Enable(Vector2 localPostion)
        {
            init();
            this.Enabled = true;
            if(sprite != null)
            {
                sprite.LocalOffset = localPostion;
                sprite.Color = color;
            }
            
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            var texture = this.Entity.Scene.Content.Load<Texture2D>("img/wombatSmoke");
            var sprites = Sprite.SpritesFromAtlas(texture, 16, 16);
            sprite = this.Entity.AddComponent(new SpriteRenderer(sprites[6]).SetOrigin(new Vector2(8f, 16f)));
            sprite.RenderLayer = 10;
        }

        public void Update()
        {
            lifetimer += Time.DeltaTime;
            sprite.Color = color * (1f - (lifetimer / LIFETIME));
            sprite.LocalOffset += new Vector2(speed, 0f) * Time.DeltaTime;
            if(lifetimer > LIFETIME)
            {
                this.Enabled = false;
                Pool<SmokeComponent>.Free(this);
            }
        }
    }
}
