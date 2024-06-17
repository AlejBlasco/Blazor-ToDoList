using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace TodoList.Application.UnitTest.Tasks;

public class TaskServiceTests
{
    private readonly ToDoList.Application.Tasks.ITaskService taskService;

    public TaskServiceTests()
    {
        taskService = new ToDoList.Application.Tasks.TaskService();
    }

    [Fact]
    public void Controller_should_not_throw_exception()
    {
        // Arrange
        ToDoList.Application.Tasks.ITaskService? testService = null;

        // Act
        var act = () =>
        { 
            testService = new ToDoList.Application.Tasks.TaskService();
        };

        // Assert
        act.Should().NotThrow<Exception>();
        testService.Should().NotBeNull();
    }

    [Fact]
    public async Task GetTasksAsync_should_throw_ObjectDisposedException_if_cancel()
    {
        // Arrange
        IEnumerable<ToDoList.Application.Tasks.Task>? response = null;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            response = await taskService.GetTasksAsync(cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ObjectDisposedException>();
    }

    [Fact]
    public async Task GetTasksAsync_should_not_throw_exception()
    {
        // Arrange
        IEnumerable<ToDoList.Application.Tasks.Task>? response = null;

        // Act
        var act = async () =>
        {
            response = await taskService.GetTasksAsync();
        };

        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetTasksAsync_should_not_be_null()
    {
        // Arrange
        IEnumerable<ToDoList.Application.Tasks.Task>? response = null;

        // Act
        var act = async () =>
        {
            response = await taskService.GetTasksAsync();
        };
        await act();

        // Assert
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task GetTasksAsync_should_have_zero_or_more_items()
    {
        // Arrange
        IEnumerable<ToDoList.Application.Tasks.Task>? response = null;

        // Act
        var act = async () =>
        {
            response = await taskService.GetTasksAsync();
        };
        await act();

        // Assert
        response.Should().HaveCountGreaterThanOrEqualTo(0);
    }

    [Fact]
    public async Task GetTaskAsync_should_throw_ObjectDisposedException_if_cancel()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            response = await taskService.GetTaskAsync(Guid.NewGuid(), cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ObjectDisposedException>();
    }

    [Fact]
    public async Task GetTaskAsync_should_not_throw_exception()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;

        // Act
        var act = async () =>
        {
            response = await taskService.GetTaskAsync(Guid.NewGuid());
        };

        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetTaskAsync_should_be_null_if_not_found()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;

        // Act
        var act = async () =>
        {
            response = await taskService.GetTaskAsync(Guid.NewGuid());
        };
        await act();

        // Assert
        response.Should().BeNull();
    }

    [Fact(Skip ="Unable to execute now")]
    public async Task GetTaskAsync_should_not_be_null_if_not_found()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;

        // Act
        var act = async () =>
        {
            response = await taskService.GetTaskAsync(Guid.NewGuid());
        };
        await act();

        // Assert
        response.Should().NotBeNull();
    }

    [Fact]
    public async Task AddTaskAsync_should_throw_ObjectDisposedException_if_cancel()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToDoList.Application.Tasks.Task inputTask = new ToDoList.Application.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Title = "Lorem ipsum",
            IsCompleted = false,
        };

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            response = await taskService.AddTaskAsync(inputTask, cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ObjectDisposedException>();
    }

