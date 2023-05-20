using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace NiuX.Linq.SqlServer;

public class SqlServerProvider : BaseProvider
{
    private readonly SqlConnection _connection;

    public SqlServerProvider(SqlConnection connection)
    {
        _connection = connection;
    }

    public override string GetQueryText(Expression expression)
    {
        return Translate(expression);
    }

    public override object Execute(Expression expression)
    {
        var command = new SqlCommand { Connection = _connection, CommandText = Translate(expression) };
        Console.WriteLine(command.CommandText);
        // DbDataReader reader = command.ExecuteReader();
        // ...
        return null;
    }

    private string Translate(Expression expression)
    {
        return new CustomTranslator().Translate(expression);
    }
}