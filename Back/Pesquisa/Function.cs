using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson.IO;

// Definições de classes para deserialização
public class Category
{
    public string Name { get; set; }
    public List<string> Tags { get; set; } = new();
}

public class Servico
{
    public string Name { get; set; }
    public List<Category> Category { get; set; } = new();
}

public class Provedor
{
    public string Email { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Nome { get; set; }
    public List<Servico> Servicos { get; set; } = new();
    public string Telefone { get; set; }
    public string TipoUsuario { get; set; }
}

// Classe de entrada da Lambda
public class Request
{
    public string Servico { get; set; }
}

public class Response
{
    public List<Provedor> Provedores { get; set; }
}

// Função Lambda
public class Function
{
    private readonly IAmazonDynamoDB _dynamoDb;

    public Function()
    {
        _dynamoDb = new AmazonDynamoDBClient();
    }

    public async Task<Response> FunctionHandler(Request request, ILambdaContext context)
    {
        var table = Table.LoadTable(_dynamoDb, "Usuarios"); // Nome da tabela
        var queryResults = await table.Scan(new ScanOperationConfig
        {
            Filter = new ScanFilter().AddCondition("servico", ScanOperator.Contains, request.Servico)
        }).GetRemainingAsync();

        var provedores = new List<Provedor>();

        foreach (var document in queryResults)
        {
            var provedor = new Provedor
            {
                Email = document["email"],
                Cidade = document["cidade"],
                Estado = document["estado"],
                Nome = document["nome"],
                Telefone = document["telefone"],
                TipoUsuario = document["tipoUsuario"],
                Servicos = document["servicos"] != null ? JsonConvert.DeserializeObject<List<Servico>>(document["servicos"].ToString())

            };
            provedores.Add(provedor);
        }

        return new Response { Provedores = provedores };
    }
}
