using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sdl_test
{
    public class SceneManager
    {
        public Scene? currentScene;
        public void ChangeScene(Scene newScene) {
            currentScene = newScene;
            currentScene.Setup();
        }
    }
}