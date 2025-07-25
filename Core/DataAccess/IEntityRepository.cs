﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //generic constraint << generic kısıt
    //class : referans tip olabilir demek.
    //IEntity : IEntity olabilir veya IEntity implemente eden bir nesne olabilir.
    //new() : new'lenebilir olması gerekir.
    public interface IEntityRepository<T> where T : class, IEntity ,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity); 

        //List<T> GetAllByCategory(int categoryId); //Expression sayesinde bu koda ihtiyacıımız kalmıyor.
    }
}
