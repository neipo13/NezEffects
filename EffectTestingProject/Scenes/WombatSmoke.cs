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
using EffectTestingProject.wombatSmoke;

namespace EffectTestingProject.Scenes
{
    public class WombatSmoke : Scene
    {
        TmxMap map;
        public override void Initialize()
        {
            base.Initialize();
            ClearColor = Color.White;
            //LemsHitEffectEntity.InitializePool(this);
            this.SetDesignResolution(256, 144, SceneResolutionPolicy.BestFit);
            //setup the map
            var tiledEntity = CreateEntity("tiled-map-entity");
            map = Content.LoadTiledMap("Content/tiled/wombatSmoke.tmx");
            var tiledMapRenderer = tiledEntity.AddComponent(new TiledMapRenderer(map, "collision"));
            tiledMapRenderer.SetLayersToRender(new[] { "collision", "decoration"});

            // render below/behind everything else. our player is at 0 and projectile is at 1.
            tiledMapRenderer.RenderLayer = 1;


            Core.Schedule(1f, SpawnPlayer);
        }

        public void SpawnPlayer(ITimer timer)
        {
            var spawnObject = map.GetObjectGroup("spawn").Objects.FirstOrDefault();
            var texture = Content.Load<Texture2D>("img/wombatSmoke");
            var player = new PlayerEntity(map, texture);
            player.Position = new Vector2(spawnObject.X, spawnObject.Y - 16);
            AddEntity(player);
        }

        //public override void Update()
        //{
        //    base.Update();
        //    if (Nez.Input.LeftMouseButtonPressed)
        //    {
        //        var e = new Entity();
        //        AddEntity(e);
        //        e.Position = Nez.Input.MousePosition;
        //        e.AddComponent(new wombatSmoke.SmokeComponent());
        //    }
        //}
    }
}
