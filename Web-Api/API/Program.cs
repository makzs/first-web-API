
using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Produto produto = new Produto();
// produto.Nome = "Bolacha";

List<Produto> produtos =
[
    new Produto("Arroz", "pacote de arroz 2kg", 20.50),
    new Produto("feijao", "pacote de feijao 1kg", 13.20),
    new Produto("Batata", "saco de 1kg de batata", 15.00),
    new Produto("Frango", "pacote de 500g de frango", 18.85),
];

// novo endpoint para a raiz da aplicação
// GET: http://localhost:5056
app.MapGet("/", () => "API de produtos");

// o metodo get pega a / como url, "/" é a raiz 
// é possivel alterar para outra url como "qualquercoisa"
// ou aterar para uma url produto/listar 
// GET: http://localhost:5056/produto/listar
app.MapGet("/produto/listar", () =>
    produtos);

// colocar a variavel entre {} para definir que pode ser diferente
// entre as () se deve colocar os parametros no caso a variavel definida 
// entre [] se escreve a origem do parametro informado
// GET: http://localhost:5056/produto/buscar/nomeDoProduto
app.MapGet("/produto/buscar/{nome}", ([FromRoute] string nome) =>
    {
        for (int i = 0; i < produtos.Count; i++)
        {
            if (produtos[i].Nome == nome)
            {
                // retornar o produto encontrado 
                return Results.Ok(produtos[i]);
            }

        }
        // caso nao encontre o produto
        return Results.NotFound("Produto nao encontrado");
    }
);


// nao se pode criar o mesmo metodo e a mesma url se nao ocorre conflito de endpoints
// app.MapGet("/produto/listar", () => "listagem de produtos");

// metodo post -> cadastro 
// app.MapPost("/produto/cadastrar", () => "Cadastro de produtos"); -> erro 405. Toda funcao acessada por uma url é pelo metodo get
// GET: http://localhost:5056/produto/cadastrar
//app.MapPost("/produto/cadastrar", () => "Cadastro de produtos");

// get é para pegar informações
// post é para enviar informações

// exercicio
// cadastar produtos em uma lista

// pela url
app.MapGet("produto/cadastrar/{nome}/{descricao}/{valor}", ([FromRoute] string nome, [FromRoute] string descricao, [FromRoute] double valor) =>
    {

        // preencher o objeto pelo construtor
        Produto novoProduto = new Produto(nome, descricao, valor);

        // verifica se valores strings nao são nulos
        if (novoProduto.Nome is null || novoProduto.Descricao is null)
        {
            return Results.BadRequest("campos invalidos.");
        }

        // adiciona o objeto novoProduto na lista produtos
        produtos.Add(new Produto(novoProduto.Nome, novoProduto.Descricao, novoProduto.Valor));

        // retorna o codigo hhtp que o metodo realizou as ações com sucesso
        return Results.Created("Produto adicionado com sucesso! ", novoProduto);
    });

// pelo corpo json
app.MapPost("/produto/cadastrar", ([FromBody] Produto novoProduto) =>
    {
        if (novoProduto.Nome is null || novoProduto.Descricao is null)
        {
            return Results.BadRequest("campos invalidos.");
        }

        produtos.Add(new Produto(novoProduto.Nome, novoProduto.Descricao, novoProduto.Valor));

        return Results.Created("Produto adicionado com sucesso! ", novoProduto);
    });

// alterar produto da lista
app.MapPut("/produto/atualizar/{Nome}", ([FromRoute] string nome, [FromBody] Produto produtoAtualizado) =>
{

    /*
        for (int i = 0; i < produtos.Count; i++)
        {
            if (produtos[i].Nome == nome)
            {
                Produto? produtoExistente = produtos[i];
            }

        }
    */

    Produto? produtoExistente = produtos.FirstOrDefault(p => p.Nome == nome);

    if (produtoExistente is null)
    {
        return Results.NotFound("Nome requisitado nao encontrado na lista de produtos");
    }

    produtoExistente.Nome = produtoAtualizado.Nome;
    produtoExistente.Descricao = produtoAtualizado.Descricao;
    produtoExistente.Valor = produtoAtualizado.Valor;

    return Results.Ok($"Produto {produtoExistente.Nome} alterado com sucesso!");
});

// deletar produto da lista
app.MapDelete("/produto/deletar/{Nome}", (string nome) =>
{

    Produto? produtoExistente = produtos.FirstOrDefault(p => p.Nome == nome);

    if (produtoExistente is null)
    {
        return Results.BadRequest("campos invalidos.");
    }

    produtos.Remove(produtoExistente);

    return Results.Ok($"Produto {produtoExistente.Nome} deletado com sucesso!");
});

// alterar parcialmente um produto
app.MapPatch("/produto/patch/{Nome}/{patch}", ( [FromRoute] string nome, [FromRoute] string patch, [FromBody] Produto produtoAtualizado) =>
{

    Produto? produtoExistente = produtos.FirstOrDefault(p => p.Nome == nome);

    if (produtoExistente is null)
    {
        return Results.BadRequest("campos invalidos.");
    }

    switch (patch)
    {
        case "nome":
            produtoExistente.Nome = produtoAtualizado.Nome;
            return Results.Ok($"Nome do produto atualizado para: {produtoExistente.Nome}");
        case "descricao":
            produtoExistente.Descricao = produtoAtualizado.Descricao;
            return Results.Ok($"Descrição do produto atualizada para: {produtoExistente.Descricao}");
        case "valor":
            produtoExistente.Valor = produtoAtualizado.Valor;
            return Results.Ok($"Valor do produto atualizado para: {produtoExistente.Valor}");
        default:
            return Results.BadRequest("Campo 'patch' inválido. Escolha entre 'nome', 'descricao' ou 'valor'.");
    }
});



app.Run();

