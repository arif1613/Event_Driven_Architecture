using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrderService.Data.Context;
using OrderService.Data.Models;
using OrderService.Data.Repo;

namespace OrderService.Api.Test.ServiceTests
{
    public class ServiceTestsHelper
    {
        public static Mock<T> MockRepo<T>() where T : class
        {
            return new Mock<T>();
        }

        public static Mock<T> MockUnitOfWork<T>() where T : class
        {
            //var genericRepo=new Mock<IRepository<T>>();
            //var context=new Mock<IOrderContext>();
            //context.Setup(r=>r.GetDbSet<T>()).Returns(CreateMockDbSet())
            return new Mock<T>();
        }


        public static Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> queryableEntity) where T : class
        {
            var dbset = new Mock<DbSet<T>>();
            dbset.As<IQueryable<T>>().Setup(p => p.Provider).Returns(queryableEntity.Provider);
            dbset.As<IQueryable<T>>().Setup(p => p.Expression).Returns(queryableEntity.Expression);
            dbset.As<IQueryable<T>>().Setup(p => p.ElementType).Returns(queryableEntity.ElementType);
            dbset.As<IQueryable<T>>().Setup(p => p.GetEnumerator()).Returns(queryableEntity.GetEnumerator());
            dbset.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => queryableEntity.ToList().Add(s));
            dbset.Setup(d => d.AddRange(It.IsAny<List<T>>())).Callback<IEnumerable<T>>((s) => queryableEntity.ToList().AddRange(s));
            return dbset;
        }
    }
}
