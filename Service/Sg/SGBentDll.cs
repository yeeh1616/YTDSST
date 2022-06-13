using System.Runtime.InteropServices;

namespace YTDSSTGenII.Service.Sg
{
    public static class SGBentDll
    {
        // Methods
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgBind(int type, string techpasswd, int provinceid, string no, string hwsn, string logonid, string logonpwd);
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgChangePassword(int mode, string opindex, string oldpasswd, string newpasswd);
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void sgClose();
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgERRMsg(int type, ref byte msg, int size);
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgFactoryTest();
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgGetAcount();
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgGetclaimAmount();
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgInit(string hwsn, ref byte version);
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgPingHost(int t);
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgSetclaimAmount(int amount);
        [DllImport("ilmsDll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int sgValidation(int type, string sn, string security, double payamount, ref double prizeamount, ref double acount);
    }
}
