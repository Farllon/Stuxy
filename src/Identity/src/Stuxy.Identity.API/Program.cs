var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStuxy(builder.Configuration);

var app = builder.Build();

app.UseCustomSwagger();

app.UseHttpsRedirection();

app.UseCustomLocalizationAndGlobalization();

app.UseAuthorization();

app.MapControllers();

app.Run();
