using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// -----Autofac--------------------------------------// -----Autofac------------------------------// -----Autofac-------------// -----Autofac---------

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Autfac yap�land�rmas�
// Add services to the container.
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Autofac container'a servisleri buradan da ekleyebilirsin fakat biz WEBAp�'ya ba�l� kalmamak i�in Business katman� i�erisinde yazd�k.

    containerBuilder.RegisterModule(new AutofacBusinessModule()); //.net core yerine ba�ka bir IoC container kullan�yorum.
});
// -----Autofac--------------------------------------// -----Autofac------------------------------// -----Autofac-------------// -----Autofac---------


//Autofac, Ninject , CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container sa�layan yap�lar.
//AOP --> Autofac bu yap� ile �al��abiliyor.
//PostSharp << �cretli �ek cumhuriyti �lkesinden bir yaz�l�m.

builder.Services.AddControllers();

/*builder.Services.AddSingleton<IProductService, ProductManager>();*/            //AddScoped -- her request i�in bir instance (genellikle servisler i�in) ve                                                                         
/*builder.Services.AddSingleton<IProductDal, EfProductDal>();  */                //AddTransient Her kullan�mda yeni bir instance olu�turulur. (stateless i�lemler i�in
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
