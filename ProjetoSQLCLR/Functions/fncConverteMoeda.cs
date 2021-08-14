using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Net;
using Microsoft.SqlServer.Server;
using ProjetoSQLCLR;

public partial class UserDefinedFunctions
{
    [SqlFunction(DataAccess = DataAccessKind.Read,
                 SystemDataAccess = SystemDataAccessKind.Read)]
    public static SqlDouble fncConverteMoeda(SqlString moedaOrigem,
                                             SqlString moedaDestino,
                                             SqlString token)
    {
            var url = "https://free.currconv.com/api/v7/convert?q=" + moedaOrigem.Value + "," + moedaDestino.Value + "&compact=ultra&apiKey=" + token.Value;
            var resposta =Web.Get(url);
            return SqlDouble.Parse(resposta.Substring(resposta.IndexOf(":") + 1).Replace("}", ""));  
    }
}
