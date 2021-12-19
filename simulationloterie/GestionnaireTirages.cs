/******************************************************************************
 * Classe:      GestionnaireTirages
 * 
 * Fichier:     GestionnaireTirages.cs
 * 
 * Auteur:      Laurent Proulx
 * 
 * But:         Permet la création et la gestion de plusieurs tiages.
 * 
 *****************************************************************************/

using System;

namespace SimulationLoterie
{
    public class GestionnaireTirages
    {
        //crée une année de tirages
        const int NB_TIRAGE = 104;
        private Tirage[] m_lesTirages;
        DateTime aujourdhui;

        /// <summary>
        /// Crée tous les tirages et gère les dates de tirages.
        /// </summary>
        public GestionnaireTirages()
        {
            m_lesTirages = new Tirage[NB_TIRAGE];
            // Date du d'aujourd'hui
            aujourdhui = DateTime.Today;
            // Transtypage de la journée.
            int jour = (int)aujourdhui.DayOfWeek;
            // compteur
            int ajoutDate = 0;

            // Recherche de la première concordance (mercredi ou samedi)
            while (jour != 3 && jour != 6)
            {
                ajoutDate++;
                jour = (int)aujourdhui.AddDays(ajoutDate).DayOfWeek;
            }

            //Création de tous les tirages.
            for (int i = 0; i < NB_TIRAGE; i++)
            {
                if (jour == 3)
                {
                    m_lesTirages[i] = new Tirage
                        (aujourdhui.AddDays(ajoutDate));
                    ajoutDate += 3;
                    jour = (int)aujourdhui.AddDays(ajoutDate).DayOfWeek;
                }
                else if (jour == 6)
                {
                    m_lesTirages[i] = new Tirage
                        (aujourdhui.AddDays(ajoutDate));
                    ajoutDate += 4;
                    jour = (int)aujourdhui.AddDays(ajoutDate).DayOfWeek;
                }
            }
        }

        /// <summary>
        /// Permet de récupérer un tirage.
        /// </summary>
        /// <param name="indice"></param>
        /// <returns>Si le tirage n'existe pas retourne null.</returns>
        public Tirage GetTirage(int indice)
        {
            if (indice >= 0 & indice < NB_TIRAGE)
            {
                return m_lesTirages[indice];
            }
            else
            {
                return null;
            }
        }
    }
}