using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using adonet_db_videogame;

const string connStr = "Data Source = localhost; Initial Catalog = db-videogames; Integrated Security = True";
var repo = new VideogameManager(connStr);

bool exit = false;

while (!exit)
{
    Console.WriteLine("Scegli una di queste opzioni:");
    Console.WriteLine("inserire un nuovo videogioco (1)");
    Console.WriteLine("ricercare un videogioco per id (2)");
    Console.WriteLine("ricercare tutti i videogiochi aventi il nome contenente una determinata stringa inserita in input (3)");
    Console.WriteLine("cancellare un videogioco (4)");
    Console.WriteLine("chiudere il programma ('esci')");
    string input = Console.ReadLine() ?? "";
    Console.WriteLine();

    switch (input)
    {
        case "1":
            Inserisci();
            break;
        case "2":
            RicercaID();
            break;
        case "3":
            RicercaNome();
            break;
        case "4":
            Elimina();
            break;
        case "esci":
            exit = true;
            break;
        default:
            Console.WriteLine("Errore, riprova");
            break;
    }
}

void Inserisci()
{
    Console.Write("Inserisci il nome: ");
    var name = Console.ReadLine() ?? "";

    Console.Write("Inserisci la descrizione: ");
    var overview = Console.ReadLine() ?? "";

    Console.Write("Inserisci la data d'uscita (yyyy-mm-dd): ");
    var releaseDate = Convert.ToDateTime(Console.ReadLine());

    Console.Write("Passa l'id della software house: ");
    var softwareHouseId = Convert.ToInt64(Console.ReadLine());

    var vg = new Videogame(0, name, overview, releaseDate, softwareHouseId);
    var success = repo.InsertVideogame(vg);

    if (success) Console.WriteLine("Videogioco inserito");
    else Console.WriteLine("Inserimento fallito");
}

void RicercaID()
{
    Console.Write("Inserisci ID videogame: ");

    var id = Convert.ToInt64(Console.ReadLine());
    var vg = repo.GetById(id);

    if (vg is null) Console.WriteLine("Videogame non trovato");
    else Console.WriteLine(vg);
}

void RicercaNome()
{
    Console.Write("Inserisci nome videogame: ");

    var name = Console.ReadLine();

    if (string.IsNullOrEmpty(name))
    {
        Console.WriteLine("Il nome del videogioco non può essere vuoto");
        return;
    }

    var vg = repo.GetByName(name);

    if (vg is null) Console.WriteLine("Videogame non trovato");
    else Console.WriteLine("Videogame trovato: " + vg.ToString());
}

void Elimina()
{
    Console.Write("Inserisci ID videogame: ");

    var id = Convert.ToInt64(Console.ReadLine());
    var success = repo.DeleteVideogame(id);

    if (success) Console.WriteLine("Videogioco eliminato");
    else Console.WriteLine("Eliminazione fallita");
}



