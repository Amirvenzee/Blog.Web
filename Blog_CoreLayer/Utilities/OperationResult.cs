using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog_CoreLayer.Utilities
{
    public class OperationResult
    {
        public string Message { get; set; }
        public OperationResultStatus Status { get; set; }

        #region Errors
        public static OperationResult Error()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message ="عملیات ناموفق",
                
            };
        }
       

        
        public static OperationResult Error(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = message,
            };
        }
        #endregion

        #region NotFound
        public static OperationResult NotFound()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = "اطلاعات درخواستی یافت نشد",
            };
        }
     


        public static OperationResult NotFound(string Message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = Message,
            };
        }
        #endregion


        #region Success
        public static OperationResult Success()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = "عملیات با موفقیت انجام شد",
            };
        }

        public static OperationResult Success(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = message,
            };
        }
         #endregion


    }

    public enum OperationResultStatus
    {
        Error=10,
        Success=200,
        NotFound=404
    }
}
