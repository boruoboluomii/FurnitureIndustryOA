 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heima8.OA.DALFactory;
using Heima8.OA.EFDAL;
using Heima8.OA.IBLL;
using Heima8.OA.IDAL;
using Heima8.OA.Model;
using Heima8.OA.NHDAL;

namespace Heima8.OA.BLL
{
	
	public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.ActionInfoDal;
        } 
	}
	
	public partial class FileInfoService:BaseService<FileInfo>,IFileInfoService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.FileInfoDal;
        } 
	}
	
	public partial class MenuInfoService:BaseService<MenuInfo>,IMenuInfoService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.MenuInfoDal;
        } 
	}
	
	public partial class OrderInfoService:BaseService<OrderInfo>,IOrderInfoService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.OrderInfoDal;
        } 
	}
	
	public partial class R_UserInfo_ActionInfoService:BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.R_UserInfo_ActionInfoDal;
        } 
	}
	
	public partial class RoleInfoService:BaseService<RoleInfo>,IRoleInfoService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.RoleInfoDal;
        } 
	}
	
	public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoDal;
        } 
	}
	
	public partial class UserInfoExtService:BaseService<UserInfoExt>,IUserInfoExtService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoExtDal;
        } 
	}
	
	public partial class WF_InstanceService:BaseService<WF_Instance>,IWF_InstanceService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.WF_InstanceDal;
        } 
	}
	
	public partial class WF_StepService:BaseService<WF_Step>,IWF_StepService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.WF_StepDal;
        } 
	}
	
	public partial class WF_TempService:BaseService<WF_Temp>,IWF_TempService //crud
    {
		public override void SetCurrentDal()
        {
            CurrentDal = DbSession.WF_TempDal;
        } 
	}
}