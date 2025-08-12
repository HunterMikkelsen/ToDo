namespace ToDo.Models
{
	public class TODOItem
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; }
		public Priority Priority { get; set; }
		public Status Status { get; set; }
	}

	public enum Priority
	{
		Lowest,
		Low,
		Medium,
		High,
		Highest
	}
	public enum Status
	{
		Backlog,
		InProgress,
		Completed,
		Cancelled
	}
}
