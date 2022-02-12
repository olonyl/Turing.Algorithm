using System;
using System.Collections.Generic;
using System.Linq;

namespace Turing.Algorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var generator = new WrapperGenerator(3);
            Console.WriteLine(generator.ToString());

            var superGenerator = new WrapperWithSuperPowers(3);
            Console.WriteLine(superGenerator.ToString());

            Console.Read();
        }
    }

    public class WrapperWithSuperPowers
    {
        private int _quantity;

        public WrapperWithSuperPowers(int quantity)
        {
            _quantity = quantity;
        }
        public override string ToString()
        {
            return $"['{string.Join("','", Generate())}']";
        }

        public IList<string> Generate()
        {
            var values = new List<string>();
            GenerateParenthesis(_quantity, ref values);

            return values;
        }

        private void GenerateParenthesis(int n, ref List<string> values, int open = 0, int close = 0, string s = "")
        {
            if (open == n && close == n)
            {
                values.Add(s);
                return;
            }
            if (open < n)
            {
                GenerateParenthesis(n, ref values, open + 1, close, s + "(");
            }
            if (close < open)
            {
                GenerateParenthesis(n, ref values, open, close + 1, s + ")");
            }
        }

    }
    public class WrapperGenerator
    {
        private int _quantity;

        public WrapperGenerator(int quantity)
        {
            _quantity = quantity;
        }
        public IList<string> Generate()
        {
            return GenerateLeftElements(_quantity).Concat(GenerateRightElements(_quantity - 1)).ToList();
        }

        public override string ToString()
        {
            return $"['{string.Join("','", Generate())}']";
        }

        private IList<string> GenerateRightElements(int remaining, IList<string> previousValues = null)
        {
            if (remaining == 0)
                return previousValues;
            if (previousValues == null)
                previousValues = new List<string>();
            var suffix = $"({ GenerateCouples(remaining)})";
            var prefix = $"{ GenerateCouples(_quantity - (remaining + 1))}";
            var currentElement = $"{prefix}{suffix}";

            var newElements = new List<string>();

            newElements.AddRange(previousValues);
            newElements.Add(currentElement);

            return GenerateRightElements(remaining - 1, newElements);

        }
        private IList<string> GenerateLeftElements(int remaining, IList<string> values = null)
        {
            if (remaining == 0)
                return values;
            if (values == null)
                values = new List<string>();

            var totalSuffixElementsToGenerate = _quantity - remaining;

            var suffix = GenerateCouples(totalSuffixElementsToGenerate);
            var prefix = $"{GenerateLayers("()", remaining)}{suffix}";
            var newElements = new List<string>();

            newElements.AddRange(values);
            newElements.Add(prefix);

            return GenerateLeftElements(remaining - 1, newElements);
        }
        private string GenerateLayers(string innerElement, int remaining)
        {
            if (remaining <= 1)
                return innerElement;

            return GenerateLayers($"({innerElement})", remaining - 1);
        }
        private string GenerateCouples(int totalElementsToGnerate)
        {
            return string.Join(string.Empty, Enumerable.Repeat("()", totalElementsToGnerate));
        }
    }
}
