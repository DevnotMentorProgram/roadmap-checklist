using System;

namespace RoadmapChecklist.Service
{
    public class ReturnModel<T>
    {
        public ReturnModel()
        {
            IsSuccess = true;
            Data = default;
            Exception = null;
        }

        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}