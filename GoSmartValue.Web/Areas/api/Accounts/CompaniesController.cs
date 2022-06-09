using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using AV.Common.Interfaces;
using AV.Contracts.Enums;
using AV.Contracts.Models.Accounts;
using AV.Contracts.Models.Accounts.Commands;
using AV.Contracts.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoSmartValue.Web.Areas.api.Accounts
{
    /// <summary>
    /// Managing Company and firms with whom valuers and corporate users are associated
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiTokenAuth]
    [Authorize(AuthenticationSchemes = ApiConstants.AuthenticationSchemes)]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private static List<Account> _accounts;

        public CompaniesController(IMediator mediator, IMapper mapper, UserManager<User> userManager,
            IAccountsRepository accountsRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
            _accounts = accountsRepository.GetAll().ToList();
        }

        [HttpPost]
        //[Authorize(Policy = Constants.AccessPolicies.InternalStaff)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateCompanyCommand createCompanyCommand)
        {
            return Ok(await _mediator.Send(createCompanyCommand));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCompaniesRequest()));
        }        
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetCompanyRequest(id)));
        }        
        
        [HttpPost]
        [Route("valuers")]
        [Authorize]
        public async Task<IActionResult> GetValuers([FromBody]List<Guid> companies)
        {
            return Ok(_mapper.Map<ICollection<ValuerViewModel>>(await _mediator.Send(new GetCompanyValuersRequest(companies))));
        }
        
        [HttpGet]
        [Route("valuer")]
        [Authorize]
        public IEnumerable<AccountViewModel> GetAccounts(AccountType accountType)
        {
            var accounts = _accounts.Where(a => a.AccountType == accountType
                                                && !string.IsNullOrEmpty(a.CompanyName));
            return _mapper.Map<ICollection<AccountViewModel>>(accounts.OrderBy(a => a.CompanyName));
        }
    }
}