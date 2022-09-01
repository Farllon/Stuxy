namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API v1");
                options.RoutePrefix = string.Empty;
            });

            return app;
        }

        public static IApplicationBuilder UseCustomLocalizationAndGlobalization(this IApplicationBuilder app)
        {
            var supportedCultures = new[] { "en-US", "pt-BR" };

            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}
