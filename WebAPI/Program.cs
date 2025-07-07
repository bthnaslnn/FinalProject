using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Autofac, Ninject , CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container saðlayan yapýlar.
//AOP --> Autofac bu yapý ile çalýþabiliyor.

builder.Services.AddControllers();

builder.Services.AddSingleton<IProductService, ProductManager>();            //AddScoped -- her request için bir instance (genellikle servisler için) ve                                                                         
builder.Services.AddSingleton<IProductDal, EfProductDal>();                  //AddTransient Her kullanýmda yeni bir instance oluþturulur. (stateless iþlemler için
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
