using api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
 {
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
 });
builder.Services.AddControllers();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "hello world");
app.MapControllers();
app.Run();


