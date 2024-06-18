using static SDL2.SDL;

namespace sdl_test;

public class GameObject {
    public int x;
    public int y;
    public int z;
    public int w;
    public int h;
    public Scene? parentScene;

    public GameObject? parent = null;
    public Action<Scene> Setup = delegate(Scene scene){};
    public Action<IntPtr> Draw = delegate(IntPtr renderer){};
    public Action<double, SDL_Event> Update = delegate(double dt, SDL_Event e){};
    public Action Destroy = delegate(){};
    public GameObject() {
        Setup += InitialSetup;
    }
    void InitialSetup(Scene scene)
    {
        parentScene = scene;
    }
}


