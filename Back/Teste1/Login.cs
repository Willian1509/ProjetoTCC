using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

public class Function
{
    private static readonly string TableName = "usuarios";
    private static readonly IAmazonDynamoDB dynamoDbClient = new AmazonDynamoDBClient();

    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
    {
        var loginData = System.Text.Json.JsonSerializer.Deserialize<LoginData>(input.Body);
        var email = loginData.Email;
        var senha = loginData.Senha;

        var table = Table.LoadTable(dynamoDbClient, TableName);
        var userItem = await table.GetItemAsync(email);

        if (userItem == null)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 404,
                Body = "Usuário não cadastrado.",
                Headers = new Dictionary<string, string>
                {
                    { "Access-Control-Allow-Origin", "*" }, // CORS header
                    { "Access-Control-Allow-Headers", "Content-Type" }
                }
            };
        }

        var senhaDb = userItem["senha"].AsString();
        if (senhaDb == senha)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 200,
                Body = "Login bem-sucedido!",
                Headers = new Dictionary<string, string>
                {
                    { "Access-Control-Allow-Origin", "*" }, // CORS header
                    { "Access-Control-Allow-Headers", "Content-Type" }
                }
            };
        }
        else
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = 401,
                Body = "Senha incorreta.",
                Headers = new Dictionary<string, string>
                {
                    { "Access-Control-Allow-Origin", "*" }, // CORS header
                    { "Access-Control-Allow-Headers", "Content-Type" }
                }
            };
        }
    }
}

public class LoginData
{
    public string Email { get; set; }
    public string Senha { get; set; }
}
