var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Produto> produtos = new List<Produto>();

// o metodo get pega a / como url, "/" é a raiz 
// é possivel alterar para outra url como "qualquercoisa"
// ou aterar para uma url produro/listar 
app.MapGet("/produto/listar", () => "listagem de produtos");

// nao se pode criar o mesmo metodo e a mesma url se nao ocorre conflito de endpoints
// app.MapGet("/produto/listar", () => "listagem de produtos");

// metodo post -> cadastro 
// app.MapPost("/produto/cadastrar", () => "Cadastro de produtos"); -> erro 405. Toda funcao acessada por uma url é pelo metodo get
app.MapPost("/produto/cadastrar", () => "Cadastro de produtos"); 

// get é para pegar informações
// post é para enviar informações

// exercicio
// tentar cadastar produtos em uma lista


app.Run();

record Produto(string nome, string descricao);