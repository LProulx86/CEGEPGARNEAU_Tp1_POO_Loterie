/******************************************************************************
 * Classe:      Mise
 * 
 * Fichier:     Mise.cs
 * 
 * Auteur:      Laurent Proulx
 * 
 * But:         Représente une mise à six nombres
 * 
 *****************************************************************************/

using System;
using Utilitaires;

namespace SimulationLoterie
{
    public class Mise
    {

        private int[] m_iLesNombres;
        /// <summary>
        /// Permets la création d'une mise. Remplis automatiquement 
        /// la mise de 6 nombres aléatoires différents. Trie les nombres.
        /// </summary>
        public Mise()
        {

            m_iLesNombres = new int[6];

            for (int i = 0; i < 6; i++)
            {
                m_iLesNombres[i] = Aleatoire.GenererNombre(48) + 1;
                int j = 0;

                //vérification des doublons.
                while (j < i)
                {
                    // création d'un nouveau nombre, 
                    // plus retour au début pour tout vérifier.
                    if (m_iLesNombres[j] == m_iLesNombres[i])
                    {
                        m_iLesNombres[i] = Aleatoire.GenererNombre(48) + 1;
                        j = -1;
                    }
                    j++;
                }
            }
            // important pour la validation.
            Array.Sort(m_iLesNombres);
        }

        /// <summary>
        /// Permet de récuppérer la mise si l'indice est valide.
        /// </summary>
        /// <param name="iIndice"> valeur en 0 et 5</param>
        /// <returns>Retourne la valeur de la mise sinon -1.</returns>
        public int GetNombre(int iIndice)
        {
            if (iIndice >= 0 && iIndice <= 5)
            {
                return m_iLesNombres[iIndice];
            }
            else
            {
                return -1;
            }
        }
    }
}