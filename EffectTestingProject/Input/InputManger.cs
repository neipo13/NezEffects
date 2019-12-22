using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectTestingProject.Input
{
    public class InputManager
    {
        protected static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

        public const int MaxInputs = 1;

        private readonly InputHandler[] inputs;

        public InputManager()
        {
            inputs = new InputHandler[MaxInputs];
            for (int i = 0; i < MaxInputs; i++)
            {
                inputs[i] = new InputHandler(i);
            }
        }

        public InputHandler GetInput(int gamepadIndex)
        {
            if (gamepadIndex > (MaxInputs - 1))
            {
                throw new Exception("Asking for a gamepad that cannot exist");
            }

            return inputs[gamepadIndex];
        }
    }
}
