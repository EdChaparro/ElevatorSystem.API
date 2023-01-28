using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using IntrepidProducts.WebAPI.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntrepidProducts.WebAPI.Controllers
{
    public abstract class AbstractApiController : ControllerBase
    {
        protected AbstractApiController
            (IRequestHandlerProcessor requestHandlerProcessor,
                LinkGenerator linkGenerator)
        {
            _requestHandlerProcessor = requestHandlerProcessor;
            LinkGenerator = linkGenerator;
        }

        #region Process Request(s)
        private readonly IRequestHandlerProcessor _requestHandlerProcessor;

        protected IEnumerable<TResponse> ProcessRequests<TRequest, TResponse>
            (params TRequest[] requests)
            where TRequest : class, IRequest
            where TResponse : class, IResponse
        {
            return ProcessRequests<TRequest, TResponse>(requests.ToList());
        }

        protected IEnumerable<TResponse> ProcessRequests<TRequest, TResponse>
        (IEnumerable<TRequest> requests,
            ExecutionStrategy executionStrategy = ExecutionStrategy.Sequential)
            where TRequest : class, IRequest
            where TResponse : class, IResponse
        {
            var requestBlock = new RequestBlock { ExecutionStrategy = executionStrategy };

            foreach (var request in requests)
            {
                requestBlock.Add(request);
            }

            var responseBlock = _requestHandlerProcessor.Process(requestBlock);

            var responses = new List<TResponse>();
            foreach (var response in responseBlock.Responses)
            {
                responses.Add((TResponse)response);
            }

            return responses;
        }
        #endregion

        #region HATEOAS
        protected LinkGenerator LinkGenerator { get; }

        private readonly string? _controllerName = null;

        private string ControllerName
        {
            get
            {
                if (_controllerName != null)
                {
                    return _controllerName;
                }

                return StrippedControllerName(GetType().Name);
            }
        }

        protected static string StrippedControllerName(string controllerClassName)
        {
            return controllerClassName[..^10];
        }

        protected Link GenerateActionByIdUri
            (string methodName, Guid id, string relation = "self")
        {
            return GenerateUri(methodName, values: new { id }, relation, id.ToString());
        }

        protected Link GenerateUri(string methodName, object values,
            string relation = "self", string id = "", string? controllerName = null)
        {
            var uri = LinkGenerator.GetUriByAction
                (HttpContext, methodName, controllerName, values);

            return new Link(uri, relation, methodName, id);
        }

        #endregion

        protected ObjectResult GetProblemDetails(IResponse response)
        {
            var errorInfo = response.ErrorInfo;
            var requestName = response.OriginalRequest.GetType().Name;

            var message = $"Error encountered in Request: {requestName}, " +
                          $"Error: {errorInfo?.ErrorId} - {errorInfo?.Message}";

            return Problem
                (message, null, StatusCodes.Status500InternalServerError);
        }
    }
}