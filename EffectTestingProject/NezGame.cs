using EffectTestingProject.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace EffectTestingProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class NezGame : Core
    {
        public static int designWidth = 1280;
        public static int designHeight = 720;
        public NezGame() : base(windowTitle: "Effects!!", isFullScreen: false)
        {
            var policy = Scene.SceneResolutionPolicy.BestFit;
            Scene.SetDefaultDesignResolution(designWidth, designHeight, policy, 0, 0);
            Window.AllowUserResizing = true;

            Nez.Input.MaxSupportedGamePads = 4;
        }
        protected override void Initialize()
        {
            base.Initialize();
            Scene = new Scenes.LemsDepthArm();
        }
    }
}
