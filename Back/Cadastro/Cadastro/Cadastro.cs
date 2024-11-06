using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using Document = Amazon.DynamoDBv2.DocumentModel.Document;

// Configuração da Lambda para execução do método principal
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CadastroLambda
{
    public class Cadastro
    {
        private static readonly AmazonDynamoDBClient dynamoDbClient = new AmazonDynamoDBClient();
        private static readonly string TableName = "usuarios";

        public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var response = new APIGatewayProxyResponse
            {
                Headers = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };

            try
            {
                // Desserializar os dados do corpo da requisição
                var user = JsonConvert.DeserializeObject<Usuario>(request.Body);

                // Verifica se o e-mail já está cadastrado
                var emailExists = await CheckIfEmailExists(user.Email);

                if (emailExists)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Body = "Usuário já cadastrado.";
                }
                else
                {
                    // Cadastrar o usuário
                    await CreateUser(user);
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Body = "Usuário cadastrado com sucesso.";
                }
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Erro: {ex.Message}");
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response.Body = "Erro ao cadastrar usuário.";
            }

            return response;
        }

        private async Task<bool> CheckIfEmailExists(string email)
        {
            Table table = Table.LoadTable(dynamoDbClient, TableName);

            // Criar uma consulta para verificar se o e-mail já existe
            var search = table.Query(new QueryOperationConfig
            {
                KeyExpression = new Amazon.DynamoDBv2.DocumentModel.Expression
                {
                    ExpressionStatement = "Email = :v_email",
                    ExpressionAttributeValues = { { ":v_email", email } }
                }
            });

            var documentList = await search.GetRemainingAsync();
            return documentList.Count > 0;
        }

        private async Task CreateUser(Usuario user)
        {
            Table table = Table.LoadTable(dynamoDbClient, TableName);

            var newUser = new Document
            {
                ["Email"] = user.Email,
                ["Nome"] = user.Nome,
                ["Senha"] = user.Senha,
                ["Telefone"] = user.Telefone,
                ["TipoUsuario"] = user.TipoUsuario
            };

            await table.PutItemAsync(newUser);
        }
    }

    public class Usuario
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public string TipoUsuario { get; set; }
    }
}
