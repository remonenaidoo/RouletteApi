using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RouletteWebApi.DataObjects.DataObjects;
using RouletteWebApi.LogicLayer.DataAccessLayer.Interfaces;
using RouletteWebApi.LogicLayer.DataAccessLayer.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RouletteWebApi.Filters
{
    public class DuplicatePayoutCheckFilter : TypeFilterAttribute
    {

        public DuplicatePayoutCheckFilter() : base(typeof(DuplicateActionFilter))
        {

        }
        public class DuplicateActionFilter : IAsyncActionFilter
        {
            private readonly IOptions<AppSettings> _appConfig;
            private readonly IRepoWrapper _repoWrapper;
            public DuplicateActionFilter(IOptions<AppSettings> appConfig, IRepoWrapper repoWrapper)
            {     
                _appConfig = appConfig;
                _repoWrapper = repoWrapper;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (context.ActionArguments.TryGetValue(_appConfig.Value.PayoutRequestParameter, out object ObjectValue))
                {
                    var BetReference = ObjectValue.GetType().GetProperty(_appConfig.Value.BetReferenceProperty).GetValue(ObjectValue, null);

                    var BetExists = await _repoWrapper.Payout.RetrievePayoutInfo(BetReference.ToString());

                    if (BetExists.IsSuccess)
                    {
                        context.Result = new ContentResult()
                        {
                            StatusCode = (int)HttpStatusCode.OK,
                            Content = JsonConvert.SerializeObject("Transaction has already been paid out")
                        };
                    }
                    else
                    {
                        await next();
                    }

                }
                else
                {
                    await next();
                }

            }
        }
    }
}