using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Autofac, Ninject , CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container sa�layan yap�lar.
//AOP --> Autofac bu yap� ile �al��abiliyor.

builder.Services.AddControllers();

builder.Services.AddSingleton<IProductService, ProductManager>();            //AddScoped -- her request i�in bir instance (genellikle servisler i�in) ve                                                                         
builder.Services.AddSingleton<IProductDal, EfProductDal>();                  //AddTransient Her kullan�mda yeni bir instance olu�turulur. (stateless i�lemler i�in
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
