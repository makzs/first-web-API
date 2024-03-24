var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Produto> produtos = new List<Produto>();
var produto1 = new Produto("Arroz", "pacote de arroz 2kg");
var produto2 = new Produto("feijao", "pacote de feijao 1kg");

produtos.Add(produto1);
produtos.Add(produto2);

// o metodo get pega a / como url, "/" é a raiz 
// é possivel alterar para outra url como "qualquercoisa"
// ou aterar para uma url produto/listar 
app.MapGet("/produto/listar", () => {
    return produtos;
});

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