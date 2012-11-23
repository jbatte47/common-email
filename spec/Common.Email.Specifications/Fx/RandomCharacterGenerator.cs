using System;
using System.Threading.Tasks;

namespace Common.Email.Specifications.Fx
{
    internal class RandomCharacterGenerator : IDisposable
    {
        private readonly Random _random = new Random();
        private bool _generate = true;
        private readonly int _min;
        private readonly int _max;

        public RandomCharacterGenerator(int min, int max, IObserver<char> observer)
        {
            _min = min;
            _max = max;
            Task.Factory.StartNew(() => GenerateCharacters(observer));
        }

        public void Dispose()
        {
            _generate = false;
        }

        private void GenerateCharacters(IObserver<char> observer)
        {
            while (_generate)
            {
                var character = (char)_random.Next(_min, _max);
                observer.OnNext(character);
            }
        }
    }
}