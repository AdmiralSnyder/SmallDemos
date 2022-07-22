using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleTestApp.GenericWeirdness;

namespace ConsoleTestApp
{
    public class DbObj { }

    public class Thing : DbObj { }

    public class BaseApps<TDbObj> where TDbObj : DbObj, new() { }

    public class OtherApps : BaseApps<Thing> { }

    public static class GenericWeirdness
    {
        public static void Method<TApps, TDbObj>(TApps apps)
            where TApps : BaseApps<TDbObj>
            where TDbObj : DbObj, new()
        { }

        public static void Invoke(OtherApps apps)
        {
            Method<OtherApps, Thing>(apps); // <--- this works
            
            // Error CS0411  The type arguments for method 'GenericWeirdness.Method<TApps, TDbObj>(TApps)'
            //   cannot be inferred from the usage.Try specifying the type arguments explicitly.
            // Method(apps);
        }
    }

    public static class MoreSpecificBiggerCase
    {
        public interface IDbObjApps<TDbObj> where TDbObj : DbObj, new() { }
        public class RightGroup : DbObj, IName { }

        public class RightGroupApps : IDbObjApps<RightGroup>, IDbObjCreateApps<RightGroup, NameCreateArgs<RightGroup>>
        { }


        public interface IDbObjCreateApps<TDbObj, TDbObjCreateArgs> : IDbObjApps<TDbObj>
        where TDbObj : DbObj, new()
        where TDbObjCreateArgs : IDbObjCreateArgs<TDbObj>
        { }

        public interface INameCreateArgs<TDbObj> : IDbObjCreateArgs<TDbObj>, IName
        where TDbObj : DbObj, new()
        { }
        public interface IDbObjCreateArgs<TDbObj>
        where TDbObj : DbObj, new()
        { }
        public interface IName { }

        public static TDbObj CreateDbObjWithName<TDbObj, TNameCreateArgs>(IDbObjCreateApps<TDbObj, TNameCreateArgs> apps, TNameCreateArgs createArgs)
        //where TApps : IDbObjCreateApps<TDbObj, TNameCreateArgs>
        where TDbObj : DbObj, IName, new()
        where TNameCreateArgs : INameCreateArgs<TDbObj>
        {
            return null;
        }

        public static TDbObj CreateDbObjWithNameDOESNTWORK<TApps, TDbObj, TNameCreateArgs>(TApps apps, TNameCreateArgs createArgs)
        where TApps : IDbObjCreateApps<TDbObj, TNameCreateArgs>
        where TDbObj : DbObj, IName, new()
        where TNameCreateArgs : INameCreateArgs<TDbObj>
        {
            return null;
        }

        public class NameCreateArgs<TDbObj> : INameCreateArgs<TDbObj>
            where TDbObj : DbObj, new()
        { }

        public static void Invoke(RightGroupApps apps)
        {
            // it works if we add generic types at calling site
            CreateDbObjWithNameDOESNTWORK<RightGroupApps, RightGroup, NameCreateArgs<RightGroup>>(apps, new());
            // CreateDbObjWithNameDOESNTWORK(apps, new());
            // Error CS0411  The type arguments for method 'GenericWeirdness.CreateDbObjWithNameDOESNTWORK<TApps, TDbObj, TNameCreateArgs>(TApps, TNameCreateArgs)' cannot be inferred from the usage.Try specifying the type arguments explicitly.

            // this is our current workaround
            CreateDbObjWithName(apps, new());
        }
    }

}
