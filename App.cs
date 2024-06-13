namespace sdl_test;

using System.Diagnostics;
using SDL2;
using static SDL2.SDL;
using static SDL2.SDL_ttf;

public class App
{
        const int SAMPLE_LIMIT = 250;
        public IntPtr window;
        public IntPtr renderer;
        public bool running = true;
        public double time = 0.0;
        public double deltaTime = 0.015;
        double currentTime = 0.0;
        double accumulator = 0.0;
        ulong startPerfCounter;
        ulong endPerfCounter;
        IntPtr font;
        SDL_Color fontColor = Utils.SDLColorFromHex("#ffffff");
        public SceneManager sceneManager = new SceneManager();
        public Stack<double> fpsSamples = new Stack<double>(SAMPLE_LIMIT);
        public int Setup(){
            if (SDL_Init(SDL_INIT_VIDEO) < 0) {
                SDL_Log($"Whoopsie {SDL_GetError()}");
                return -1;
            }
            window = SDL_CreateWindow("MySDL app", SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, 1024, 768, SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (window == IntPtr.Zero){
                SDL_Log($"Whoopsie {SDL_GetError()}");
                return -1;
            }
            renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (renderer == IntPtr.Zero){
                SDL_Log($"Whoopsie {SDL_GetError()}");
                return -1;
            }
            if (TTF_Init() < 0){
                SDL_Log($"Whoopsie {SDL_GetError()}");
                return -1;
            }
            font = TTF_OpenFont("tahoma.ttf", 12);
            if (font == IntPtr.Zero) {
                SDL_Log($"Whoopsie {SDL_GetError()}");
                return -1;
            }
            Scene mainScene = new MainScene(this);
            sceneManager.ChangeScene(mainScene);
            currentTime = (double)SDL_GetTicks() / 1000;
            return 0;
        }

        public void Destroy(){
            sceneManager.currentScene?.Destroy();
            SDL_DestroyRenderer(renderer);
            SDL_DestroyWindow(window);
            SDL_Quit();
            TTF_Quit();
        }

        void Draw(){
            // no clear, each scene should to that explicitly as it's NOT free.
            sceneManager.currentScene?.Draw(renderer);
            if (fpsSamples.Count() != 0){
                double averageFps = fpsSamples.Average();
                IntPtr text;
                text = TTF_RenderText_Solid(font, String.Format("FPS: {0:N0}", averageFps), fontColor);
                if (text != IntPtr.Zero){
                    IntPtr texture;
                    texture = SDL_CreateTextureFromSurface(renderer, text);
                    SDL_Rect dest;
                    dest.x = 0;
                    dest.y = 0;
                    SDL_QueryTexture(texture, out _, out _, out dest.w, out dest.h);
                    SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref dest);
                    SDL_FreeSurface(text);
                    SDL_DestroyTexture(texture);
                }
            }
            SDL_RenderPresent(renderer);
        }

        public void MainLoop(){
            while (running) {
                startPerfCounter = SDL_GetPerformanceCounter();

                // https://gafferongames.com/post/fix_your_timestep/
                double newTime = (double)SDL_GetTicks() / 1000;
                double frameTime = newTime - currentTime;
                // if (frameTime > 0.25){
                //     frameTime = 0.25;
                // }
                currentTime = newTime;
                accumulator += frameTime;

                _ = SDL_PollEvent(out SDL_Event e);
                if (e.type == SDL_EventType.SDL_QUIT){
                    running = false;
                    return;
                }

                while (accumulator >= deltaTime){
                    sceneManager.currentScene?.Update(deltaTime, e);
                    accumulator -= deltaTime;
                    time += deltaTime;
                }
                Draw();

                endPerfCounter = SDL_GetPerformanceCounter();
                double fps = 1.0 / ((endPerfCounter - startPerfCounter) / (double)SDL_GetPerformanceFrequency());
                if (fpsSamples.Count() >= SAMPLE_LIMIT) {
                    fpsSamples.Pop();
                }
                fpsSamples.Push(fps);
            }
        }
}
