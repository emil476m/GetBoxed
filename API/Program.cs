using Infarstructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*builder.Services.AddNpgsqlDataSource(Utilities.ProperlyFormattedConnectionString,
    dataSourceBuilder => dataSourceBuilder.EnableParameterLogging()); */
builder.Services.AddSingleton<Repository>();
builder.Services.AddSingleton<Service.Service>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors(options =>

{

    options.SetIsOriginAllowed(origin => true)

        .AllowAnyMethod()

        .AllowAnyHeader()

        .AllowCredentials();

});

//app.UseAuthorization();

app.MapControllers();

app.Run();