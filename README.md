# How to register DependencyInjection

Dapper.Extensions.Microsoft.DependencyInjection was created to achieve the same ease as EntityFramework's DbContext.
Using DependencyInjection we must add our DapperContext as shown in the Program.cs example.

The use of UseSqlServerUserSecrets serves to help in the configuration of the ConnectionString. By default it will search
in the appsettings.json file in the ConnectionStrings section DefaultConnection property, which can be changed when configuring dependency injection.

## How to change the property DefaultConnection of the DapperContext
```csharp
    services.AddDapperDbContext<MyDapperContext>(p => p.UseSqlServerUserSecrets(hostContext.Configuration, "MyDefaultConnection"), ServiceLifetime.Singleton);
```

## How the ConnectionString password is retrieved.

The DependencyInjection looks for the configuration of appsettings, concatenating the name of the ConnectionString + Password, by default DefaultConnectionPassword.
Case another name  such especify, as MyDefaultConnection, it will look for the configuration key in property "MyDefaultConnectionPassword"

# How to use DapperContext
Below is an example from MyRepository:

```csharp
public class MyRepository
{
    private MyDapperContext _myDapperContext;

    public MyRepository(MyDapperContext myDapperContext)
    {
        _myDapperContext = myDapperContext;
    }

    public IEnumerable<Person> GetAll()
    {
        return _myDapperContext.Person.GetAll();
    }
}
```

#Below is an example from Program:
```csharp
Program.cs
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDapperDbContext<MyDapperContext>(p => p.UseSqlServerUserSecrets(hostContext.Configuration), ServiceLifetime.Singleton);
                    services.AddHostedService<Worker>();
                });
        }
    }
```

```csharp
MyDapperContext.cs
    public class MyDapperContext : DapperContext
    {
        public DapperDbSet<Person> Person { get; set; }
        public MyDapperContext(DapperContextOptions<MyDapperContext> pDapperContextOptions) : base(pDapperContextOptions)
        {
        }
    }
```

```csharp
Person.cs
    [Table("Persons")]
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
```

```json
appsettings.json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Database=MyDapperDataBase;User Id=User;"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft": "Warning",
          "Microsoft.Hosting.Lifetime": "Information"
        }
      }
    }
```

```json
secrets.json
{
  "DefaultConnectionPassword": "l^#!hODH~19UR4T"
}
```