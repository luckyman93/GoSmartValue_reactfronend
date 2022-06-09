using System.Threading.Tasks;
using AutoMapper;
using AV.Common.Entities;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoSmartValue.Web.Areas.admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Area("admin")]
    public class OrganisationController : SecureController
    {
        private readonly IUserManagerService _userManagerService;
        private readonly IMapper _mapper;
        private static string _viewsPath = "/areas/admin/Views/Organisation";
        private User _currentUser;

        public OrganisationController(IUserManagerService userManagerService, 
            UserManager<User> userManager, 
            RoleManager<Role> roleManager,
            IUserManagerService userService,
            IMapper mapper
            )
        : base(userManager, roleManager, userService, mapper)
        {
            _userManagerService = userManagerService;
            _mapper = mapper;
            _currentUser = userManagerService.GetUserDetails(User.Identity.Name);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var organisations = _userManagerService.GetOrganistions(CurrentUser.Id);
            ViewBag.userId = CurrentUser.Id;
            return View($"{_viewsPath}/Index.cshtml", organisations);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View($"{_viewsPath}/Create.cshtml");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View($"{_viewsPath}/Create.cshtml");
        }

        // POST: Organisation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Edit");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}