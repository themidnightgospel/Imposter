using System.Collections.Generic;
using Imposter.Abstractions;
using Imposter.Tests.Features.ClassImposter.Suts;

[assembly: GenerateImposter(typeof(ClassWithMultiParameterIndexer))]

namespace Imposter.Tests.Features.ClassImposter.Suts
{
    public class ClassWithMultiParameterIndexer
    {
        private readonly Dictionary<(int Row, string Column), int> _values = new Dictionary<(int Row, string Column), int>();

        protected virtual int this[int row, string column]
        {
            get => _values.TryGetValue((row, column), out var value) ? value : -1;
            set => _values[(row, column)] = value;
        }

        public int ReadValue(int row, string column) => this[row, column];

        public void WriteValue(int row, string column, int value) => this[row, column] = value;
    }
}
