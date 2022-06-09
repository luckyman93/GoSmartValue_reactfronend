using AutoMapper;
using AV.Common.Entities;
using AV.Contracts.Enums;
using AV.Persistence.EntityFramework;
using GoSmartValue.Web.Controllers;
using GoSmartValue.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoSmartValue.Web.Areas.admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    // [Authorize(Roles = "Manager,Admin")]
    [Area("admin")]
    [Route("admin/comparables/")]
    public class ComparablesController : SecureController
    {
        private readonly ValuationsContext _context;

        public ComparablesController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ValuationsContext context,
            IUserManagerService userService,
            IMapper mapper

            )
        : base(userManager, roleManager, userService, mapper)
        {
            _context = context;
        }

        [Route("Index")]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int pageSize = 100, [FromQuery] int pageNumber = 1)
        {
            var valuationsContext = _context.Comparables
                .Where(c => c.DataState != DataState.Raw)
                .Include(p => p.Locality)
                .Include(p => p.Location)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .OrderByDescending(c => c.AddedOn);
            return View(await valuationsContext.ToListAsync());
        }

        // GET: admin/Comparables/Details/5
        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyDetail = await _context.Comparables
                .Include(p => p.Locality)
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyDetail == null)
            {
                return NotFound();
            }

            return View(propertyDetail);
        }

        // GET: admin/Comparables/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Id");
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id");
            return View();
        }

        // POST: admin/Comparables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("Id,AddedOn,DataState,DateOfSale,SalePrice,LocationId,LocalityId,StreetId,StreetName,PlotSize,PlotId,LandUse,Date,PropertyType,PlotNo")] Comparable comparable)
        {
            if (ModelState.IsValid)
            {
                comparable.Id = Guid.NewGuid();
                _context.Add(comparable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Id", comparable.LocalityId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", comparable.LocationId);
            return View(comparable);
        }

        // GET: admin/Comparables/Edit/5
        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyDetail = await _context.Comparables.FindAsync(id);
            if (propertyDetail == null)
            {
                return NotFound();
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Id", propertyDetail.LocalityId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", propertyDetail.LocationId);
            return View(propertyDetail);
        }

        // POST: admin/Comparables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AddedOn,DataState,DateOfSale,SalePrice,LocationId,LocalityId,StreetId,StreetName,PlotSize,PlotId,LandUse,Date,PropertyType,PlotNo")] Comparable comparable)
        {
            if (id != comparable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comparable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyDetailExists(comparable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "Id", comparable.LocalityId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", comparable.LocationId);
            return View(comparable);
        }

        [HttpGet("Delete")]
        // GET: admin/Comparables/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyDetail = await _context.Comparables
                .Include(p => p.Locality)
                .Include(p => p.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (propertyDetail == null)
            {
                return NotFound();
            }

            return View(propertyDetail);
        }

        // POST: admin/Comparables/Delete/5
        [HttpPost("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var propertyDetail = await _context.Comparables.FindAsync(id);
            _context.Comparables.Remove(propertyDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyDetailExists(Guid id)
        {
            return _context.Comparables.Any(e => e.Id == id);
        }
    }
}
