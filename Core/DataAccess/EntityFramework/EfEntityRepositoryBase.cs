using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()   
    {
        public void Add(TEntity entity)
        {
            //IDısposible pattern implementation of c#
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); //Git verikaynağından benim gönderdiğim entity'E eşleştir. Ekleme olduğu için direkt ekliyor.
                addedEntity.State = EntityState.Added; //Veritabanında ekle demek.
                context.SaveChanges(); //Veritabanındaki değişiklikler .
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity); //Git verikaynağından benim gönderdiğim entity'E eşleştir. Ekleme olduğu için direkt ekliyor.
                deletedEntity.State = EntityState.Deleted; //Veritabanında sil demek. Durumu silinicek.
                context.SaveChanges(); //Veritabanındaki değişiklikler .
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var uptadedEntity = context.Entry(entity); //Git verikaynağından benim gönderdiğim entity'E eşleştir. Ekleme olduğu için direkt ekliyor.
                uptadedEntity.State = EntityState.Modified; //Veritabanında güncelleme demek. Durumu güncellenecek.
                context.SaveChanges(); //Veritabanındaki değişiklikler .
            }
        }
    }
}
