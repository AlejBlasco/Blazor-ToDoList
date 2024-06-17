using System.ComponentModel.DataAnnotations;

namespace ToDoList.Application.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly IList<Tasks.Task> tasksInMemory;

        public TaskService()
        {
            tasksInMemory = new List<Tasks.Task>();
        }

        public async Task<Tasks.Task?> GetTaskAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Tasks.Task? response = null;

            await System.Threading.Tasks.Task.Run(() =>
            {
                response = tasksInMemory.FirstOrDefault(t => t.Id == id);

            }, cancellationToken);

            return response;
        }

        public async Task<IEnumerable<Tasks.Task>> GetTasksAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Tasks.Task> response = new List<Tasks.Task>();

            await System.Threading.Tasks.Task.Run(() =>
            {
                response = tasksInMemory;
            }, cancellationToken);

            return response;
        }

        public async Task<Tasks.Task> AddTaskAsync(Tasks.Task task, CancellationToken cancellationToken = default)
        {
            Tasks.Task? response = null;

            await System.Threading.Tasks.Task.Run(() =>
            {
                if (GetTaskAsync(task.Id, cancellationToken).Result != null)
                    throw new ValidationException("This task already exists");

                tasksInMemory.Add(task);

                response = GetTaskAsync(task.Id, cancellationToken).Result;
                if (response == null)
                    throw new ValidationException($"Operation failed for Task:{task.Id}");

            }, cancellationToken);

            return response!;
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                var foundTask = await GetTaskAsync(id, cancellationToken);
                if (foundTask == null)
                    throw new ValidationException("This task not exists");
                else
                    tasksInMemory.Remove(foundTask);

            }, cancellationToken);
        }

        private void DeleteTasks(bool IsComplete)
        {
            var removedItems = tasksInMemory.Where(t => t.IsCompleted = IsComplete)
                .ToList();

            if (removedItems.Any())
            {
                for (var itemCount = 0; itemCount < removedItems.Count; itemCount++)
                {
                    tasksInMemory.Remove(removedItems[itemCount]);
                }
            }
        }

        public async System.Threading.Tasks.Task DeleteCompleteTasksAsync(CancellationToken cancellationToken = default)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                DeleteTasks(true);
            }, cancellationToken);
        }

        public async System.Threading.Tasks.Task DeleteAllTasksAsync(CancellationToken cancellationToken = default)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                tasksInMemory.Clear();
            }, cancellationToken);
        }

        public async Task<Tasks.Task> EditTaskAsync(Tasks.Task task, CancellationToken cancellationToken = default)
        {
            Tasks.Task? response = null;

            await System.Threading.Tasks.Task.Run(async () =>
            {
                await DeleteTaskAsync(task.Id);
                response = await AddTaskAsync(task, cancellationToken);
            }, cancellationToken);

            return response!;
        }

        private async System.Threading.Tasks.Task ToggleTaskAsync(Guid id, bool isComplete, CancellationToken cancellationToken = default)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                var selectedTask = await GetTaskAsync(id);
                if (selectedTask == null)
                    throw new ValidationException("This task not exists");

                if (selectedTask.IsCompleted == isComplete)
                    throw new ValidationException("This task has the same state");

                selectedTask.IsCompleted = isComplete;
                await EditTaskAsync(selectedTask, cancellationToken);
            }, cancellationToken);
        }

        public async System.Threading.Tasks.Task CompleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                await ToggleTaskAsync(id, true, cancellationToken);
            }, cancellationToken);
        }

        public async System.Threading.Tasks.Task UnCompleteTaskAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await System.Threading.Tasks.Task.Run(async () =>
            {
                await ToggleTaskAsync(id, false, cancellationToken);
            }, cancellationToken);
        }
    }
}
