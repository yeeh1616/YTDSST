using YTDSSTGenII.Forms.model;
using YTDSSTGenII.Service.Enums;


namespace YTDSSTGenII.Service.Context
{
    public class MachineContext
    {
        //调试模式，控制日志
        private static EnumRunMode runMode = EnumRunMode.PRODUCT;
        //private static EnumRunMode runMode = EnumRunMode.DEBUG;
        //环境模式，生产环境还是测试环境
        private static EnumAppEnvMode appEnvMode = EnumAppEnvMode.PRODUCT;
        //private static EnumAppEnvMode appEnvMode = EnumAppEnvMode.TEST;

        //private static MotorController motorController;
        //public static MotorController MotorController { 
        //    get {
        //        if (motorController == null) {
        //            motorController = new MotorController();
        //        }
        //        return motorController;
        //    }
        //}

        private ServerMotor[] server_motor_array = new ServerMotor [ 8 ];

        private MachineContext ( )
        {}

        private static MachineContext instance;
        public static MachineContext getInstance ( )
        {
            if ( null == instance )
            {
                instance = new MachineContext ( );
            }
            return instance;
        }

        public ServerMotor [ ] ServerMotorArray
        {
            get { return server_motor_array; }
        }

	    public static EnumRunMode RunMode
        {
            get { return MachineContext.runMode; }
        }

        public static EnumAppEnvMode AppEnvMode
        {
            get { return MachineContext.appEnvMode; }
        }
    }
}

