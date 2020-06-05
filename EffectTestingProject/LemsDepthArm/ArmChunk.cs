using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectTestingProject.LemsDepthArm
{
    public class ArmChunk
    {
        //public ArmChunk previousChunk;
        //public ArmChunk nextChunk;
        public int forwardDepth; // which chunk am i 
        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer spriteRenderer
        {
            get
            {
                return _spriteRenderer;
            }
            set
            {
                _spriteRenderer = value;
                _spriteRenderer.LayerDepth = forwardDepth;
            }
        }

        public ArmChunk(int depth)
        {

        }
    }
}
