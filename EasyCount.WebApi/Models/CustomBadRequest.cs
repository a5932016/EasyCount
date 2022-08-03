using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EasyCount.WebApi.Models
{
    /// <summary>
    /// 自訂異常返回訊息
    /// </summary>
    public class CustomBadRequest : ValidationProblemDetails
    {
        public CustomBadRequest(ActionContext context)
        {
            Title = "WebAPI客戶端傳入的參數無效";
            Detail = "客戶端使用WebAPI時傳入的參數類型與接口需要的類型不匹配";
            Status = 400;
            ConstructErrorMessages(context);
        }

        private void ConstructErrorMessages(ActionContext context)
        {
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors.Count == 0) continue;

                if (errors.Count == 1)
                {
                    var errorMessage = GetErrorMessage(errors[0]);
                    Errors.Add(key, new[] { errorMessage });
                }
                else
                {
                    var errorMessages = new string[errors.Count];
                    for (var i = 0; i < errors.Count; i++)
                    {
                        errorMessages[i] = GetErrorMessage(errors[i]);
                    }
                    Errors.Add(key, errorMessages);
                }
            }
        }

        private string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ? "The input was not valid." : error.ErrorMessage;
        }
    }
}
