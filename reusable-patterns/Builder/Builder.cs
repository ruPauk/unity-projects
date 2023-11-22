using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core
{
    public interface IBuilder<T>
    {
        T Build();
    }


    public class Builder : IBuilder<TestBuilder>, IDisposable
    {
        private TestBuilder _testBuilder;
        public Builder()
        {
            _testBuilder = new TestBuilder();
        }

        public void SetUpA(int a)
        {
            _testBuilder.a = a; 
        }

        public void SetUpB(float b)
        {
            _testBuilder.b = b;
        }

        public void SetUpC(string c)
        {
            _testBuilder.c = c;
        }

        //Liquid Builder
        public Builder TestA(int a)
        {
            _testBuilder.a = a;
            return this;
        }
        public Builder TestB(int a)
        {
            _testBuilder.a = a;
            return this;
        }
        public Builder TestC(int a)
        {
            _testBuilder.a = a;
            return this;
        }

        public TestBuilder Build()
        {
            return _testBuilder;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class TestBuilder
    {
        public int a;
        public float b;
        public string c;
    }

    public class Client
    {
        TestBuilder _testBuilder;

        public void Do()
        {
            
            using (var builder = new Builder()) 
            {
                builder.SetUpA(1);
                //_testBuilder = builder.Build();
                _testBuilder =  builder.TestA(2).TestB(3).Build();
            }
        }
    }

}
