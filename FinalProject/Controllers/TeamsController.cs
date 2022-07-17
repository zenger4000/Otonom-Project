﻿using FinalProject.Data;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    [Authorize(Roles ="admin")]
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TeamsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams
                .FirstOrDefaultAsync(m => m.id == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,description,Picture,Facebook,Twitter,Instagram,Linkdin,position")] Teamsviewmodel vm)
        {
            string stringFileName = UploadFile(vm);
            var teams = new Teams
            {
                Name = vm.Name,
                Picture = stringFileName,
                description = vm.description,
                position = vm.position,
                Facebook = vm.Facebook
            ,
                Linkdin = vm.Linkdin
            };
            if (ModelState.IsValid)
            {
                _context.Add(teams);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teams);
        }
        private string UploadFile(Teamsviewmodel vm)
        {
            string fileName = null;
            if (vm.Picture != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "TeamsImages");
                fileName = Guid.NewGuid().ToString() + "-" + vm.Picture.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.Picture.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams.FindAsync(id);
            if (teams == null)
            {
                return NotFound();
            }
            return View(teams);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,description,Picture,Facebook,Twitter,Instagram,Linkdin,position")] Teams teams)
        {
            if (id != teams.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teams);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamsExists(teams.id))
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
            return View(teams);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teams = await _context.Teams
                .FirstOrDefaultAsync(m => m.id == id);
            if (teams == null)
            {
                return NotFound();
            }

            return View(teams);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teams = await _context.Teams.FindAsync(id);
            _context.Teams.Remove(teams);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamsExists(int id)
        {
            return _context.Teams.Any(e => e.id == id);
        }
    }
}
