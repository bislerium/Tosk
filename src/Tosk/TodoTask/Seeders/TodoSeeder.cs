using Bogus;
using Tosk.TodoTask.Models;

namespace Tosk.TodoTask.Seeders;

public class TodoSeeder
{
    private static readonly Lazy<List<string>> ResolutionIdeas = new(() => new List<string>
    {
        "Exercise daily",
        "Read 12 books this year",
        "Learn a new skill or hobby",
        "Save more money",
        "Improve time management",
        "Spend more time with family and friends",
        "Travel to a new place",
        "Eat healthier meals",
        "Quit a bad habit",
        "Start journaling regularly",
        "Volunteer in the community",
        "Practice mindfulness or meditation",
        "Set a monthly budget and stick to it",
        "Reduce screen time",
        "Learn a new language",
        "Take an online course",
        "Create a better sleep routine",
        "Focus on personal growth",
        "Be more eco-friendly",
        "Run a marathon or complete a fitness goal"
    });


    public static ICollection<Todo> GenerateTodos()
    {
        var ideas = ResolutionIdeas.Value;
        var faker = new Faker();
        return ideas
            .OrderBy(_ => faker.Random.Int())
            .Take(ideas.Count)
            .Select(title =>
            {
                var createdAt = faker.Date.Past(1); // Random date within the past year
                var isCompleted = faker.Random.Bool(0.6f); // 60% chance of being completed
                var completedAt = isCompleted
                    ? (DateTime?)faker.Date.Between(createdAt, DateTime.Now) // Ensure completion is after creation
                    : null;

                return new Todo
                {
                    Title = title,
                    CreatedAt = createdAt,
                    IsCompleted = isCompleted,
                    CompletedAt = completedAt,
                    IsImportant = faker.Random.Bool(0.5f)
                };
            }).ToList();
    }
}
