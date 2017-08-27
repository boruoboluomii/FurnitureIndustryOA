﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Heima8.OA.IBLL;
using Heima8.OA.Model;
using Spring.Context;
using Spring.Context.Support;

namespace Heima8.OA.Workflow
{

    public sealed class SetStepActivity : CodeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public InArgument<string> StepName { get; set; }
        public InArgument<bool> IsEnd { get; set; }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 派生并从 Execute 方法返回该值。
        protected override void Execute(CodeActivityContext context)
        {
            // 获取 Text 输入参数的运行时值
            string text = context.GetValue(this.StepName);
            bool end = context.GetValue(this.IsEnd);

            Guid insId = context.WorkflowInstanceId;
            Common.LogHelper.WriteLog("工作流实例id是："+context.WorkflowInstanceId);
            IApplicationContext ctx = ContextRegistry.GetContext();
            var WF_InstanceService = ctx.GetObject("WF_InstanceService") as IWF_InstanceService;
            var WF_StepService = ctx.GetObject("WF_StepService") as IWF_StepService;

            var inst = WF_InstanceService.GetEntities(w => w.WFInstanceId == insId).FirstOrDefault();

            if (inst == null)
            {
                Common.LogHelper.WriteLog("inst is null");
            }

            var stepStatus = (short) Heima8.OA.Model.Enum.WFStepEnum.UnProecess;
            var step = inst.WF_Step.Where(s => s.StepStatus == stepStatus).FirstOrDefault();
            Common.LogHelper.WriteLog("SetSetpActivity:"+text);
            step.StepName = text;
            step.IsEndStep = end;
            if (end)
            {
                step.ProcessResult = "审批结束";
                inst.Status = (short) Heima8.OA.Model.Enum.WF_InstanceEnum.Processed;
                WF_InstanceService.Update(inst);
            }
            WF_StepService.Update(step);

        }
    }
}
