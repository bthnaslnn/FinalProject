using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
/*builder.Services.AddSingleton<IProductService, ProductManager>();*/            //AddScoped -- her request için bir instance (genellikle servisler için) ve                                                                         
/*builder.Services.AddSingleton<IProductDal, EfProductDal>();  */                //AddTransient Her kullanýmda yeni bir instance oluþturulur. (stateless iþlemler için
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});

//JWT
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

//JWT


var app = builder.Build();


ServiceTool.Initialize(app.Services);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run(); 
