using Microsoft.Extensions.FileProviders;
using MVCSample.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMahasiswa,MahasiswaDAL>();
builder.Services.AddScoped<IPengguna,PenggunaDAL>();

//menambahkan dan setting onject session
builder.Services.AddSession(options=>{
    options.IdleTimeout = TimeSpan.FromMinutes(2);
});

//menambahkan file provider
builder.Services.AddSingleton<IFileProvider>(
    new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

        

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//Menggunakan session
app.UseSession();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
