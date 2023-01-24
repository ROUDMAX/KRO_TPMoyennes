//using System;
//using System.Collections.Generic;
//using System.Linq;


namespace TPMoyennes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Création d'une classe
            Classe sixiemeA = new Classe("6eme A");
            // Ajout des élèves à la classe
            sixiemeA.ajouterEleve("Jean", "RAGE");
            sixiemeA.ajouterEleve("Paul", "HAAR");
            sixiemeA.ajouterEleve("Sibylle", "BOQUET");
            sixiemeA.ajouterEleve("Annie", "CROCHE");
            sixiemeA.ajouterEleve("Alain", "PROVISTE");
            sixiemeA.ajouterEleve("Justin", "TYDERNIER");
            sixiemeA.ajouterEleve("Sacha", "TOUILLE");
            sixiemeA.ajouterEleve("Cesar", "TICHO");
            sixiemeA.ajouterEleve("Guy", "DON");
            // Ajout de matières étudiées par la classe
            sixiemeA.ajouterMatiere("Francais");
            sixiemeA.ajouterMatiere("Anglais");
            sixiemeA.ajouterMatiere("Physique/Chimie");
            sixiemeA.ajouterMatiere("Histoire");
            Random random = new Random();
            // Ajout de 5 notes à chaque élève et dans chaque matière
            for (int ieleve = 0; ieleve < sixiemeA.eleves.Count; ieleve++)
            {
                for (int matiere = 0; matiere < sixiemeA.matieres.Count; matiere++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        sixiemeA.eleves[ieleve].ajouterNote(new Note(matiere, (float)((6.5 +
                       random.NextDouble() * 34)) / 2.0f));
                        // Note minimale = 3
                    }
                }
            }

            Eleve eleve = sixiemeA.eleves[6];
            // Afficher la moyenne d'un élève dans une matière
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            eleve.Moyenne(1) + "\n");
            // Afficher la moyenne générale du même élève
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne Generale : " + eleve.Moyenne() + "\n");
            // Afficher la moyenne de la classe dans une matière
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            sixiemeA.Moyenne(1) + "\n");
            // Afficher la moyenne générale de la classe
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne Generale : " + sixiemeA.Moyenne() + "\n");
            Console.Read();
        }
    }
}

// Classes fournies par HNI Institut
class Note
{
    public int matiere { get; private set; }
    public float note { get; private set; }
    public Note(int m, float n)
    {
        matiere = m;
        note = n;
    }
}

class Classe
{
    //nom de la classe
    public string nomClasse { get;  set; }
    //liste de matieres
    public List<string> matieres { get;  set; }
    //liste d'eleves
    public List<Eleve> eleves { get; set; }
    //nombre d'eleves
    static int nbeleve = 0;
    //nombre de matieres
    static int nbmatiere = 0;
    public Classe(string nom_classe)
    {
        // trim pour enlever les espace au debut et a la fin.
        nomClasse = nom_classe.Trim();
        eleves = new List<Eleve>();
        matieres = new List<string>();
    }
    //ajouter un eleve
    public void ajouterEleve(string prenom, string nom)
    {
        Eleve e = new Eleve(prenom, nom);
        eleves.Add(e);
        nbeleve++;
    }
    //ajouter une matiere
    public void ajouterMatiere(string matiere)
    {
        matieres.Add(matiere);
        nbmatiere++;
    }
    //calcule de la moyenne de la class
    public float Moyenne()
    {
        float somme = 0;
        for (int ieleve = 0; ieleve < eleves.Count; ieleve++)
        {
            somme += eleves[ieleve].Moyenne();
        }
        if (Classe.nbeleve == 0)
        {
            return 0;
        }
        else
        {
            return somme / eleves.Count;
        }
    }
    // calcule de la moyenne de la classe par matiere
    public float Moyenne(int m)
    {
        float somme = 0;
        for (int ieleve = 0; ieleve < eleves.Count; ieleve++)
        {
            somme += eleves[ieleve].Moyenne(m);
        }
        if (Classe.nbeleve == 0 || Classe.nbmatiere == 0)
        {
            return 0;
        }
        else
        {
            return somme / eleves.Count;
        }
    }
}
// class des eleves
class Eleve
{
    public string prenom { get;  set; }

    public string nom { get;  set; }

    public List<Note> notes { get; set; }

    //nombre de note
    static int nbnote = 0;

    // constructeur
    public Eleve(string prenomA, string nomA)
    {
        prenom = prenomA;
        nom = nomA;
        notes = new List<Note>(); //un eleve recoit 200 notes max par année
    }
    // fonction qui permet l'ajout d'une note a un eleve
    public void ajouterNote(Note n)
    {
        notes.Add(n);
        nbnote++;
    }
    // moyenne pour chaque matiere (l'id de la matiere en parametre)
    public float Moyenne(int m)
    {
        float somme = 0;
        int div = 0;
        for (int i= 0; i < notes.Count ; i++)
        {
            if (notes[i].matiere == m)
            {
                somme += notes[i].note;
                div++;
            }
        }
        // le cas ou il n'existe pas de note pour cette matiere
        if (div == 0)
        {
            return 0;
        }
        // en moins une note est renseigné dans cette matière
        else
        {
            return somme / div;
        }
    }
    // calcule de moyenne generale par eleve
    public float Moyenne()
    {
        float somme = 0;
        // j'utilise un conteur au lieu de la taille du tableau note pour etre sur du bon fonctionnement de la fonction
        int div = 0;
        for (int i = 0; i < notes.Count; i++)
        {
           somme += notes[i].note;
           div++;
        }
        // le cas ou les notes ne sont pas renseigné
        if (div == 0)
        {
            return 0;
        }
        // si en moins une note est renseigné
        else
        {
            return somme / div;
        }
    }

}


