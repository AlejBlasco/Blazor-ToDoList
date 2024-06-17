namespace ToDoList.Application.Tasks;

public class Task
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
