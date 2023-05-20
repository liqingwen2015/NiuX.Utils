using System.Linq.Expressions;

namespace NiuX.Linq.SqlServer;

public abstract class BaseProvider : IQueryProvider
{
    IQueryable<T> IQueryProvider.CreateQuery<T>(Expression expression)
    {
        return new CustomQuery<T>(this, expression);
    }

    IQueryable IQueryProvider.CreateQuery(Expression expression)
    {
        return (IQueryable)Activator.CreateInstance(typeof(CustomQuery<>).MakeGenericType(expression.Type),
            this, expression);
    }

    T IQueryProvider.Execute<T>(Expression expression)
    {
        return (T)Execute(expression);
    }

    object IQueryProvider.Execute(Expression expression)
    {
        return Execute(expression);
    }

    public abstract string GetQueryText(Expression expression);

    public abstract object Execute(Expression expression);
}