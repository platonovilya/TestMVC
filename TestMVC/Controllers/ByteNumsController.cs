using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestMVC.Data;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class ByteNumsController : Controller
    {
        private readonly ByteNumContext _context;

        public ByteNumsController(ByteNumContext context)
        {
            _context = context;
        }

        // GET: ByteNums
        public async Task<IActionResult> Index()
        {
            return View(await _context.ByteNum.ToListAsync());
        }

        // GET: ByteNums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ByteNums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Binary")] ByteNum byteNum)
        {
            if (ModelState.IsValid)
            {
                calculateDec(byteNum);
                _context.Add(byteNum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(byteNum);
        }

        // GET: ByteNums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var byteNum = await _context.ByteNum.FindAsync(id);
            if (byteNum == null)
            {
                return NotFound();
            }
            return View(byteNum);
        }

        // POST: ByteNums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Binary")] ByteNum byteNum)
        {
            if (id != byteNum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    calculateDec(byteNum);
                    _context.Update(byteNum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ByteNumExists(byteNum.Id))
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
            return View(byteNum);
        }

        // GET: ByteNums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var byteNum = await _context.ByteNum
                .FirstOrDefaultAsync(m => m.Id == id);
            if (byteNum == null)
            {
                return NotFound();
            }

            return View(byteNum);
        }

        // POST: ByteNums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var byteNum = await _context.ByteNum.FindAsync(id);
            _context.ByteNum.Remove(byteNum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ByteNumExists(int id)
        {
            return _context.ByteNum.Any(e => e.Id == id);
        }

        /// <summary>
        /// Calculate Decimal represent of binary number
        /// </summary>
        /// <param name="byteNum"></param>
        private void calculateDec(ByteNum byteNum)
		{
            int[] binary = Array.ConvertAll(byteNum.Binary.Split(' '), Int32.Parse);
            int dec = 0;
			for (int i = 0; i < binary.Length; i++)
			{
                dec += binary[i] * Convert.ToInt32(Math.Pow(2, 7 - i));
			}
            byteNum.Dec = dec;
        }
    }
}
