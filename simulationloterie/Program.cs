/******************************************************************************
 * 
 * Auteur:      Laurent Proulx
 * 
 *****************************************************************************/

using SimulationLoterie;
using System;

namespace loterie
{
    class Program
    {
        // Rend le gestionnaire visible.
        public static GestionnaireTirages leGestionnaire;
        static bool bValidation = false;
        static bool bEffectuer = false;

        static void Main(string[] args)
        {
            string choix = "-1";


            while (choix != "5")
            {
                choix = AfficherMenu();

                switch (choix)
                {
                    case "1":
                        leGestionnaire = CreerGestionnaire();
                        break;
                    case "2":
                        ValidationTirages();
                        break;
                    case "3":
                        ResumerTirage();
                        break;
                    case "4":
                        Info();
                        break;
                    default:
                        break;
                }
            }
            Console.Write("Quitter le programme...");
            Console.ReadLine();
        }

        static string AfficherMenu()
        {
            string choixU = "-1";

            Console.Write
                (
                "Menu principale\n" +
                "\n" +
                "[1]  Génération de données\n" +
                "[2]  Résultats des tirages\n" +
                "[3]  Sommaire des résultats\n" +
                "[4]  Auteur\n" +
                "[5]  Quittter\n" +
                "\n" +
                "Votre Choix:"
                );

            choixU = Console.ReadLine();
            return choixU;
        }

        /// <summary>
        /// Création du Gestionnaire, 
        /// création des Tirages et 
        /// inscription des Mises.
        /// </summary>
        /// <returns></returns>
        static GestionnaireTirages CreerGestionnaire()
        {
            GestionnaireTirages Gestion = new GestionnaireTirages();

            // Permet la création d'un nouveau gestionaire.
            bValidation = false;
            bEffectuer = false;

            int noTirage = 0;

            Console.Clear();

            //Remplis toutes les cases du gestionnaire.
            while (Gestion.GetTirage(noTirage) != null)
            {
                Tirage leTirage = Gestion.GetTirage(noTirage);

                leTirage.InscrireMises(
                    Utilitaires.Aleatoire.
                    GenererNombre(200000) + 100000);

                Console.WriteLine("Génération du tirage: " +
                    "{0:yyyy-MM-dd} ", leTirage.Date);

                noTirage++;
            }

            Console.Write("Appuyez sur <Entrée> " +
                "pour retourner au menu principal...");
            Console.ReadLine();
            Console.Clear();
            return Gestion;


        }

        /// <summary>
        /// Effectue le tirage et valides les Mises.
        /// Affiche les résultats.
        /// </summary>
        static void ValidationTirages()
        {
            if (!bEffectuer)
            {
                if (leGestionnaire != null)
                {
                    int i = 0;
                    string chaine = "";

                    while (leGestionnaire.GetTirage(i) != null)
                    {
                        bEffectuer = leGestionnaire.GetTirage(i).Effectuer();

                        bValidation = leGestionnaire.
                            GetTirage(i).ValiderMises();

                        if (bValidation)
                        {
                            chaine = leGestionnaire.GetTirage(i).ToString();
                        }
                        else
                        {
                            Console.WriteLine("Les mises n'ont " +
                                "pas été validé");
                        }

                        Console.WriteLine(chaine);
                        Console.WriteLine("\n\n");
                        i++;
                    }
                }
                else
                {
                    Console.WriteLine("Vous devez avoir généré des données " +
                        "pour voir les résultats des tirages.");
                }
            }
            else
            {
                Console.WriteLine("\nLe tirage est déjà effectué");
            }

            Console.Write("Appuyez sur <Entrée> " +
                "pour retourner au menu principal...");
            Console.ReadLine();
            Console.Clear();
        }

        /// <summary>
        /// Récupère tous les résultats et affiche un résumer.
        /// </summary>
        static void ResumerTirage()
        {
            if (leGestionnaire != null && bValidation)
            {
                int i = 0;
                int[] TotalResultats = new int[6];
                int SommeDesMises = 0;

                while (leGestionnaire.GetTirage(i) != null)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        TotalResultats[j] += leGestionnaire.GetTirage(i).
                                            Resultats.GetQuantite((Indice)j);
                    }
                    SommeDesMises += leGestionnaire.GetTirage(i).NbMises;
                    i++;
                }

                Console.Clear();
                Console.Write("Sommaire des Résultats\n\n" +
                    "Nombre de Mises:\t" + SommeDesMises + "\n" +
                    "Gagnants du 2 sur 6+:\t" + TotalResultats[0] + "\n" +
                    "Gagnants du 3 sur 6:\t" + TotalResultats[1] + "\n" +
                    "Gagnants du 4 sur 6:\t" + TotalResultats[2] + "\n" +
                    "Gagnants du 5 sur 6:\t" + TotalResultats[3] + "\n" +
                    "Gagnants du 5 sur 6+:\t" + TotalResultats[4] + "\n" +
                    "Gagnants du 6 sur 6:\t" + TotalResultats[5] + "\n\n" +
                    "Appuyez sur <Entrée> pour retourner " +
                    "au menu principal...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine("\nVous devez générer le tirage et" +
                    " le valider préalablement");
                Console.Write("Appuyez sur <Entrée> pour " +
                    "retourner au menu principal...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        /// <summary>
        /// Affiche les crédits de l'auteur.
        /// </summary>
        static void Info()
        {
            Console.Write("Travail 1 réalisé par " +
                "Laurent Proulx (DA 1962550) \n\n" +
                "Appuyez sur <Entrée> pour retourner au menu principal...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}