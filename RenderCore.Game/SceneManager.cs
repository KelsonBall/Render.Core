using Render.Core;
using System;
using System.Collections.Generic;

namespace RenderCore.Game
{
    public class SceneManager
    {
        private object currentSceneLock = new object();

        public GameObject Current;

        private readonly Dictionary<string, Func<Scene>> Scenes = new Dictionary<string, Func<Scene>>();

        public void AddSceneFactory(string name, Func<Scene> sceneFactory) => Scenes.Add(name, sceneFactory);

        public void OpenScene(string name)
        {
            lock (currentSceneLock)
                Current = Scenes[name]();
        }

        public SceneManager(ICanvas canvas)
        {
            canvas.Step += __ =>
            {
                lock (currentSceneLock)
                    Current?.Update(TimeSpan.FromMilliseconds(__ * 1000));
            };

            canvas.Draw += _ =>
            {
                lock (currentSceneLock)
                    Current?.Render(canvas);
            };
        }
    }
}
