public class Conta
{
    List<Operacao> operacoes = new List<Operacao>();

    public int IdConta { get; private set; }
    public string NomeTitular { get; private set; }
    public decimal Saldo { get; private set; }
    private bool Status;

    public Conta(int id, string nome)
    {
        IdConta = id;
        NomeTitular = nome;
    }

    public void Depositar()
    {
        Console.Clear();
        Console.WriteLine("DEPÓSITO");

        Console.Write("\nDigite o valor a ser depositado: ");
        decimal valorDeposito = decimal.Parse(Console.ReadLine()!);

        if (valorDeposito <= 0)
        {
            Console.WriteLine("Digite um valor acima de zero");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
        else
        {
            Saldo += valorDeposito;
            Console.WriteLine($"R${valorDeposito} depositado com sucesso!");
            Console.WriteLine($"O novo saldo é de R${Saldo}");

            Operacao operacao = new Operacao("Depósito", valorDeposito);
            operacoes.Add(operacao);

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }

    public void Sacar()
    {
        Console.Clear();
        Console.WriteLine("SAQUE");
        Console.WriteLine($"Seu saldo é de: R${Saldo}");

        Console.Write("\nDigite o valor a ser sacado: ");
        decimal valorSaque = decimal.Parse(Console.ReadLine()!);

        if (valorSaque <= 0)
        {
            Console.WriteLine("Digite um valor acima de zero");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
        else if (valorSaque > Saldo)
        {
            Console.WriteLine("Saldo insuficiente");
            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
        else
        {
            Saldo -= valorSaque;
            Console.WriteLine($"R${valorSaque} sacado com sucesso!");
            Console.WriteLine($"O novo saldo é de R${Saldo}");

            Operacao operacao = new Operacao("Saque", valorSaque);
            operacoes.Add(operacao);

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }

    public bool Transferir(Conta destino, decimal valor)
    {
        if (destino == null)
            return false;

        if (valor <= 0)
            return false;

        if (valor > Saldo)
            return false;

        Saldo -= valor;
        Operacao operacaoOrigem = new Operacao("Transferência enviada", valor);
        operacoes.Add(operacaoOrigem);

        destino.Saldo += valor;
        Operacao operacaoDestino = new Operacao("Transferência recebida", valor);
        destino.operacoes.Add(operacaoDestino);

        return true;
    }


    public void MostrarExtrato()
    {
        Console.Clear();
        Console.WriteLine("EXTRATO");

        if (operacoes.Count == 0)
        {
            Console.WriteLine("Nenhuma transação de dinheiro registrada");
            Console.ReadKey();
        }
        foreach (Operacao operacao in operacoes)
        {
            Console.WriteLine($"{operacao.DataHora:dd/MM/yyyy HH:mm} | {operacao.Tipo} | R${operacao.Valor}");
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }
}