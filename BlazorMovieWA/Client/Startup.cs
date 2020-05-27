using Blazor.FileReader;
using BlazorMovieWA.Client.Helpers;
using BlazorMovieWA.Client.Repository;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorMovieWA.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRepository, RepositoryInMemory>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IDisplayMessage, DisplayMessage>();
            services.AddScoped<IUsersRepository, UserRepository>();
            services.AddFileReaderService(options => options.InitializeOnFirstCall = true);

            services.AddApiAuthorization();

            //services.AddScoped<TokenRenewer>();
        }

        //public void Configure(IComponentsApplicationBuilder app)
        //{
        //    app.AddComponent<App>("app");
        //}
    }
}
