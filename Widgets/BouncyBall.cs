namespace sdl_test;

using SDL2;
using static SDL2.SDL;
using static SDL2.SDL_gfx;

public class BouncyBall : GameObject
{
    public SDL_Color color = Utils.SDLColorFromHex("#ffffff");
    public uint colorInt = 0;
    public int r = 10;
    public int speed = 500;
    bool bumpX = false;
    bool bumpY = false;
    public App? app;
    int ww = 0;
    int wh = 0;
 
    public BouncyBall() : base()
    {
        Setup += PrivateSetup;
        Draw += PrivateDraw;
        Update += PrivateUpdate;
    }

    void PrivateSetup(Scene scene)
    {
        app = scene.app;
        SDL_GetWindowSize(app.window, out ww, out wh);
        colorInt = Utils.IntFromColor(color);
    }
    void PrivateDraw(IntPtr renderer)
    {
        _ = SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
        _ = filledCircleColor(renderer, (short)x, (short)y, (short)r, colorInt);
        _ = aacircleRGBA(renderer, (short)x, (short)y, (short)r, color.r, color.g, color.b, color.a);
    }

    void PrivateUpdate(double dt, SDL_Event e)
    {
        if (x >= ww || x <= 0) {
            bumpX = !bumpX;
        }
        if (y >= wh || y <= 0){
            bumpY = !bumpY;
        }
        x += (int)(dt * speed * (bumpX ? 1 : -1));
        y += (int)(dt * speed * (bumpY ? 1 : -1));
    }
}