    [Fact]
    public async Task AddTaskAsync_should_not_throw_exception_if_correct()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;
        ToDoList.Application.Tasks.Task inputTask = new ToDoList.Application.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Title = "Lorem ipsum",
            IsCompleted = false,
        };

        // Act
        var act = async () =>
        {
            response = await taskService.AddTaskAsync(inputTask);
        };

        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }

    [Fact(Skip ="Unable to execute now")]
    public async Task AddTaskAsync_should_throw_exception_if_exists()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;
        ToDoList.Application.Tasks.Task inputTask = new ToDoList.Application.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Title = "Lorem ipsum",
            IsCompleted = false,
        };

        // Act
        var act = async () =>
        {
            response = await taskService.AddTaskAsync(inputTask);
        };

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task DeleteTaskAsync_should_throw_ObjectDisposedException_if_cancel()
    {
        // Arrange
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToDoList.Application.Tasks.Task inputTask = new ToDoList.Application.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Title = "Lorem ipsum",
            IsCompleted = false,
        };

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            await taskService.DeleteTaskAsync(inputTask.Id, cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ObjectDisposedException>();
    }

    [Fact(Skip = "Unable to execute now")]
    public async Task DeleteTaskAsync_should_throw_exception_if_not_found()
    {
        // Arrange
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToDoList.Application.Tasks.Task inputTask = new ToDoList.Application.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Title = "Lorem ipsum",
            IsCompleted = false,
        };

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            await taskService.DeleteTaskAsync(inputTask.Id, cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(Skip = "Unable to execute now")]
    public async Task DeleteTaskAsync_should_remove_record_from_list()
    {
        // Arrange
        IEnumerable<ToDoList.Application.Tasks.Task>? response = null;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToDoList.Application.Tasks.Task inputTask = new ToDoList.Application.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Title = "Lorem ipsum",
            IsCompleted = false,
        };

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            await taskService.DeleteTaskAsync(inputTask.Id, cancellationTokenSource.Token);

            response = await taskService.GetTasksAsync();
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().NotThrowAsync<ValidationException>();
        response.Should().NotContain(inputTask);
    }

    [Fact]
    public async Task DeleteCompleteTasksAsync_should_throw_ObjectDisposedException_if_cancel()
    {
        // Arrange
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            await taskService.DeleteCompleteTasksAsync(cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ObjectDisposedException>();
    }

    [Fact]
    public async Task DeleteCompleteTasksAsync_should_remove_record_from_list()
    {
        // Arrange
        IEnumerable<ToDoList.Application.Tasks.Task>? response = null;
        
        // Act
        var act = async () =>
        {
            await taskService.DeleteCompleteTasksAsync();

            response = await taskService.GetTasksAsync();
        };

        // Assert
        await act.Should().NotThrowAsync<ValidationException>();
        response.Should().NotContain(x => x.IsCompleted);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void DeleteTasks_should_throw_ObjectDisposedException_if_cancel(bool isComplete)
    {
        // Arrange
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        // Act
        cancellationTokenSource.Cancel();
        var act = () =>
        {
            taskService.DeleteAllTasksAsync(cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        act.Should().Throw<ObjectDisposedException>();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task DeleteTasks_should_remove_record_from_list(bool isComplete)
    {
        // Arrange
        IEnumerable<ToDoList.Application.Tasks.Task>? response = null;

        // Act
        var act = async () =>
        {
            await taskService.DeleteAllTasksAsync();

            response = await taskService.GetTasksAsync();
        };

        // Assert
        await act.Should().NotThrowAsync<ValidationException>();
        response.Where(t => t.IsCompleted == isComplete).Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task DeleteAllTasksAsync_should_throw_ObjectDisposedException_if_cancel()
    {
        // Arrange
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            await taskService.DeleteAllTasksAsync(cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ObjectDisposedException>();
    }

    [Fact]
    public async Task DeleteAllTasksAsync_should_remove_record_from_list()
    {
        // Arrange
        IEnumerable<ToDoList.Application.Tasks.Task>? response = null;

        // Act
        var act = async () =>
        {
            await taskService.DeleteAllTasksAsync();

            response = await taskService.GetTasksAsync();
        };

        // Assert
        await act.Should().NotThrowAsync<ValidationException>();
        response.Should().BeEmpty();
    }

    [Fact]
    public async Task EditTaskAsync_should_throw_ObjectDisposedException_if_cancel()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToDoList.Application.Tasks.Task inputTask = new ToDoList.Application.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Title = "Lorem ipsum",
            IsCompleted = false,
        };

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            response = await taskService.EditTaskAsync(inputTask, cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ObjectDisposedException>();
    }

    [Fact(Skip = "Unable to execute now")]
    public async Task EditTaskAsync_should_not_throw_exception_if_correct()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;
        ToDoList.Application.Tasks.Task inputTask = new ToDoList.Application.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Title = "Lorem ipsum",
            IsCompleted = false,
        };

        // Act
        var act = async () =>
        {
            response = await taskService.EditTaskAsync(inputTask);
        };

        // Assert
        await act.Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task EditTaskAsync_should_throw_exception_if_not_exists()
    {
        // Arrange
        ToDoList.Application.Tasks.Task? response = null;
        ToDoList.Application.Tasks.Task inputTask = new ToDoList.Application.Tasks.Task
        {
            Id = Guid.NewGuid(),
            Title = "Lorem ipsum",
            IsCompleted = false,
        };

        // Act
        var act = async () =>
        {
            response = await taskService.EditTaskAsync(inputTask);
        };

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CompleteTaskAsync_should_throw_ObjectDisposedException_if_cancel()
    {
        // Arrange
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            await taskService.CompleteTaskAsync(Guid.NewGuid(), cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ObjectDisposedException>();
    }

    [Fact]
    public async Task CompleteTaskAsync_should_throw_exception_if_not_found()
    {
        // Arrange

        // Act
        var act = async () =>
        {
            await taskService.CompleteTaskAsync(Guid.NewGuid());
        };

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(Skip = "Unable to execute now")]
    public async Task CompleteTaskAsync_should_throw_exception_if_already_complete()
    {
        // Arrange
        var taskId = Guid.NewGuid();

        // Act
        var act = async () =>
        {
            await taskService.CompleteTaskAsync(taskId);
        };

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(Skip = "Unable to execute now")]
    public async Task CompleteTaskAsync_should_change_isComplete_if_found()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        ToDoList.Application.Tasks.Task? response = null;

        // Act
        var act = async () =>
        {
            await taskService.CompleteTaskAsync(taskId);
            response = await taskService.GetTaskAsync(taskId);
        };

        // Assert
        await act.Should().ThrowAsync<Exception>();
        response.Should().NotBeNull();
        response?.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task UnCompleteTaskAsync_should_throw_ObjectDisposedException_if_cancel()
    {
        // Arrange
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        // Act
        cancellationTokenSource.Cancel();
        var act = async () =>
        {
            await taskService.UnCompleteTaskAsync(Guid.NewGuid(), cancellationTokenSource.Token);
        };
        cancellationTokenSource.Dispose();

        // Assert
        await act.Should().ThrowAsync<ObjectDisposedException>();
    }

    [Fact]
    public async Task UnCompleteTaskAsync_should_throw_exception_if_not_found()
    {
        // Arrange

        // Act
        var act = async () =>
        {
            await taskService.UnCompleteTaskAsync(Guid.NewGuid());
        };

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(Skip = "Unable to execute now")]
    public async Task UnCompleteTaskAsync_should_throw_exception_if_already_complete()
    {
        // Arrange
        var taskId = Guid.NewGuid();

        // Act
        var act = async () =>
        {
            await taskService.UnCompleteTaskAsync(taskId);
        };

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(Skip = "Unable to execute now")]
    public async Task UnCompleteTaskAsync_should_change_isComplete_if_found()
    {
        // Arrange
        var taskId = Guid.NewGuid();
        ToDoList.Application.Tasks.Task? response = null;

        // Act
        var act = async () =>
        {
            await taskService.UnCompleteTaskAsync(taskId);
            response = await taskService.GetTaskAsync(taskId);
        };

        // Assert
        await act.Should().ThrowAsync<Exception>();
        response.Should().NotBeNull();
        response?.IsCompleted.Should().BeFalse();
    }


}
