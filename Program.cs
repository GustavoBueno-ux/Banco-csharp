List<Conta> contas = new List<Conta>();
int Id = 1;

bool OpcoesMenu()
{
    Console.Clear();

    Console.WriteLine("1 - Criar conta");
    Console.WriteLine("2 - Vizualizar contas");
    Console.WriteLine("3 - Selecionar conta");
    Console.WriteLine("0 - Sair");

    int escolha = int.Parse(Console.ReadLine()!);

    switch (escolha)
    {
        case 0:
            return false;
        case 1:
            CriarConta();
            break;
        case 2:
            VizualizarContas();
            break;
        case 3:
            SelecionarConta();
            break;
        default:
            Console.WriteLine("Opção inválida!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            break;
    }

    return true;
}

void CriarConta()
{
    Console.Clear();

    Console.WriteLine("Digite seu nome para criarmos sua conta: ");
    string nome = Console.ReadLine()!;

    Conta conta = new Conta(Id, nome);
    Id++;
    contas.Add(conta);

    Console.WriteLine($"Conta de número #{conta.IdConta} de {conta.NomeTitular} criada com sucesso!");

    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}

void VizualizarContas()
{
    if (contas.Count == 0)
    {
        Console.WriteLine("Nenhuma conta registrada no momento.");
    }
    else
    {
        foreach (Conta conta in contas)
        {
            Console.WriteLine($"[{conta.IdConta}] {conta.NomeTitular}");
        }
    }
    
    Console.WriteLine("\nPressione qualquer tecla para voltar...");
    Console.ReadKey();
}

void SelecionarConta()
{
    Console.Clear();

    if (contas.Count == 0)
    {
        Console.WriteLine("Nenhuma conta cadastrada.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("Contas disponíveis:\n");

    foreach (Conta conta in contas)
    {
        Console.WriteLine($"[{conta.IdConta}] {conta.NomeTitular}");
    }

    Console.Write("\nDigite o ID da conta que deseja acessar: ");
    int idEscolhido = int.Parse(Console.ReadLine()!);

    Conta? contaSelecionada = contas.Find(c => c.IdConta == idEscolhido);

    if (contaSelecionada == null)
    {
        Console.WriteLine("Conta não encontrada.");
        Console.ReadKey();
        return;
    }

    MenuConta(contaSelecionada);
}

void MenuConta(Conta conta)
{
    bool dentroDaConta = true;
    while (dentroDaConta)
    {
        Console.Clear();

        Console.WriteLine($"Responsável pela conta: {conta.NomeTitular}");
        Console.WriteLine($"Saldo atual: R${conta.Saldo}");

        Console.WriteLine("\n1 - Depositar");
        Console.WriteLine("2 - Sacar");
        Console.WriteLine("3 - Transferir");
        Console.WriteLine("4 - Extrato");
        Console.WriteLine("0 - Voltar");

        Console.Write("\nDigite a sua opção: ");
        int escolha = int.Parse(Console.ReadLine()!);

        switch (escolha)
        {
            case 1:
                conta.Depositar();
                break;
            case 2:
                conta.Sacar();
                break;
            case 3:
                Transferir(conta);
                break;
            case 4:
                conta.MostrarExtrato();
                break;
            case 0:
                dentroDaConta = false;
                break;
            default:
                Console.WriteLine("Opção inválida!");
                Console.ReadKey();
                break;
        }
    }
}

void Transferir(Conta contaAtual)
{
    Console.Clear();
    Console.WriteLine("TRANSFERÊNCIA\n");
    Console.WriteLine($"Seu saldo é de: {contaAtual.Saldo}");

    if (contas.Count <= 1)
    {
        Console.WriteLine("Não há outras contas para transferir.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("Contas disponíveis:");

    foreach (Conta conta in contas)
    {
        if (conta.IdConta != contaAtual.IdConta)
        {
            Console.WriteLine($"[{conta.IdConta}] {conta.NomeTitular}");
        }
    }

    Console.Write("\nDigite o ID da conta destino: ");
    int idDestino = int.Parse(Console.ReadLine()!);

    Conta? contaDestino = contas.Find(c => c.IdConta == idDestino);

    if (contaDestino == null || contaDestino.IdConta == contaAtual.IdConta)
    {
        Console.WriteLine("Conta inválida.");
        Console.ReadKey();
        return;
    }

    Console.Write("Digite o valor da transferência: ");
    decimal valor = decimal.Parse(Console.ReadLine()!);

    bool sucesso = contaAtual.Transferir(contaDestino, valor);

    if (sucesso)
        Console.WriteLine("Transferência realizada com sucesso!");
    else
        Console.WriteLine("Não foi possível realizar a transferência.");

    Console.ReadKey();
}

while (OpcoesMenu())
{
}