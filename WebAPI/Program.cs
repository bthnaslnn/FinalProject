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

// Autfac yapýlandýrmasý
// Add services to the container.
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // Autofac container'a servisleri buradan da ekleyebilirsin fakat biz WEBApý'ya baðlý kalmamak için Business katmaný içerisinde yazdýk.

    containerBuilder.RegisterModule(new AutofacBusinessModule()); //.net core yerine baþka bir IoC container kullanýyorum.
});
// -----Autofac--------------------------------------// -----Autofac------------------------------// -----Autofac-------------// -----Autofac---------


//Autofac, Ninject , CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container saðlayan yapýlar.
//AOP --> Autofac bu yapý ile çalýþabiliyor.
//PostSharp << ücretli Çek cumhuriyti ülkesinden bir yazýlým.

builder.Services.AddControllers();

/*builder.Services.AddSingleton<IProductService, ProductManager>();*/            //AddScoped -- her request için bir instance (genellikle servisler için) ve                                                                         
/*builder.Services.AddSingleton<IProductDal, EfProductDal>();  */                //AddTransient Her kullanýmda yeni bir instance oluþturulur. (stateless iþlemler için
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
