using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Moviefy.Data;
using Moviefy.Models;

namespace Moviefy.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: Movies
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Movie.ToListAsync());
        }
        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            return View(await _context.Movie.ToListAsync());
        }
        [Authorize]
        public async Task<IActionResult> Watch()
        {
            TempData["AlertMessage"] = "Enjoy the Movie!";
            return View(await _context.Movie.ToListAsync());
        }

        // GET: Movies/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        // GET: Movies/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Genre,DateRelease")] MovieModel movieModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieModel);
        }

        // GET: Movies/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await _context.Movie.FindAsync(id);
            if (movieModel == null)
            {
                return NotFound();
            }
            return View(movieModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,DateRelease")] MovieModel movieModel)
        {
            if (id != movieModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieModelExists(movieModel.Id))
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
            return View(movieModel);
        }

        // GET: Movies/Delete/5
        [Authorize(Policy = "RequireUserAndrea")]
        public async Task<IActionResult> Delete(int? id)
        {
            //I first used an if statement to implement security features for a specific user then went with [Authorize(Policy)] instead
            //if (User.Identity.Name != "andrea@email.com")
            //{
            //    return View("AccessDenied");
            //    //add view saying they can not access this page
            //}
            if (id == null)
            {
                return NotFound();
            }

            var movieModel = await _context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireUserAndrea")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            //if(User.Identity.Name != "andrea@email.com") //I first used an if statement to implement security features for a specific user then went with [Authorize(Policy)] instead
          //    {
          //        return View("AccessDenied");
          //        //add view saying they can not access this page
          //    }
            var movieModel = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movieModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieModelExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
    }
