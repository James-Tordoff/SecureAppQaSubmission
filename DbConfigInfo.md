Any Database work can be scaffolded back into the project via the following command (example).

The command below will over write all Tables at the base level so ensure Model Mapping is in place or this could seriously mess with your project -- For testing purpose change the end OutputDir from Models to Test or something and you can see and check prior to publishing it to the correct Folder.

https://learn.microsoft.com/en-us/ef/core/cli/dotnet <- cli commands for scaffolding.

 Scaffold-DbContext "Server=demosqldbserver.database.windows.net,1433;Initial Catalog=JamesQADB;User ID=jamesdemologin;Password=Password1!;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ModelsTEST -NoOnConfiguring

 The - NoOnConfiguring end option stops a DBcontext being added into the produced code so we do not have dangerous code within the project - Not tested if the -noconfiguring actually works but found new work around if the dbcontext is not set then the following will work inside the context file. This will read conn string from appsettings.json.

 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Get Server Connection String from appsettings.json configuration file.
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        Scaffold-DbContext "Server=demosqldbserver.database.windows.net,1433;Database=JamesQADB;User ID=jamesdemologin;Password=Password1!;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir ModelsTest -NoOnConfiguring 