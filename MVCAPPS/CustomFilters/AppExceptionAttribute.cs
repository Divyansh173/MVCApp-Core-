using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.SqlClient;

namespace MVCAPPS.CustomFilters
{
    public class AppExceptionAttribute : IExceptionFilter
    {
        private IModelMetadataProvider _metadataProvider;

        public AppExceptionAttribute(IModelMetadataProvider _metadataProvider) 
        {
            this._metadataProvider = _metadataProvider;    
        }

        public void OnException(ExceptionContext context) 
        {
            context.ExceptionHandled = true;
            string errorMessage = context.Exception.Message;
            ViewResult viewResult = new ViewResult();
            if (context.Exception.GetType() == typeof(SqlException))
            {
                viewResult.ViewName = "DbErrors";
            }
            else
            {
                viewResult.ViewName = "Error";
            }

            ViewDataDictionary viewData = new ViewDataDictionary(_metadataProvider,context.ModelState);

            viewData["Controller"] = context.RouteData.Values["controller"]!.ToString();
            viewData["Action"] = context.RouteData.Values["action"]!.ToString();
            viewData["ErrorMessage"] = errorMessage;

            viewResult.ViewData = viewData;
            context.Result = viewResult;
        }
    }
}
