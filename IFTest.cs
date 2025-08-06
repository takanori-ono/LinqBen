using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqBen
{
    public interface IMyIF
    {
        int MyProp { get; }
    }
    class MyClass : IMyIF
    {
        private int _MyProp;
        public MyClass()
        {
            _MyProp = 10;
        }
        public int MyProp { get => _MyProp; }
    }

    public class TestClass
    {
        public static void test_func()
        {
            var m = new MyClass();
            var v = m.MyProp;
        }
    }
}
