using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using ProjetoSQLCLR;
using System.Collections;
using MiniDapper;
using System.Collections.Generic;

public partial class UserDefinedFunctions
{
    /// <summary>
    /// Função que retorna todos os feriados via WebService
    /// </summary>
    /// <param name="ano"> ano a ser escolhido </param>
    [SqlFunction(DataAccess = DataAccessKind.Read,
                 SystemDataAccess = SystemDataAccessKind.Read,
                 FillRowMethodName = nameof(PreencherFeriados),
                 TableDefinition = "Data DATETIME, Nome NVARCHAR(MAX), Tipo NVARCHAR(MAX), Descricao NVARCHAR(MAX), CodigoFeriado NVARCHAR(MAX)")]
    public static IEnumerable fnFeriadosAno(SqlInt32 ano)
    {
        var data = Web.Get(string.Format(ConstanteTokenFeriados.Url, ano.Value, ConstanteTokenFeriados.Token));
        return JsonConverter.DeserializeList<Feriado>(data);
    }

    private static void PreencherFeriados(object obj,
                                          out SqlDateTime Data,
                                          out SqlString Nome,
                                          out SqlString Tipo,
                                          out SqlString Descricao,
                                          out SqlString CodigoFeriado)
    {
        Feriado feriado = (Feriado)obj;
        Data = new SqlDateTime(feriado.Date.TryParseDate());
        Nome = new SqlString(feriado.Name);
        Tipo = new SqlString(feriado.Type);
        Descricao = new SqlString(feriado.Description);
        CodigoFeriado = new SqlString(feriado.Type_code);
    }
}
