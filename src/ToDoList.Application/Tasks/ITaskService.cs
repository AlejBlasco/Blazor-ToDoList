namespace ToDoList.Application.Tasks;

public interface ITaskService
{
    Task<IEnumerable<Tasks.Task>> GetTasksAsync(CancellationToken cancellationToken = default);

    Task<Tasks.Task?> GetTaskAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Tasks.Task> AddTaskAsync(Tasks.Task task, CancellationToken cancellationToken = default);

    System.Threading.Tasks.Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default);

    System.Threading.Tasks.Task DeleteCompleteTasksAsync(CancellationToken cancellationToken = default);

    System.Threading.Tasks.Task DeleteAllTasksAsync(CancellationToken cancellationToken = default);

    Task<Tasks.Task> EditTaskAsync(Tasks.Task task, CancellationToken cancellationToken = default);

    System.Threading.Tasks.Task CompleteTaskAsync(Guid id, CancellationToken cancellationToken = default);

    System.Threading.Tasks.Task UnCompleteTaskAsync(Guid id, CancellationToken cancellationToken = default);
}
