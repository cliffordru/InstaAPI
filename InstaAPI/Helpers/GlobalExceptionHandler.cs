using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Newtonsoft.Json;

namespace InstaAPI.Helpers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            //TODO: Add logging, finer grained info based on exception type, etc.            

            var compilationSection = (CompilationSection)System.Configuration.ConfigurationManager.GetSection(@"system.web/compilation");
            var content = ConfigurationData.ErrorMessage;
            
            
            if (compilationSection.Debug)
            {
                content = context.Exception.ToString();
            }

            var test = context.Exception;

            //var result = new HttpResponseMessage(HttpStatusCode.InternalServerError) 
            //{               
            //    ReasonPhrase = "An Unexpected error occured."
            //};

            var customError = new CustomError() {Info = ConfigurationData.ErrorInfo, Message = content};

            var jsonType = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, customError, jsonType);

            //context.Result = new ExceptionResult(context.Request, result);
            context.Result = new ResponseMessageResult(response);
        }

        public class CustomError
        {
            [JsonProperty(PropertyName = "info")]
            public string Info { get; set; }
            [JsonProperty(PropertyName = "message")]
            public string Message { get; set; }
        }

        //public class ExceptionResult : IHttpActionResult
        //{
        //    private HttpRequestMessage _request;
        //    private HttpResponseMessage _httpResponseMessage;


        //    public ExceptionResult(HttpRequestMessage request, HttpResponseMessage httpResponseMessage)
        //    {
        //        _request = request;
        //        _httpResponseMessage = httpResponseMessage;
        //    }

        //    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        //    {
        //        return Task.FromResult(_httpResponseMessage);
        //    }
        //}
    }
}