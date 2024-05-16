
using LoclizationWebApi.Sevices;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace LoclizationWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddLocalization();
            builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocilizerFactory>();
            builder.Services.AddMvc().AddDataAnnotationsLocalization(option =>
            {
                option.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(JsonStringLocilizerFactory));
            });
            //builder.Services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    var suportedculter = new[]
            //    {
            //        new CultureInfo("en-US"),
            //        new CultureInfo("ar-EG"),
            //        new CultureInfo("de-DE"),

            //    };
            //    options.DefaultRequestCulture = new RequestCulture(culture: suportedculter[0]);
            //    options.SupportedCultures =suportedculter;
            //});


            builder.Services.Configure<Localizationsettengs>(builder.Configuration.GetSection("Localization"));
            // Add localization services
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
            var app = builder.Build();
            // Retrieve the localization settings from the DI container
            var localizationSettings = app.Services.GetRequiredService<IOptions<Localizationsettengs>>().Value;
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            //var suportedcultuer = new[] { "en-US", "ar-EG", "de-DE" };
            //var localizationoption = new RequestLocalizationOptions().
            //    SetDefaultCulture(suportedcultuer[0]).
            //    AddSupportedCultures(suportedcultuer);
            //app.UseRequestLocalization(localizationoption);
            var supportedCultures = localizationSettings.SupportedCultures.Select(c => new CultureInfo(c)).ToList();
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(localizationSettings.DefaultCulture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(localizationOptions);

            app.MapControllers();

            app.Run();
        }
    }
}
