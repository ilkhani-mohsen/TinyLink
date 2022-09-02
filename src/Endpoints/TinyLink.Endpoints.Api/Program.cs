using Microsoft.EntityFrameworkCore;
using Quartz;
using TinyLink.Endpoints.Api.Jobs;
using TinyLink.Persistence.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TinyLinkDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDependencies(options =>
{
    options.AssembliesToLoad = new string[] { "TinyLink" };
});

builder.Services.AddQuartz(q => 
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    q.AddJob<LinkVisitStatisticsSyncJob>(c => c.WithIdentity(nameof(LinkVisitStatisticsSyncJob)));
    q.AddTrigger(c => c.ForJob(nameof(LinkVisitStatisticsSyncJob))
        .WithIdentity($"{nameof(LinkVisitStatisticsSyncJob)}.trigger")
        .WithCronSchedule(builder.Configuration["LinkVisitStatisticsSyncJobCronExpression"]));
});

builder.Services.AddQuartzServer(options =>
{
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
