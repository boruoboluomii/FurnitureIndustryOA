using System;

namespace Heima8.OA.Workflow
{
    public class BaseResumeBookMarkValue
    {
        public object Value { get; set; }

        public string BookMarkName { get; set; }

        public Guid InstanceId { get; set; }
    }
}