using System.Collections.Generic;
using AV.Contracts.Models.Accounts.Subscriptions.Command;
using AV.Contracts.Models.Product.Commands;
using AV.Contracts.Models.Product.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Areas.api.Products
{
    [ApiController]
    [ApiTokenAuth]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new Product
        /// </summary>
        /// <param name="createProductCommand"></param>
        /// <returns>Created Product</returns>
        [HttpPost(ApiConstants.Routes.Product.CreateProduct)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
            return Created(nameof(CreateProduct), await _mediator.Send(createProductCommand));
        }

        /// <summary>
        /// Get all available products
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiConstants.Routes.Product.AllProducts)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _mediator.Send(new GetProductsRequest()));
        }

        /// <summary>
        /// Create a new Package for account Subscriptions
        /// </summary>
        /// <param name="createPackageCommand"></param>
        /// <returns></returns>
        [HttpPost(ApiConstants.Routes.Packages.CreatePackage)]
        public async Task<IActionResult> CreatePackage([FromBody] CreatePackageCommand createPackageCommand)
        {
            return Created(nameof(CreatePackage), await _mediator.Send(createPackageCommand));
        }

        /// <summary>
        /// Create a new Packages for account Subscriptions
        /// </summary>
        /// <param name="createPackageCommands"></param>
        /// <returns></returns>
        [HttpPost(ApiConstants.Routes.Packages.CreatePackages)]
        public async Task<IActionResult> CreatePackages([FromBody] List<CreatePackageCommand> createPackageCommands)
        {
            return Created(nameof(CreatePackages), await _mediator.Send(new CreatePackagesCommand(createPackageCommands)));
        }

        /// <summary>
        /// Get all available subscription packages
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiConstants.Routes.Packages.AllPackage)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetPackagesRequest()));
        }

        // update package

        // disable a package

        // Add features and benefits

        // remove feature
    }
}
