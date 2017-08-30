using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Heima8.OA.Workflow;

namespace Heima8.OA.UI.Portal.Controllers
{
    public class WFInstanceController : BaseController
    {
        //
        // GET: /WFInstance/

        short delflagNormal = (short)Heima8.OA.Model.Enum.DelFlagEnum.Normal;

        public IWF_TempService WF_TempService { get; set; }
        public IUserInfoService UserInfoService { get; set; }

        public IWF_InstanceService WF_InstanceService { get; set; }
        public IWF_StepService WF_StepService { get; set; }

        public IRoleInfoService RoleInfoService { get; set; }
        #region 发起流程
        //发起流程Id：  流程模板的id
        public ActionResult Add(int id)
        {

            var temp = WF_TempService.GetEntities(u => u.ID == id).FirstOrDefault();
            ViewBag.Temp = temp;
           
            
            //
            var allUsers = UserInfoService.GetEntities(u => u.DelFlag == delflagNormal).ToList();
            var flowToUserInfos = GetFlowToRoleUserInfos(temp.ID);
            if (flowToUserInfos != null && flowToUserInfos.Count > 0)
            {
                ViewData["flowTo"] = (from u in flowToUserInfos
                 select new SelectListItem() { Selected = false, Text = u.UName, Value = u.ID + "" })
                                    .ToList();
            }
            else
            {
                ViewData["flowTo"] = (from u in allUsers
                                    select new SelectListItem() { Selected = false, Text = u.UName, Value = u.ID + "" })
                                    .ToList();
            }
            
            return View();
        }

        public List<UserInfo> GetFlowToRoleUserInfos(int tempId)
        {
            var allUserInfos = UserInfoService.GetEntities(u => u.DelFlag == delflagNormal).ToList();
            List<UserInfo> flowToUserInfos = new List<UserInfo>();
            //当前用户每个角色
            foreach (RoleInfo loginUserRoleInfo in LoginUser.RoleInfo)
            {
                string flowToRoleKeyWord = GetRoleHelper.GetFlowToRoleKeyWord(loginUserRoleInfo, tempId);
                foreach (UserInfo onUserInfo in allUserInfos)
                {
//所有用户中一个用户的每个角色
                    foreach (RoleInfo oneRoleOfoneUser in onUserInfo.RoleInfo)
                    {
                        if(flowToRoleKeyWord == null) continue;
                        if (oneRoleOfoneUser.RoleName.Contains(flowToRoleKeyWord)) flowToUserInfos.Add(onUserInfo);
                    }
                }
            }
            return flowToUserInfos;
        }

        /// <summary>
        /// 发起流程
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id"></param>
        /// <param name="flowTo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(WF_Instance instance, int id,int flowTo)
        {
            var currentUserId = LoginUser.ID;

            //在工作流实例表添加一条数据：
            instance.DelFlag = delflagNormal;
            instance.StartTime = DateTime.Now;
            instance.FilePath = string.Empty;

            instance.StartBy = currentUserId;
            instance.Level = 0;
            instance.Status = (short) Heima8.OA.Model.Enum.WF_InstanceEnum.Processing;
            instance.WFInstanceId = Guid.Empty;
            instance.WF_TempID = id;
            WF_InstanceService.Add(instance);
            //第二点：启动工作流
//            var wfApp = WorkflowApplicationHelper.CreateWorkflowApp(new FincalActivity(), null);
            var role =
                RoleInfoService.GetEntities(r => r.DelFlag == delflagNormal && r.RoleName.Contains("设计"))
                    .FirstOrDefault();
            var wfApp = WorkflowApplicationHelper.CreateWorkflowApp(new ProductFlow(), 
                new Dictionary<string, object>()
                {
                     {"AfterDesignFlowTo",GetRoleHelper.GetFlowToRoleKeyWord(role ,id)}
                });
            instance.WFInstanceId = wfApp.Id;
            WF_InstanceService.Update(instance);
            
            //第三点：在步骤表里面添加两条步骤。一个当前已经处理的完成步骤。
            WF_Step startStep = new WF_Step();
            startStep.WF_InstanceID = instance.ID;
            startStep.SubTime = DateTime.Now;
            startStep.StepName = "提交审批表单";
            startStep.IsEndStep = false;
            startStep.IsStartStep = true;
            startStep.ProcessBy = currentUserId;
            startStep.PorcessComment = "提交申请";
            startStep.ProcessResult = "通过";
            startStep.ProcessTime = DateTime.Now;
//            startStep.StepName = "提交审批表单";
            startStep.StepStatus = (short) Heima8.OA.Model.Enum.WFStepEnum.Processed;
            

            WF_StepService.Add(startStep);

            //二个步骤：下一步谁审批的步骤。  项目经理审批
            WF_Step nextStep = new WF_Step();
            nextStep.WF_InstanceID = instance.ID;
            nextStep.SubTime = DateTime.Now;
            nextStep.ProcessTime = DateTime.Now;
            nextStep.PorcessComment = string.Empty;
            
            nextStep.IsEndStep = false;
            nextStep.IsStartStep = false;
            nextStep.ProcessBy = flowTo;
            
            nextStep.ProcessResult = "";
            
            nextStep.StepName = "";
            nextStep.StepStatus = (short)Heima8.OA.Model.Enum.WFStepEnum.UnProecess;
            WF_StepService.Add(nextStep);
            return RedirectToAction("ShowMyCheck");
        }

