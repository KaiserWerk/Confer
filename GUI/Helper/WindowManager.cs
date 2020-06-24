using GUI.View;

namespace GUI.Helper
{
    public static class WindowManager
    {
        private static RequestFileWindow requestFileWindow;
        public static void OpenRequestFileWindow()
        {
            requestFileWindow = new RequestFileWindow();
            requestFileWindow.ShowDialog();
        }

        public static void CloseRequestFileWindow()
        {
            requestFileWindow?.Close();
        }
    }
}
