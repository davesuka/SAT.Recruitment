using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SAT.Recruitment.Business.DTO;
using SAT.Recruitment.Business.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<ActionResult<ResultDTO>> CreateUser(New.Execute data)
        {
            return await _mediator.Send(data);
        }
        
    }
    
}
