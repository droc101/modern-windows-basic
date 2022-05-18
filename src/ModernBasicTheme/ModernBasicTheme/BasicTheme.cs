using System;
using System.Runtime.InteropServices;

namespace DWM
{


    public static class BasicTheme
    {
        [DllImport("dwmapi.dll")]
        internal static extern int DwmSetWindowAttribute(IntPtr hWnd, uint dwAttribute, IntPtr pvAttribute, uint cbAttribute);

        enum DWMWINDOWATTRIBUTE
        {
            DWMWA_NCRENDERING_POLICY = 2

        }
        enum DWMNCRENDERINGPOLICY
        {
            DWMNCRP_DISABLED = 1,
            DWMNCRP_ENABLED = 2
        }

        public static void DisableDwmNCRendering(IntPtr hWnd)
        {
            DWMNCRENDERINGPOLICY ncrp = DWMNCRENDERINGPOLICY.DWMNCRP_DISABLED;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
            Marshal.WriteInt32(ptr, (int)ncrp);
            DwmSetWindowAttribute(hWnd, (uint)DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY, ptr, sizeof(int));
            Marshal.FreeHGlobal(ptr);
        }

        public static void EnableDwmNCRendering(IntPtr hWnd)
        {
            DWMNCRENDERINGPOLICY ncrp = DWMNCRENDERINGPOLICY.DWMNCRP_ENABLED;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
            Marshal.WriteInt32(ptr, (int)ncrp);
            DwmSetWindowAttribute(hWnd, (uint)DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY, ptr, sizeof(int));
            Marshal.FreeHGlobal(ptr);
        }

    }
}
