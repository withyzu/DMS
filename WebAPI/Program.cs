using FileAssistant.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Read UserFileOptions from Settings.json
var userFileOptions = builder.Configuration.GetSection(nameof(UserFileOptions)).Get<UserFileOptions>() ?? new();

// Configure UserFileOptions
builder.Services.AddUserFile(options =>
{
    options.StoragePath = Path.Combine(@"C:\Users\withyzu\Desktop\FileAssistant\FileAssistant", userFileOptions.StoragePath);
    Console.WriteLine(userFileOptions);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddLogging(options =>
// {
//     options.AddConsole();
//     options.AddDebug();
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();

app.Run();
