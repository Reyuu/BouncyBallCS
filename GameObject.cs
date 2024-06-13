using static SDL2.SDL;

namespace sdl_test;

public interface IGameObject{
    public void Setup(Scene parentScene);
    public void Draw(IntPtr renderer);
    public void Update(double dt, SDL_Event e);
    public void Destroy();
}
public class GameObject : IGameObject
{
    public int x;
    public int y;
    public int z;
    public int w;
    public int h;
    public Scene? parentScene;

    public GameObject? parent = null;
    virtual public void Setup(Scene scene){
        parentScene = scene;
    }
    virtual public void Draw(IntPtr renderer){}
    virtual public void Update(double dt, SDL_Event e){}
    virtual public void Destroy(){}
}


