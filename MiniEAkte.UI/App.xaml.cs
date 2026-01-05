using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using MiniEAkte.Application.Auth.Interfaces;
using MiniEAkte.Application.Auth.Services;
using MiniEAkte.Application.Services.CaseServices;
using MiniEAkte.Application.ViewModels;
using MiniEAkte.Application.ViewModels.Cases;
using MiniEAkte.Application.ViewModels.Users;
using MiniEAkte.Infra.Data;
using MiniEAkte.Infra.Identity;
using MiniEAkte.Infrastructure.Data.Seeds;
using MiniEAkte.UI.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MiniEAkte.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{

    public static IServiceProvider Services { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        ConfigureServices(services);

        Services = services.BuildServiceProvider();

        var loginView = Services.GetRequiredService<LoginView>();
        loginView.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var connectionString =
            "Server=localhost;Port=3306;Database=minieakte;User=root;Password=12345678;";

        services.AddSingleton(_ =>
            DbContextFactory.CreateDbContext(connectionString));

        services.AddSingleton<PasswordHasher>();
        services.AddSingleton<IAuthorizationService, AuthorizationService>();

        services.AddScoped<IUserRegistrationService, UserRegistrationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICaseFileService, CaseService>();
        services.AddSingleton<ICurrentUserContext, CurrentUserContext>();

        services.AddTransient<LoginViewModel>();
        services.AddTransient<SignupViewModel>();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<CaseFileListViewModel>();
        services.AddTransient<CreateCaseFileViewModel>();
        services.AddTransient<CaseFileDetailsViewModel>();

        services.AddTransient<LoginView>();
        services.AddTransient<SignupView>();
        services.AddTransient<MainWindow>();
        services.AddTransient<CreateCaseFileView>();

    }
}

