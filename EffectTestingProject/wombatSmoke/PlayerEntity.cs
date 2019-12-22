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
using Nez.Textures;
namespace EffectTestingProject.wombatSmoke
{
    public class PlayerEntity : Entity
    {
        public PlayerEntity(TmxMap map, Texture2D texture)
        {
            var sprites = Sprite.SpritesFromAtlas(texture, 16, 16);
            AddComponent(new SpriteRenderer(sprites[4]).SetOrigin(new Vector2(8f, 16f)));
            AddComponent(new BoxCollider(-4, -16, 8, 16));
            AddComponent(new TiledMapMover(map.GetLayer<TmxLayer>("collision")));
            AddComponent(new PlayerController());
        }
    }
}
