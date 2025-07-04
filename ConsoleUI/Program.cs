using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;


//SOLID 
//Open Closed Principle
internal class Program
{
    private static void Main(string[] args)
    {
        ProductManager productManager = new ProductManager(new EfProductDal());

        foreach (var product in productManager.GetByUnitPrice(40,100))
        {
            Console.WriteLine(product.ProductName);
        }

        Console.WriteLine("Hello, World!");
    }
}