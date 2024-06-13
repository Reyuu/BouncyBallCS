using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace sdl_test
{
    public class Scene
    {
        Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
        List<string> sortedKeys = new List<string>();
        List<string> revSortedKeys = new List<string>();
        int globalZCounter = 0;

        public App app;

        public Scene(App parentApp) {
            app = parentApp;
        }

        public bool AddGameObject(GameObject obj, string name = "") {
            if (name == ""){
                name = Guid.NewGuid().ToString();
            }
            obj.z = globalZCounter;
            gameObjects.Add(name, obj);
            Sort();
            return true;
        }
        void Sort() {
            sortedKeys = new List<string>(gameObjects.Keys.OrderBy(x => gameObjects[x].z));
            revSortedKeys = new List<string>(sortedKeys); // explicit copy
            revSortedKeys.Reverse(); // very expensive, do beforehand!
        }
        virtual public void Setup(){
            foreach (string key in sortedKeys) {
                GameObject obj = gameObjects[key];
                obj.Setup(this);
            }
        }
        virtual public void Draw(IntPtr renderer) {
            foreach (string key in sortedKeys) {
                GameObject obj = gameObjects[key];
                obj.Draw(renderer);
            }
        }
        virtual public void Update(double dt, SDL_Event e) {
            foreach (string key in revSortedKeys) {
                GameObject obj = gameObjects[key];
                obj.Update(dt, e);
            }
        }
        virtual public void Destroy(){
            foreach (string key in sortedKeys) {
                GameObject obj = gameObjects[key];
                obj.Destroy();
            }
        }
    }
}