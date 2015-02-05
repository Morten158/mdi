using System;
using System.Diagnostics;
using System.Threading;

namespace mdi_api {

    public delegate void KeyListener(ConsoleKey key);
    public delegate void TickListener();

    /// <summary>
    /// Function as a event dispatch thread that fires off OnTick events and OnKeyPressed events.
    /// </summary>
    public class EventDispatchThread {

        public static bool FlushBuffer = true;
        private static double _tick = 100;

        private static volatile KeyListener _onKeyPressed;
        private static volatile TickListener _onTick;

        public static volatile bool _running;

        /// <summary>
        /// Starts this thread(edt) that reads keys from the console and fires a OnTick every 100ms.
        /// </summary>
        public static void Start() {
            if(!_running)
                new Thread(Run).Start();
        }

        private static void Run() {
            _running = true;

            Stopwatch watch = new Stopwatch();
            watch.Start();
            while(_running) {

                if(Console.KeyAvailable) {
                    ConsoleKeyInfo info = Console.ReadKey(FlushBuffer);
                    if(_onKeyPressed != null)
                        _onKeyPressed(info.Key);
                }

                if(watch.ElapsedMilliseconds < _tick)
                    continue;
                watch.Restart();

                if(_onTick != null)
                    _onTick();
            }
        }

        /// <summary>
        /// Add a subscriber to the OnKeyPressed Event.
        /// </summary>
        /// <param name="listener">The function to be called when a key has been pressed.</param>
        public static void AddKeyListener(KeyListener listener) {_onKeyPressed += listener;}

        /// <summary>
        /// Removes a subscriber from the OnKeyPressed Event.
        /// </summary>
        /// <param name="listener">The function to be removed from the OnKeyPressed Event.</param>
        public static void RemoveKeyListener(KeyListener listener) {_onKeyPressed -= listener;}

        /// <summary>
        /// Add a subscriber to the OnTick Event.
        /// </summary>
        /// <param name="listener">The function to be called when a tick occurs.</param>
        public static void AddTickListener(TickListener listener) {_onTick += listener;}

        /// <summary>
        /// Removes a subscriber from the OnTick Event.
        /// </summary>
        /// <param name="listener">The function to be removed from the OnTick Event.</param>
        public static void RemoveTickListener(TickListener listener) {_onTick -= listener;}

        /// <summary>
        /// Terminates the edt thead.
        /// </summary>
        public static void Terminate() {
            if(_running) {
                _running = false;
                Dispose();
            }
        }

        /// <summary>
        /// Sets the tick interval.
        /// </summary>
        /// <param name="tick">tick interval.</param>
        public static void SetTick(int tick) {
            if(tick < 16 || tick > 60000)
                return;
            _tick = tick;
        }

        /// <summary>
        /// Clear the list of subscribers from OnkeyPressed and OnTick.
        /// </summary>
        public static void Dispose() {
            _onKeyPressed = null;
            _onTick = null;
        }
    }
}
