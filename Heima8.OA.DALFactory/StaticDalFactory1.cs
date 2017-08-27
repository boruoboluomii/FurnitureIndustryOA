 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Heima8.OA.EFDAL;
using Heima8.OA.IDAL;
using Heima8.OA.NHDAL;

namespace Heima8.OA.DALFactory
{
    /// <summary>
    /// 由简单工厂转变成了抽象工厂。
    /// 抽象工厂  VS  简单工厂
    /// 
    /// </summary>
    public partial class StaticDalFactory
    {
   
	
		public static IActionInfoDal GetActionInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ActionInfoDal")
				as IActionInfoDal;
		}
	
		public static IFileInfoDal GetFileInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".FileInfoDal")
				as IFileInfoDal;
		}
	
		public static IMenuInfoDal GetMenuInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".MenuInfoDal")
				as IMenuInfoDal;
		}
	
		public static IOrderInfoDal GetOrderInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".OrderInfoDal")
				as IOrderInfoDal;
		}
	
		public static IR_UserInfo_ActionInfoDal GetR_UserInfo_ActionInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".R_UserInfo_ActionInfoDal")
				as IR_UserInfo_ActionInfoDal;
		}
	
		public static IRoleInfoDal GetRoleInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".RoleInfoDal")
				as IRoleInfoDal;
		}
	
		public static IUserInfoDal GetUserInfoDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoDal")
				as IUserInfoDal;
		}
	
		public static IUserInfoExtDal GetUserInfoExtDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".UserInfoExtDal")
				as IUserInfoExtDal;
		}
	
		public static IWF_InstanceDal GetWF_InstanceDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WF_InstanceDal")
				as IWF_InstanceDal;
		}
	
		public static IWF_StepDal GetWF_StepDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WF_StepDal")
				as IWF_StepDal;
		}
	
		public static IWF_TempDal GetWF_TempDal()
		{
			return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WF_TempDal")
				as IWF_TempDal;
		}
	}
}