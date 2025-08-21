using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
	public class TODOItemsController : Controller
	{
		private readonly ToDoContext _context;

		public TODOItemsController(ToDoContext context)
		{
			_context = context;
		}

		// GET: TODOItems
		public async Task<IActionResult> Index(string selectedPriority, string searchString)
		{
			if(_context.TODOItem == null)
			{
				return Problem("Entity set 'ToDoContext.TODOItem'  is null.");
			}

			IQueryable<Priority> priorityQuery = from m in _context.TODOItem
												   orderby m.Priority
												   select m.Priority;

			var items = from i in _context.TODOItem
						 select i;
			if (!String.IsNullOrEmpty(searchString))
			{
				items = items.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
			}
			if (!String.IsNullOrEmpty(selectedPriority) && Enum.TryParse(selectedPriority, out Priority result))
			{
				items = items.Where(x => x.Priority == result);
			}

			var itemPriorityVM = new ItemPriorityViewModel
			{
				Priorities = new SelectList(await priorityQuery.Distinct().ToListAsync()),
				Items = await items.ToListAsync()
			};
			return View(itemPriorityVM);
		}

		// GET: TODOItems/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var tODOItem = await _context.TODOItem
				.FirstOrDefaultAsync(m => m.Id == id);
			if (tODOItem == null)
			{
				return NotFound();
			}

			return View(tODOItem);
		}

		// GET: TODOItems/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: TODOItems/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Description,Priority,Status")] TODOItem tODOItem)
		{
			if (ModelState.IsValid)
			{
				_context.Add(tODOItem);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(tODOItem);
		}

		// GET: TODOItems/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var tODOItem = await _context.TODOItem.FindAsync(id);
			if (tODOItem == null)
			{
				return NotFound();
			}
			return View(tODOItem);
		}

		// POST: TODOItems/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Priority,Status")] TODOItem tODOItem)
		{
			if (id != tODOItem.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(tODOItem);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!TODOItemExists(tODOItem.Id))
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
			return View(tODOItem);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangeTaskStatus(int id, Status newStatus)
		{
			var item = await _context.TODOItem.FindAsync(id);
			if (item == null)
			{
				return NotFound();
			}

			item.Status = newStatus;
			_context.Update(item);
			int numAffected = await _context.SaveChangesAsync();

			if(numAffected == 1)
			{
				var response = Json(new { success = true });
				return Ok(response);
			}
			return NotFound();
		}


		// GET: TODOItems/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var tODOItem = await _context.TODOItem
				.FirstOrDefaultAsync(m => m.Id == id);
			if (tODOItem == null)
			{
				return NotFound();
			}

			return View(tODOItem);
		}

		// POST: TODOItems/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var tODOItem = await _context.TODOItem.FindAsync(id);
			if (tODOItem != null)
			{
				_context.TODOItem.Remove(tODOItem);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool TODOItemExists(int id)
		{
			return _context.TODOItem.Any(e => e.Id == id);
		}
	}
}
