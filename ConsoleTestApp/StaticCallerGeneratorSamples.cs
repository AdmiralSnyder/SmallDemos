using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestApp
{




    interface ICaller
    {
        void Call();
    }

    [AttributeUsage(AttributeTargets.Class)]
    class ContractAttribute : Attribute
    {
        public ContractAttribute(Type interfaceType) { }
    }


    [Contract(typeof(ICaller))]
    static partial class Caller
    {
        public static void Call() { }
    }
    [Contract(typeof(ICaller))]
    static class OtherCaller
    {
        public static void Call() { }
    }


    // generated

    static partial class Caller
    {
        public class Wrapper : ICaller
        {
            static Wrapper Instance { get; }
            // plus Instance creation;

            public void Call() => Caller.Call();
        }
    }

    // consume
    class Consumer<TCaller> where TCaller : ICaller, new()
    {
        TCaller wrapper = new();
        public void MakeCall()
        {
            wrapper.Call();
        }
    }

    class Consumer2
    {
        void DoSomething()
        {
            new Consumer<Caller.Wrapper>().MakeCall();
        }
    }
}
