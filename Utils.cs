namespace sdl_test;

using System.Drawing;
using static SDL2.SDL;

public class Utils
{
    public static SDL_Color SDLColorFromHex(string color) {
        SDL_Color return_color;
        Color hex_color = ColorTranslator.FromHtml(color);
        return_color.r = Convert.ToByte(hex_color.R);
        return_color.g = Convert.ToByte(hex_color.G);
        return_color.b = Convert.ToByte(hex_color.B);
        return_color.a = 255;
        return return_color;
    }
    public static uint IntFromColor(SDL_Color color){
        return (uint)((color.r << 24) + (color.g << 16) + (color.b << 8) + color.a);
    }
}
