using System.Collections;
using System.Linq.Expressions;

namespace NiuX.Linq.SqlServer;

public class CustomQuery<T> : IQueryable<T>
{
    private readonly BaseProvider _provider;

    public CustomQuery(BaseProvider provider)
    {
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        Expression = Expression.Constant(this);
    }

    public CustomQuery(BaseProvider provider, Expression expression)
    {
        if (expression == null) throw new ArgumentNullException(nameof(expression));

        if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            throw new ArgumentOutOfRangeException(nameof(expression));

        Expression = expression;
        _provider = provider ?? throw new ArgumentNullException(nameof(provider));
    }

    public Type ElementType => typeof(T);
    public Expression Expression { get; }
    public IQueryProvider Provider => _provider;

    public IEnumerator<T> GetEnumerator()
    {
        return Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Provider.Execute<IEnumerable>(Expression).GetEnumerator();
    }

    public override string ToString()
    {
        return _provider.GetQueryText(Expression);
    }
}