using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
	public class ItemPriorityViewModel
	{
		public List<TODOItem>? Items { get; set; }
		public SelectList? Priorities { get; set; }
		[Display(Name ="Priority")]
		public string? SelectedPriority { get; set; }
		public string? SearchString { get; set; }
	}
}
