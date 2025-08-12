using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Data;

namespace ToDo.Models
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new ToDoContext(
				serviceProvider.GetRequiredService<
					DbContextOptions<ToDoContext>>()))
			{
				if (context.TODOItem.Any())
				{
					return;   // DB has been seeded
				}
				context.TODOItem.AddRange(
					new TODOItem
					{
						Title = "Create the TODO app",
						Description = "This is the current objective.",
						Priority = Priority.Low,
						Status = Status.InProgress
					},
					new TODOItem
					{
						Title = "Add search functionality",
						Description = "This would be a nice QoL feature if there are a lot of TODO items.",
						Priority = Priority.Medium,
						Status = Status.Backlog
					},
					new TODOItem
					{
						Title = "Add user authentication",
						Description = "We should have the ability to have different users. They also shouldn't have access to other user data.",
						Priority = Priority.High,
						Status = Status.Backlog
					},
					new TODOItem
					{
						Title = "Add light / dark mode",
						Description = "Another nice QoL feature that will make the site more accessible.",
						Priority = Priority.Low,
						Status = Status.InProgress
					},
					new TODOItem
					{
						Title = "Enable a zebra to use the site",
						Description = "Zebras also have things they want to get done, let's make the site usable by a zerbra.",
						Priority = Priority.Highest,
						Status = Status.Cancelled
					}
				);
				context.SaveChanges();
			}
		}
	}
}
