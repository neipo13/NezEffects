using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez.Sprites;

namespace EffectTestingProject.LemsHitEffect
{
    public class LemsHitEffectEntity : Entity
    {
        private static FastList<LemsHitEffectEntity> pool = new FastList<LemsHitEffectEntity>(10);
        private static int poolIterator = 0;

        public static void InitializePool(Scene scene, int num = 10)
        {
            pool.Clear(); // clear the pool as this may use items from a previous scene
            while(pool.Length < num)
            {
                var item = new LemsHitEffectEntity();
                scene.AddEntity(item);
                pool.Add(item);
            }
        }

        public static LemsHitEffectEntity GetNextAvailable()
        {
            var next = pool.Buffer[poolIterator];
            poolIterator++;
            return next;
        }

        public static void UnloadPool()
        {
            pool.Clear(); // hopefully this helps clear memory?
        }

        public bool InUse => lheComponent.Enabled;
        private LemsHitEffectComponent lheComponent;

        public LemsHitEffectEntity():base("LemsHitEffect")
        {
            lheComponent = new LemsHitEffectComponent();
            AddComponent(lheComponent);
        }

        public void StartEffect(int numberOfRays = 3)
        {
            lheComponent.Enabled = true;
            lheComponent.Start(numberOfRays);
        }
    }
}
