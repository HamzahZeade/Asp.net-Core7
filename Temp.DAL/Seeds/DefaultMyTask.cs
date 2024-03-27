using Newtonsoft.Json;
using Temp.DAL.Data;
using Temp.Models.Entities.Models;

namespace Temp.DAL.Seeds
{





    public static class DefaultMyTask
    {
        //public static async Task SeedAsync(ApplicationDbContext dbContext)
        //{
        //    await SeedTaskAsync(dbContext, new MyTask
        //    {
        //        Description = "Description of the task",
        //        CalendarId = 1, // Example value
        //        EndDate = DateTime.Parse("2020-12-15T10:00:00"),
        //        Text = "Sales Management",
        //        Company = "ElectrixMax",
        //        Priority = TaskPriority.High,
        //        StartDate = DateTime.Parse("2020-11-23T10:00:00"),
        //        DueDate = DateTime.Parse("2020-12-10T10:00:00"),
        //        Owner = "Sammy Hill",
        //        Status = Models.Entities.Models.TaskStatus.InProgress,
        //        ParentId = Guid.NewGuid(),
        //        Progress = 55
        //    });
        //    // Add more tasks here if needed
        //}
        public static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            // Read task data from JSON file
            var tasksJson = await File.ReadAllTextAsync("D:\\Projects\\Template\\Temp.DAL\\tasksjsonSeed\\tasks.json");
            var tasks = JsonConvert.DeserializeObject<List<MyTask>>(tasksJson);
            // Seed each task
            foreach (var task in tasks)
            {
                await SeedTaskAsync(dbContext, task);
            }
        }
        private static async Task SeedTaskAsync(ApplicationDbContext context, MyTask task)
        {
            // Check if the task already exists in the database based on some criteria, 
            // for example, you can check if a task with the same text and owner exists
            if (context.MyTasks.Any(t => t.Text == task.Text && t.Owner == task.Owner))
            {
                return; // Task already exists, no need to seed
            }

            // Add the task to the database
            context.MyTasks.Add(task);
            await context.SaveChangesAsync();
        }

    }

}
