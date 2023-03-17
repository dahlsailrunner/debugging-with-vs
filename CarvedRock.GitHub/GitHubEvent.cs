using System.Text.Json;

namespace CarvedRock.GitHub;

public class GitHubEvent
{
    public GitHubRepository? Repository { get; set; }
    public GitHubPusher? Pusher { get; set; }
    public string Compare { get; set; } = null!;
    public GitHubHeadCommit? HeadCommit { get; set; }
    public override string ToString()
    {
        return $"repo:{Repository?.Name};pusher:{Pusher?.Name};Message:{HeadCommit?.Message}";
    }
}

public class GitHubHeadCommit
{
    public string Id { get; set; } = null!;
    public bool Distinct { get; set; }
    public string Message { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public string Url { get; set; } = null!;
    public GitHubUser? Author { get; set; }
    public GitHubUser? Committer { get; set; }
    public List<string> Added { get; set; } = new();
    public List<string> Removed { get; set; } = new();
    public List<string> Modified { get; set; } = new();
}

public class GitHubUser
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
}

public class GitHubPusher
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}

public class GitHubRepository
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Url { get; set; } = null!;
    public long CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long PushedAt { get; set; }
    public string DefaultBranch { get; set; } = null!;
}

public static class StringUtils
{
    public static string ToSnakeCase(this string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }
}

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public static SnakeCaseNamingPolicy Instance { get; } = new();

    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}