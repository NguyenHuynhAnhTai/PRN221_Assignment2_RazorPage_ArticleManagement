using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Implementations;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IServiceCollection services = new ServiceCollection();

services.AddSingleton(typeof(INewsArticleService), typeof(NewsArticleService));
services.AddSingleton(typeof(ICategoryService), typeof(CategoryService));
services.AddSingleton(typeof(ISystemAccountService), typeof(SystemAccountService));
services.AddSingleton(typeof(ITagService), typeof(TagService));

services.AddSingleton(typeof(ITagRepository), typeof(TagRepository));
services.AddSingleton(typeof(ICategoryRepository), typeof(CategoryRepository));
services.AddSingleton(typeof(ISystemAccountRepository), typeof(SystemAccountRepository));
services.AddSingleton(typeof(INewsArticleRepository), typeof(NewsArticleRepository));

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
