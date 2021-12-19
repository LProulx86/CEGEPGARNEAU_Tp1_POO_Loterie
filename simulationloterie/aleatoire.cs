using System;

namespace Utilitaires
{
    public static class Aleatoire
    {
        public static Random g_rndGenerateur = new Random();

        public static int GenererNombre(int iBorneSuperieure)
        {
            return g_rndGenerateur.Next(iBorneSuperieure + 1);
        }
    }

}