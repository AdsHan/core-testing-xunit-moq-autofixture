using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Communication
{
    public class BaseResult
    {
        public List<String> Errors { get; set; }
        public Object Response { get; set; }

        public BaseResult()
        {
            Errors = new List<String>();
            Response = null;
        }

        public bool IsValid()
        {
            return !Errors.Any();
        }
    }
}