        #endregion

       
        #region 我的审批流程
        //我申请、发起的流程
        public ActionResult ShowMyCheck()
        {
            var data = WF_InstanceService.GetEntities(i => i.StartBy == LoginUser.ID).ToList();
            return View(data);
        }

        //显示待审批
        public ActionResult ShowMyUnCheck()
        {

            var runEnum = (short) Heima8.OA.Model.Enum.WFStepEnum.UnProecess;
            var instanceEnum = (short) Heima8.OA.Model.Enum.WF_InstanceEnum.Processing;
            //拿到当前用户未处理的 步骤
            var steps = WF_StepService.GetEntities(s => s.StepStatus == runEnum && s.ProcessBy == LoginUser.ID).ToList();

            //通过我没有处理的步骤拿到流程的实例id
            var instanceIds = (from s in steps
                               select s.WF_InstanceID).Distinct();
            //流程实例的id拿到所有的流程。
            var data = WF_InstanceService.GetEntities(
                i => instanceIds.Contains(i.ID) && i.Status == instanceEnum).ToList();

            return View(data);
        }
        
        public ActionResult ShowMyChecked()
        {
            var runEnum = (short)Heima8.OA.Model.Enum.WFStepEnum.UnProecess;
            var instanceEnum = (short)Heima8.OA.Model.Enum.WF_InstanceEnum.Processed;


            var steps = WF_StepService.GetEntities(s => s.StepStatus == runEnum && s.ProcessBy == LoginUser.ID).ToList();

            var instanceIds = (from s in steps
                               select s.WF_InstanceID).Distinct();
            var data = WF_InstanceService.GetEntities(i => instanceIds.Contains(i.ID)).ToList();

            return View(data);
        }
        #endregion

        #region 详情
        public ActionResult ShowInstance(int id)
        {
            ViewData.Model = WF_InstanceService.GetEntities(u => u.ID == id).FirstOrDefault();
            return View();
        }
        #endregion

        #region 审批
        public ActionResult ShowCheck(int id)
        {
            var instance = WF_InstanceService.GetEntities(u => u.ID == id).FirstOrDefault();

            ViewData.Model = instance;
//            ViewData["flowTo"] =
//               UserInfoService.GetEntities(u => u.DelFlag == delflagNormal).ToList()
//                              .Select(u => new SelectListItem() { Selected = false, Text = u.UName, Value = u.ID.ToString() });

            var temp = WF_TempService.GetEntities(u => u.ID == instance.WF_TempID).FirstOrDefault();
            var flowToUserInfos = GetFlowToRoleUserInfos(temp.ID);
            if (flowToUserInfos != null && flowToUserInfos.Count > 0)
            {
                ViewData["flowTo"] = (from u in flowToUserInfos
                                      select new SelectListItem() { Selected = false, Text = u.UName, Value = u.ID + "" })
                                    .ToList();
            }
            else
            {
                ViewData["flowTo"] = UserInfoService.GetEntities(u => u.DelFlag == delflagNormal).ToList()
                    .Select(u => new SelectListItem() { Selected = false, Text = u.UName, Value = u.ID.ToString() });
            }
            return View();
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="stepId">当前审批步骤id</param>
        /// <param name="isPass">是否通过</param>
        /// <param name="Comment">审批意见</param>
        /// <param name="flowTo">流转到谁</param>
        /// <returns></returns>
        public ActionResult DoCheck(int stepId, bool isPass, string Comment, int flowTo)
        {
            //stepId:id, isPass: pass, Comment: $("#Comment").val() }

            //1、更新当前步骤
            var step = WF_StepService.GetEntities(s => s.ID == stepId).FirstOrDefault();

            step.ProcessResult = isPass ? "通过" : "不通过";
            step.StepStatus = (short) Heima8.OA.Model.Enum.WFStepEnum.Processed;
            step.ProcessTime = DateTime.Now;
            
            step.PorcessComment = Comment;
            WF_StepService.Update(step);


            //初始化下一个步骤
            WF_Step nextStep = new WF_Step();
            nextStep.IsEndStep = false;
            nextStep.IsStartStep = false;
            nextStep.ProcessBy = flowTo;//下一个步骤处理人
            nextStep.SubTime = DateTime.Now;
            nextStep.ProcessResult = string.Empty;

            nextStep.StepStatus = (short)Heima8.OA.Model.Enum.WFStepEnum.UnProecess;
            nextStep.StepName = string.Empty;
            nextStep.WF_InstanceID = step.WF_InstanceID;
            nextStep.ProcessTime = DateTime.Now;
            nextStep.PorcessComment = string.Empty;
            
            WF_StepService.Add(nextStep);

            //让书签继续往下执行。
            var Value = isPass ? 1 : 0;

            Heima8.OA.Workflow.WorkflowApplicationHelper.ResumeBookMark(
//                new FincalActivity(),
                new ProductFlow(), 
                step.WF_Instance.WFInstanceId,
                step.StepName,
                Value);
            return Content("ok");
        }
        #endregion
     

    }
}
