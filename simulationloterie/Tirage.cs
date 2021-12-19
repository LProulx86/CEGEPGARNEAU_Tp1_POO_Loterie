/******************************************************************************
 * Classe:      Tirage
 * 
 * Fichier:     Tirage.cs
 * 
 * Auteur:      Laurent Proulx
 * 
 * But:         Représente un Tirage.
 * 
 *****************************************************************************/

using System;
using Utilitaires;

namespace SimulationLoterie
{
    public class Tirage
    {
        private DateTime m_dtmTirage;
        private int[] m_iLesNombresGagnants;
        private Mise[] m_lesMises;
        private Resultats m_lesresultats;

        /// <summary>
        /// Création d'un tirage à la date donnée.
        /// </summary>
        /// <param name="dtmDate">La date du tirage.</param>
        public Tirage(DateTime dtmDate)
        {
            m_dtmTirage = dtmDate;
        }

        /// <summary>
        /// Permet de récuppérer la date du tirage.
        /// </summary>
        public DateTime Date
        {
            get
            {
                return m_dtmTirage;
            }

        }

        /// <summary>
        /// Permets de récupéré le nombre de mise dans le tirage.
        /// </summary>
        public int NbMises
        {
            get
            {
                if (m_lesMises != null)
                {
                    return m_lesMises.Length;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Permets de récupérer les résultats du tirage.
        /// </summary>
        public Resultats Resultats
        {
            get
            {
                return m_lesresultats;
            }
        }

        /// <summary>
        /// Revois une chaine de caractère formaté pour affichage.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strChaine;
            if (NbMises != 0)
            {
                strChaine = "Résultat du tirage du " + m_dtmTirage.Year + "-" +
                    m_dtmTirage.Month + "-" + m_dtmTirage.Day + "\n";
                strChaine += "Nombres de mises:\t" + NbMises + "\n";
                strChaine += "Gagnants du 2 sur 6+\t";
                strChaine += Resultats.GetQuantite
                    (Indice.DeuxSurSixPlus) + "\n";
                strChaine += "Gagnants du 3 sur 6\t";
                strChaine += Resultats.GetQuantite
                    (Indice.TroisSurSix) + "\n";
                strChaine += "Gagnants du 4 sur 6\t";
                strChaine += Resultats.GetQuantite
                    (Indice.QuatreSurSix) + "\n";
                strChaine += "Gagnants du 5 sur 6\t";
                strChaine += Resultats.GetQuantite
                    (Indice.CinqSurSix) + "\n";
                strChaine += "Gagnants du 5 sur 6+\t";
                strChaine += Resultats.GetQuantite
                    (Indice.CinqSurSixPlus) + "\n";
                strChaine += "Gagnants du 6 sur 6\t";
                strChaine += Resultats.GetQuantite
                    (Indice.SixSurSix);
                return strChaine;
            }
            else
            {
                return "Les mises n'ont pas encore " +
                    "été validées pour ce tirage.";
            }
        }

        /// <summary>
        /// Permets la création des Mises à la quantité désiré.
        /// </summary>
        /// <param name="nbDeMises"> Nombre de mise voulu, 
        /// doit etre compris entre 100 000 et 300 000 
        /// sinon 200 000 sera mis par défaut.</param>
        public void InscrireMises(int nbDeMises)
        {
            const int NB_MISES_MIN = 100000;
            const int NB_MISES_MAX = 300000;

            if (nbDeMises > NB_MISES_MIN && nbDeMises < NB_MISES_MAX)
            {
                m_lesMises = new Mise[nbDeMises];
            }
            else
            {
                nbDeMises = (NB_MISES_MIN + NB_MISES_MAX) / 2;
                m_lesMises = new Mise[nbDeMises];
            }

            // création des Mises.
            for (int i = 0; i < nbDeMises; i++)
            {
                m_lesMises[i] = new Mise();
            }
        }

        /// <summary>
        /// Crée les nombres gagnants d'un tirage.
        /// </summary>
        /// <returns>Retourne vrai si réussit.</returns>
        public bool Effectuer()
        {
            //vérifie s'il y a des Mises.
            if (m_lesMises.Length > 0)
            {
                m_iLesNombresGagnants = new int[7];

                //Remplis la dernière case pour l'exclure du triage.
                m_iLesNombresGagnants[6] = 50;

                //Remplis toutes les cases sauf la dernère. 
                //Vérifie qu'il n'y a pas de doublon.
                for (int i = 0; i < 6; i++)
                {
                    m_iLesNombresGagnants[i] = Aleatoire.GenererNombre(48) + 1;
                    int j = 0;
                    while (j < i)
                    {
                        if (m_iLesNombresGagnants[j] ==
                            m_iLesNombresGagnants[i])
                        {
                            m_iLesNombresGagnants[i] =
                                Aleatoire.GenererNombre(48) + 1;
                            j = 0;
                        }
                        j++;
                    }
                }
                Array.Sort(m_iLesNombresGagnants);

                // création du complémentaire.
                m_iLesNombresGagnants[6] = Aleatoire.GenererNombre(48) + 1;
                int nb = 0;

                //Vérifie qu'il n'y a pas de doublon avec 
                //le complémentaire sinon relance.
                while (m_iLesNombresGagnants[nb] <
                    m_iLesNombresGagnants[6] & nb < 5)
                {
                    if (m_iLesNombresGagnants[nb] == m_iLesNombresGagnants[6])
                    {
                        m_iLesNombresGagnants[6] =
                            Aleatoire.GenererNombre(48) + 1;
                        nb = -1;
                    }
                    nb++;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Permets la vérification des Mises gagnantes.
        /// </summary>
        /// <returns>Vrai si réussit.</returns>
        public bool ValiderMises()
        {
            if (m_iLesNombresGagnants != null)
            {
                m_lesresultats = new Resultats();

                for (int i = 0; i < m_lesMises.Length; i++)
                {
                    // quantité de nombre dans une mise.
                    const int nbDeNombre = 6;
                    // variables d'incrémentation.
                    int j = 0;
                    int k = 0;
                    int h = 0;
                    // nombre de concordances principale.
                    int compteur = 0;
                    // concordances complémentaire.
                    int complement = 0;

                    // vérifie la concordance entre deux tableaux triés.
                    while (j < nbDeNombre && k < nbDeNombre)
                    {
                        if (m_lesMises[i].GetNombre(j) ==
                            m_iLesNombresGagnants[k])
                        {
                            compteur++;
                            j++;
                            k++;
                        }
                        else if (m_lesMises[i].GetNombre(j) <
                            m_iLesNombresGagnants[k])
                        {
                            j++;
                        }
                        else
                        {
                            k++;
                        }
                    }

                    // vérifie la concordance avec le complémentaire.
                    while (complement < 1 && h < nbDeNombre)
                    {
                        if (m_lesMises[i].GetNombre(h) ==
                            m_iLesNombresGagnants[6])
                        {
                            complement++;
                        }
                        h++;
                    }

                    //calcul des points relativement aux concordances.
                    int caseSwitch = (compteur * 10) + complement;

                    //Ajout d'un résultats si une mise est gagnante.
                    switch (caseSwitch)
                    {
                        case 21:
                            Resultats.AugmenterQuantite(Indice.DeuxSurSixPlus);
                            break;
                        case 30:
                            Resultats.AugmenterQuantite(Indice.TroisSurSix);
                            break;
                        case 40:
                            Resultats.AugmenterQuantite(Indice.QuatreSurSix);
                            break;
                        case 50:
                            Resultats.AugmenterQuantite(Indice.CinqSurSix);
                            break;
                        case 51:
                            Resultats.AugmenterQuantite(Indice.CinqSurSixPlus);
                            break;
                        case 60:
                            Resultats.AugmenterQuantite(Indice.SixSurSix);
                            break;
                        default: break;
                    }

                }
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}