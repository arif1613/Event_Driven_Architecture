using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace OrderService.Data.Test.TestHelpers
{
    public class TestHelpers
    {
        public static Mock<T> MockContext<T>() where T : class
        {
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
        public static Mock<T> MockRepository<T>() where T : class
        {
            return new Mock<T>();
        }
    }
}
