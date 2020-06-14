using System;
using System.Linq.Expressions;
using LiteDB;

namespace Core
{
    public static class LiteDatabaseExtensions
    {
        public static ILiteQueryable<T> Where<T>(this ILiteQueryable<T> self, bool execute, Expression<Func<T, bool>> query)
        {
            if (execute)
                return self.Where(query);
            return self;
        }
    }
}
