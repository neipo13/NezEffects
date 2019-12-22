using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectTestingProject.Input
{
    public class InputMapping
    {
        public int Index { get; set; }
        public int[] Left { get; set; }
        public int[] Right { get; set; }
        public int[] Up { get; set; }
        public int[] Down { get; set; }
        public int[] JumpKey { get; set; }
        public int[] JumpButton { get; set; }
        public int[] AttackKey { get; set; }
        public int[] AttackButton { get; set; }
        public int[] DashKey { get; set; }
        public int[] DashButton { get; set; }
        public int[] ConfirmKey { get; set; }
        public int[] ConfirmButton { get; set; }
        public int[] BackKey { get; set; }
        public int[] BackButton { get; set; }
    }
}
