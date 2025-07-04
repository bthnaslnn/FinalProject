using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDısposible pattern implementation of c#
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(entity); //Git verikaynağından benim gönderdiğim entity'E eşleştir. Ekleme olduğu için direkt ekliyor.
                addedEntity.State = EntityState.Added; //Veritabanında ekle demek.
                context.SaveChanges(); //Veritabanındaki değişiklikler .
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity); //Git verikaynağından benim gönderdiğim entity'E eşleştir. Ekleme olduğu için direkt ekliyor.
                deletedEntity.State = EntityState.Deleted; //Veritabanında sil demek. Durumu silinicek.
                context.SaveChanges(); //Veritabanındaki değişiklikler .
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null 
                    ? context.Set<Product>().ToList() 
                    : context.Set<Product>().Where(filter).ToList();
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var uptadedEntity = context.Entry(entity); //Git verikaynağından benim gönderdiğim entity'E eşleştir. Ekleme olduğu için direkt ekliyor.
                uptadedEntity.State = EntityState.Modified; //Veritabanında güncelleme demek. Durumu güncellenecek.
                context.SaveChanges(); //Veritabanındaki değişiklikler .
            }
        }
    }
}
