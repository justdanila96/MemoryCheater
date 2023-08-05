using System.Runtime.InteropServices;
using System.Text;

namespace MemoryCheater {
    internal class WindowData {
        public WindowData(nint ptr, string name) {
            Ptr = ptr;
            Name = name;
        }

        public nint Ptr { get; }
        public string Name { get; }
        public override string ToString() {
            return Name;
        }
    }
    internal static partial class ScreenCapture {

        private delegate bool EnumWindowsProc(nint hWnd, int lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct WinAPIRect {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [LibraryImport("USER32.DLL")]
        private static partial nint GetForegroundWindow();

        [LibraryImport("USER32.DLL")]
        private static partial nint GetDesktopWindow();

        [LibraryImport("USER32.DLL")]
        private static partial nint GetWindowRect(nint hWnd, ref WinAPIRect rect);

        [LibraryImport("USER32.DLL")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(nint hWnd, StringBuilder lpString, int nMaxCount);

        [LibraryImport("USER32.DLL", EntryPoint = "GetWindowTextLengthA", SetLastError = true)]
        private static partial int GetWindowTextLength(nint hWnd);

        [LibraryImport("USER32.DLL")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool IsWindowVisible(nint hWnd);

        [LibraryImport("USER32.DLL")]
        private static partial nint GetShellWindow();

        public static List<WindowData> GetOpenWindows() {

            nint shellWindow = GetShellWindow();
            var windows = new List<WindowData>();

            _ = EnumWindows((hWnd, lParam) => {
                if (hWnd == shellWindow)
                    return true;

                if (!IsWindowVisible(hWnd))
                    return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0)
                    return true;

                var builder = new StringBuilder(length);
                _ = GetWindowText(hWnd, builder, length + 1);

                windows.Add(new WindowData(hWnd, builder.ToString()));
                return true;
            }, 0);

            return windows;
        }

        public static Bitmap CaptureWindow(nint handle) {

            var rect = new WinAPIRect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var bmp = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics G = Graphics.FromImage(bmp)) {
                G.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return bmp;
        }
    }
}
