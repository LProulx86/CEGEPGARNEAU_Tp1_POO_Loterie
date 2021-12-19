/******************************************************************************
 * Classe:      Resultats
 * 
 * Fichier:     Resultats.cs
 * 
 * Auteur:      Laurent Proulx
 * 
 * But:         Permet de calculer le nombre de mises gagnant dans un Tirage.
 * 
 *****************************************************************************/

namespace SimulationLoterie
{
    /// <summary>
    /// Enumération de valeurs gagantes possible.
    /// </summary>
    public enum Indice
    {
        DeuxSurSixPlus,
        TroisSurSix,
        QuatreSurSix,
        CinqSurSix,
        CinqSurSixPlus,
        SixSurSix
    }

    public class Resultats
    {
        private int[] m_ilesQuantites;

        /// <summary>
        /// Crée le tableau des résultats.
        /// </summary>
        public Resultats()
        {
            m_ilesQuantites = new int[6];
        }

        /// <summary>
        /// Permets de récupéré le nombre de mises gagnantes.
        /// </summary>
        /// <param name="gagnant">Nom de l'énumération rechercher.</param>
        /// <returns>Le nombre de mise gagnante.</returns>
        public int GetQuantite(Indice gagnant)
        {
            return m_ilesQuantites[(int)gagnant];
        }

        /// <summary>
        /// Permets d'augmenter de 1 la quantité de résultats gagnant
        /// </summary>
        /// <param name="gagnant">Nom de l'énumération</param>
        public void AugmenterQuantite(Indice gagnant)
        {
            m_ilesQuantites[(int)gagnant]++;
        }
    }
}