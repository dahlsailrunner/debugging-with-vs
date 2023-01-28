# Debugging with Visual Studio (2022)

This is the repository with the code associated with a Pluralsight course called "Debugging with Visual Studio 2022".

Repo URL: [https://github.com/dahlsailrunner/aspnet6-performance](https://github.com/dahlsailrunner/debugging-with-vs)

## Running the Solution
In Visual Studio with the solution open, the **Set Startup Projects** should be chosen
and then both `CarvedRock.Api` and `CarvedRock.WebApp` should be set to **Start**.

## Data
The database for this project is SqLite, and a database is 
automatically created at startup in the 
`Environment.SpecialFolder.LocalApplicationData` folder. 
(`C:\Users\USERNAME\AppData\Local` on Windows and 
`/Users/USERNAME/.local/share` on Mac):  `carvedrock.db`


## Seq (optional)

The Docker image for Seq can used for logging and instrumention in this code as well.  Use the following commands:

```bash
docker pull datalust/seq
docker run -d --name seq --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq
```

The NuGet package used for this is [Serilog.Sinks.Seq](https://www.nuget.org/packages/Serilog.Sinks.Seq)

To enable the logging for Seq, comment in the following line 
in `Program.cs` in both the API and WebApp projects:

```csharp
//.WriteTo.Seq("http://localhost:5341")
```
