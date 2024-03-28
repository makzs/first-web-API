
using API.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Produto produto = new Produto();
// produto.Nome = "Bolacha";

List<Produto> produtos = new List<Produto>();
produtos.Add(new Produto("Arroz", "pacote de arroz 2kg", 20.50));
produtos.Add(new Produto("feijao", "pacote de feijao 1kg", 13.20));
produtos.Add(new Produto("Batata", "saco de 1kg de batata", 15.00));
produtos.Add(new Produto("Frango", "pacote de 500g de frango", 18.85));

// novo endpoint para a raiz da aplicação
// GET: http://localhost:5056
app.MapGet("/", () => "API de produtos");

// o metodo get pega a / como url, "/" é a raiz 
// é possivel alterar para outra url como "qualquercoisa"
// ou aterar para uma url produto/listar 
// GET: http://localhost:5056/produto/listar
app.MapGet("/produto/listar", () =>
    produtos);

// nao se pode criar o mesmo metodo e a mesma url se nao ocorre conflito de endpoints
// app.MapGet("/produto/listar", () => "listagem de produtos");

// metodo post -> cadastro 
// app.MapPost("/produto/cadastrar", () => "Cadastro de produtos"); -> erro 405. Toda funcao acessada por uma url é pelo metodo get
// GET: http://localhost:5056/produto/cadastrar
app.MapPost("/produto/cadastrar", () => "Cadastro de produtos");

// get é para pegar informações
// post é para enviar informações

// exercicio
// tentar cadastar produtos em uma lista
app.MapGet("produto/cadastrar/{id}", (int id) => $"Obter item com ID {id}");

app.Run();

