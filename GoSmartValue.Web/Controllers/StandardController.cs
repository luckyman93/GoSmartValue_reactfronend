using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Enums;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GoSmartValue.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class StandardController : SecureController
    {
        private readonly IInstructionService _instructionService;
        public StandardController(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IInstructionService instructionService,
            IUserManagerService usersService,
            IMapper mapper
            )
            : base(userManager, roleManager, usersService, mapper)
        {
            _instructionService = instructionService;
        }

        [HttpGet]
        [Route("[controller]/completed")]
        public IActionResult CompleteInstructions()
        {
            var instructions =
                _instructionService.GetInstructions(CurrentUser, new List<InstructionStatus> { InstructionStatus.Completed });
            ViewBag.CurrentUserId = CurrentUser.Id;
            return View(instructions);
        }
        public IActionResult InstructionSent()
        {
            return View();
        }
        public IActionResult Administration()
        {
            return View();
        }
    }
}