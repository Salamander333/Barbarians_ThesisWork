using Barbarians.Data;
using Barbarians.Data.GlobalEnums;
using Barbarians.Models;
using Barbarians.Services;
using BarbariansTests.Common;
using BarbariansTests.Common.Seeders;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BarbariansTests.Services
{
    public class TasksServiceTests
    {
        [Fact]
        public async Task GenerateGatheringTaskShouldAddTaskToDb()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tasksService = this.GetTasksService(context);

            var seeder = new UsersSeeder();
            await seeder.SeedUsers(context);

            await tasksService.GenerateGatheringTask(MaterialType.Metal.ToString(), AdventureDifficulties.Hard.ToString(), "Id1");

            var result = context.TasksGather.Any(x => x.UserId == "Id1");

            Assert.True(result == true, "User doesn't have generated gathering task.");
        }

        [Fact]
        public void ValidGatheringTaskShouldReturnTrue()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tasksService = this.GetTasksService(context);

            var result = tasksService.IsGatheringTaskValid(MaterialType.Cloth.ToString(), AdventureDifficulties.Easy.ToString());

            Assert.True(result == true, "Validity check returns task is invalid when it is.");
        }

        [Fact]
        public void InvalidGatheringTaskShouldReturnFalse()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tasksService = this.GetTasksService(context);

            var result = tasksService.IsGatheringTaskValid("invalidParam", "invalidParam");

            Assert.True(result == false, "Validity check returns task is valid when it isn't.");
        }

        [Fact]
        public async Task CheckIfActiveTaskIsComplete()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tasksService = this.GetTasksService(context);

            var seeder = new UsersSeeder();
            await seeder.SeedUsers(context);

            var user1 = context.Users.First(x => x.UserName == "User1");
            var user2 = context.Users.First(x => x.UserName == "User2");

            await context.TasksGather.AddAsync(new TaskGather
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user1.Id,
                IsComplete = false,
                EndTime = DateTime.UtcNow.AddDays(-1),
            });

            await context.TasksGather.AddAsync(new TaskGather
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user2.Id,
                IsComplete = false,
                EndTime = DateTime.UtcNow.AddDays(1)
            });

            await context.SaveChangesAsync();

            var returnsTrue = await tasksService.CheckGatheringTaskCompletion(user1.Id);
            var returnsFalse = await tasksService.CheckGatheringTaskCompletion(user2.Id);

            Assert.True(returnsTrue == true, $"User didn't complete his task.");
            Assert.True(returnsFalse == false, $"User completed his task when he shouldn't have.");
        }

        [Fact]
        public async Task DoesUserHaveActiveGatherTasks()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tasksService = this.GetTasksService(context);

            var seeder = new UsersSeeder();
            await seeder.SeedUsers(context);

            var userDoentHaveActiveTask = context.Users.First(x => x.UserName == "User1");
            var userHasActiveTask = context.Users.First(x => x.UserName == "User2");

            await context.TasksGather.AddAsync(new TaskGather
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userDoentHaveActiveTask.Id,
                IsComplete = true,
            });

            await context.TasksGather.AddAsync(new TaskGather
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userHasActiveTask.Id,
                IsComplete = false,
            });

            await context.SaveChangesAsync();

            var returnsFalse = tasksService.HasActiveTask(userDoentHaveActiveTask.Id, "Gather");
            var returnsTrue = tasksService.HasActiveTask(userHasActiveTask.Id, "Gather");

            Assert.True(returnsFalse == false, $"User has an active task.");
            Assert.True(returnsTrue == true, $"User doen't have an active task.");
        }

        private TasksService GetTasksService(ApplicationDbContext context)
        {
            var tasksService = new TasksService(context);

            return tasksService;
        }
    }
}
