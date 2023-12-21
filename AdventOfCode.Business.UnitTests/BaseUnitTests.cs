using System;
using System.Threading.Tasks;
using AdventOfCode.Business.UnitTests.Logging;
using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace AdventOfCode.Business.UnitTests
{

    public abstract class BaseUnitTest<TSut> : IAsyncLifetime
    {
        private readonly ITestOutputHelper _output;
        private AutoMock _mock;
        private TSut _sut;

        protected BaseUnitTest(ITestOutputHelper output)
        {
            _output = output;
        }

        public TSut Sut
        {
            get
            {
                if (_sut == null)
                {
                    _sut = _mock.Create<TSut>();
                }

                return _sut;
            }
            set => _sut = value;
        }

        public virtual Task InitializeAsync()
        {
            _mock = AutoMock.GetLoose();

            _mock.Provide<ILogger<TSut>>(new XUnitLogger<TSut>(_output));

            return Task.CompletedTask;
        }

        public virtual Task DisposeAsync()
        {
            _mock.MockRepository.VerifyAll();

            _mock.Dispose();

            return Task.CompletedTask;
        }

        protected Mock<T> Mock<T>()
            where T : class
        {
            return _mock.Mock<T>();
        }

        protected T Create<T>()
            where T : class
        {
            return _mock.Create<T>();
        }

        protected T Provide<T>(T instance)
            where T : class
        {
            if (_sut != null)
            {
                throw new Exception("SutAlreadyExists - You already provided the sut instance.");
            }

            return _mock.Provide(instance);
        }
    }
}
