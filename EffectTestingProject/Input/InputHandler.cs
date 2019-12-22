using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Nez;

namespace EffectTestingProject.Input
{
    public class InputHandler
    {
        VirtualIntegerAxis _xAxisInput;
        VirtualIntegerAxis _yAxisInput;

        public VirtualButton JumpButton { get; private set; }
        public VirtualButton DashButton { get; private set; }
        public VirtualButton ConfirmButton { get; private set; }
        public VirtualButton BackButton { get; private set; }

        Vector2 _axialInput; //utility vec2 to hold input values without constantly creating/destroying vec2s
        public int gamepadIndex = 0;
        public InputMapping mapping;
        public Vector2 LeftStickInput
        {
            get
            {
                _axialInput.X = _xAxisInput.Value;
                _axialInput.Y = _yAxisInput.Value;
                return _axialInput;
            }
        }

        private const float A_CONST = -5f;
        private Vector2 _eightDirection = new Vector2(1f, 0f);
        public Vector2 LeftStick8DirectionLocked
        {
            get
            {
                if (LeftStickInput == Vector2.Zero) return _eightDirection;
                var temp = LeftStickInput;
                temp.Normalize();
                temp = temp * 2f;
                temp.X = Mathf.Round(temp.X);
                temp.Y = Mathf.Round(temp.Y);
                temp = temp / 2f;
                if (temp.X == 0.5f) temp.X = 0.75f;
                else if (temp.X == -0.5f) temp.X = -0.75f;
                if (temp.Y == -0.5f) temp.Y = -0.75f;
                //if (temp.Y == 0.5f) temp.Y = 0.75f;
                _eightDirection = temp;
                return temp;
            }
        }

        public float XInput => _xAxisInput.Value;

        public float YInput => _yAxisInput.Value;



        public bool AnyButtonPressed => JumpButton.IsPressed || ConfirmButton.IsPressed || BackButton.IsPressed;

        public InputHandler(int index)
        {
            this.gamepadIndex = index;
            using (StreamReader reader = new StreamReader("input.json"))
            {
                string json = reader.ReadToEnd();
                mapping = JsonConvert.DeserializeObject<List<InputMapping>>(json).Single(m => m.Index == index);
            }
            SetupInput();
        }

        public InputHandler(InputMapping mapping)
        {
            gamepadIndex = mapping.Index;
            SetupInput();
        }

        /// <summary>
        /// Needs a better way to bind keys, just hard bind for now
        /// </summary>
        public void SetupInput()
        {
            _axialInput = Vector2.Zero;
            // horizontal input from dpad, left stick or keyboard left/right
            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.Nodes.Add(new Nez.VirtualAxis.GamePadDpadLeftRight(gamepadIndex));
            _xAxisInput.Nodes.Add(new Nez.VirtualAxis.GamePadLeftStickX(gamepadIndex));
            for (int i = 0; i < mapping.Left.Length; i++)
            {
                _xAxisInput.Nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, (Keys)mapping.Left[i], (Keys)mapping.Right[i]));
            }

            // vertical input from dpad, left stick or keyboard up/down
            _yAxisInput = new VirtualIntegerAxis();
            _yAxisInput.Nodes.Add(new Nez.VirtualAxis.GamePadDpadUpDown(gamepadIndex));
            _yAxisInput.Nodes.Add(new Nez.VirtualAxis.GamePadLeftStickY(gamepadIndex));
            for (int i = 0; i < mapping.Up.Length; i++)
            {
                _yAxisInput.Nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, (Keys)mapping.Up[i], (Keys)mapping.Down[i]));
            }

            //action buttons
            JumpButton = new VirtualButton();
            foreach (var key in mapping.JumpKey)
            {
                JumpButton.Nodes.Add(new Nez.VirtualButton.KeyboardKey((Keys)key));
            }
            foreach (var button in mapping.JumpButton)
            {
                JumpButton.Nodes.Add(new Nez.VirtualButton.GamePadButton(gamepadIndex, (Buttons)button));
            }

            DashButton = new VirtualButton();
            foreach (var key in mapping.DashKey)
            {
                DashButton.Nodes.Add(new Nez.VirtualButton.KeyboardKey((Keys)key));
            }
            foreach (var button in mapping.DashButton)
            {
                DashButton.Nodes.Add(new Nez.VirtualButton.GamePadButton(gamepadIndex, (Buttons)button));
            }


            ConfirmButton = new VirtualButton();
            foreach (var key in mapping.ConfirmKey)
            {
                ConfirmButton.Nodes.Add(new Nez.VirtualButton.KeyboardKey((Keys)key));
            }
            foreach (var button in mapping.ConfirmButton)
            {
                ConfirmButton.Nodes.Add(new Nez.VirtualButton.GamePadButton(gamepadIndex, (Buttons)button));
            }


            BackButton = new VirtualButton();
            foreach (var key in mapping.BackKey)
            {
                BackButton.Nodes.Add(new Nez.VirtualButton.KeyboardKey((Keys)key));
            }
            foreach (var button in mapping.BackButton)
            {
                BackButton.Nodes.Add(new Nez.VirtualButton.GamePadButton(gamepadIndex, (Buttons)button));
            }
        }
    }
}
