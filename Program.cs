using sdl_test;
using static SDL2.SDL; 

namespace Program {
    class Program{
        public static int Main(){
            App app = new App();
            if (app.Setup() < 0){
                return -1;
            }
            app.MainLoop();
            app.Destroy();
            return 0;
        }
    }
}