using FluentValidation;
using MediatR;
using Stuxy.Bus.Communication;
using Stuxy.Bus.Messaging;
using Stuxy.Bus.Notifier;
using Stuxy.Bus.Notifier.Notifications;
using Stuxy.Bus.Pipelines.BeforeExecution;
using Teste;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddScoped<IServiceBus, ServiceBus>();
builder.Services.AddTransient(typeof(IBeforeExecutionCommandPipelineStep<,>), typeof(TestePipelineStep<,>));
builder.Services.AddTransient(typeof(IBeforeExecutionCommandPipelineStep<>), typeof(TestePipelineStep<>));
builder.Services.AddBusNotifications();
builder.Services.AddBusValidations(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", async (IServiceBus bus) =>
{
    await bus.SendAsync(new TesteCommand());

    var errors = bus.GetErrors();
    var wanings = bus.GetWarnings();

    bus.Notify(new BusWarning("Teste warning"));

    if (errors.Any() || wanings.Any())
        return errors.Select(e => e.Message).Union(wanings.Select(w => w.Message));

    return new List<string>();
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

class TesteCommand : Command
{
    public string Name { get; set; }
}

class TesteCommandHandler : IRequestHandler<TesteCommand>
{
    public Task<Unit> Handle(TesteCommand request, CancellationToken cancellationToken)
    {
        System.Diagnostics.Debug.WriteLine("Entrou");

        return Task.FromResult(Unit.Value);
    }
}

class CommandValidator : AbstractValidator<TesteCommand>
{
    public CommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();
    }
}
