namespace API.Models;

public class Produto
{

    // construtor

    public Produto()
    {
        Id = Guid.NewGuid().ToString();
        CriadoEm = DateTime.Now;
    }

    public Produto(string nome, string descricao, double valor)
    {
        Id = Guid.NewGuid().ToString();
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
        CriadoEm = DateTime.Now;
    }

    // atributos ou propriedades -> caracteristicas de um objeto
    // get set em C# (transforma de publico pra privado automaticamente)
    // o ? é para tratamentos de excecoes, o valor padrao de uma string é nulo e isso pode dar problema

    public string Id { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public double Valor { get; set; }
    public DateTime CriadoEm { get; set; }


    // get set em java

    // private string nome;
    // private string descricao;

    //  guardar informação
    // public void setNome(string nome)
    // {

    //     this.nome = nome;
    // }

    //  retorna a informação
    // public string getNome()
    // {
    //     return this.nome;
    // }
}