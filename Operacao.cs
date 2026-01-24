public class Operacao
{
    public string Tipo { get; }
    public decimal Valor { get; }
    public DateTime DataHora { get; private set; }

    public Operacao (string tipo, decimal valor)
    {
        Tipo = tipo;
        Valor = valor;
        DataHora = DateTime.Now;
    }
}