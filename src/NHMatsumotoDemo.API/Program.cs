using NHMatsumotoDemo.Infrastructure.CrossCutting.IoC;


var builder = WebApplication.CreateBuilder(args);


ConfigurationManager configuration = builder.Configuration; 
IWebHostEnvironment environment = builder.Environment;

////Custom IoC
BootStrapper.RegisterServices(builder.Services, configuration);

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
