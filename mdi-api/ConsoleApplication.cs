using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace mdi_api {

    /// <summary>
    /// Commonly used as a base class that abstracts functionality from the superclass to make the development more trivial.
    /// </summary>
    /// <remarks>
    /// Can be used to change the Resolution or handle events that applies to the console.
    /// It also manages the edt.
    /// </remarks>
    public class ConsoleApplication {

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static readonly IntPtr MyConsole = GetConsoleWindow();

        [DllImport("kernel32.dll", ExactSpelling = true)]
        protected static extern bool SetConsoleCtrlHandler(HandlerRoutine handler, bool add);
        protected delegate bool HandlerRoutine(ControlTypes ctrlType);

        protected enum ControlTypes {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        /// <summary>
        /// The class constructor.
        /// -Sets cursor visible = false as default.
        /// -Sets up the control handler for the console events.
        /// -Starts the EDT</summary>
        /// <seealso cref="EventDispatchThread"/>
        public ConsoleApplication() {
            Console.CursorVisible = false;
            SetConsoleCtrlHandler(ConsoleControlCheck, true);
            EventDispatchThread.Start();
        }

        /// <summary>
        /// Handles the different events that the Console sends.
        /// </summary>
        /// <param name="type">Type of Event</param>
        /// <seealso cref="ControlTypes"/>
        /// <returns></returns>
        private bool ConsoleControlCheck(ControlTypes type) {
            switch(type) {
                case (ControlTypes.CTRL_CLOSE_EVENT): {
                        Terminate();
                        break;
                    }
            }
            return true;
        }

        /// <summary>
        /// Can be called at any time, is automaticlly called when CTRL_CLOSE_EVENT occurs.
        /// The result of calling this function is shutting down the EDT.</summary>
        /// <seealso cref="EventDispatchThread"/>
        protected virtual void Terminate() {
            EventDispatchThread.Terminate();
        }

        /// <summary>
        /// Applies a new Resolution to the Console.
        /// </summary>
        /// <param name="resolution">An instance of a Resolution.</param>
        /// <param name="position">A Boolean that decides if the Console window should be positioned at x=0, y=0</param>
        public static void SetResolution(Resolution resolution, bool position = true) {
            try {
                Console.SetWindowSize(resolution.Rows, resolution.Columns);
            } catch(ArgumentOutOfRangeException e) {
                Debug.Write(e.Message);
            }

            if(position)
                SetWindowPos(MyConsole, 0, 0, 0, 0, 0, 0x0001);

            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight+1);
        }
    }
}
