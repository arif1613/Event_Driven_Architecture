using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace OrderService.Api.Test.TestHelpers
{
    public class ServiceTestHelpers
    {
        public static Mock<T> MockService<T>() where T : class
        {
            return new Mock<T>();
        }
    }
}
