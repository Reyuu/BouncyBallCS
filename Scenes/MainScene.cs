using static SDL2.SDL;

namespace sdl_test;

public class MainScene : Scene
{
    public MainScene(App parentApp) : base(parentApp) {}
    public override void Setup(){
        BouncyBall bouncyBall = new BouncyBall {
            x = 100,
            y = 100,
            w = 20,
            h = 20,
            r = 20,
        };
        AddGameObject(bouncyBall);
        base.Setup();
    }

    public override void Draw(nint renderer)
    {  
        _ = SDL_SetRenderDrawColor(renderer, 0, 0, 0, 255);
        _ = SDL_RenderClear(renderer);
        base.Draw(renderer);
    }
}
