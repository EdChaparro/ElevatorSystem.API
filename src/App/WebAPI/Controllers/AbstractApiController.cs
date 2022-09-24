using System;
using IntrepidProducts.RequestResponse.Requests;
using IntrepidProducts.RequestResponse.Responses;
using IntrepidProducts.RequestResponseHandler.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using IntrepidProducts.WebAPI.Models;
using Microsoft.AspNetCore.Routing;

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
            ExecutionStrategy executionStrategy = ExecutionStrategy.Sequential,
            bool ignoreErrors = false)
            where TRequest : class, IRequest
            where TResponse : class, IResponse
        {
            var requestBlock = new RequestBlock { ExecutionStrategy = executionStrategy };

            foreach (var request in requests)
            {
                requestBlock.Add(request);
            }

            var responseBlock = _requestHandlerProcessor.Process(requestBlock);

            if (!ignoreErrors)
            {
                if (responseBlock.HasErrors)
                {
                    Problem("Errors encountered processing request(s)", null,
                        StatusCodes.Status500InternalServerError);
                }
            }

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

        protected Link GenerateGetByIdUri
            (HttpContext httpContext, string methodName, Guid id)
        {
            var link = LinkGenerator.GetUriByAction
                (httpContext, methodName, values: new { id });

            return new Link(link, GetType().Name, methodName);
        }
        #endregion
    }
}