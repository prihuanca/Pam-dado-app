using System;

namespace Dado
{
    class Dado
    {
        private Random random;

        public Dado()
        {
            random = new Random();
        }

        public int Jogar(int lados = 6)
        {
            return random.Next(1, lados + 1);
        }

        public string GetImagem(int numero)
        {
            return $"dado{numero}.png";
        }
    }
}


