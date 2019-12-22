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

namespace EffectTestingProject.Scenes
{
    public class LemsHitEffect : SelectScene
    {
        public override void Initialize()
        {
            base.Initialize();
            ClearColor = Color.Black;
            //LemsHitEffectEntity.InitializePool(this);
        }


        public override void Update()
        {
            base.Update();
            if (Nez.Input.LeftMouseButtonPressed)
            {
                SpawnHitEffect(Nez.Input.MousePosition);
            }
        }

        private void SpawnHitEffect(Vector2 position)
        {
            var effect = new LemsHitEffectEntity();
            effect.Position = position;
            AddEntity(effect);
            effect.StartEffect(Nez.Random.Range(3, 5));
        }
    }
}
