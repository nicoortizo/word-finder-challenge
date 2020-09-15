using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder
{
    internal class WordFounderComparer : IEqualityComparer<WordFound>
    {
        public bool Equals(WordFound x, WordFound y)
        {
            return x.word == y.word;
        }

        public int GetHashCode(WordFound obj)
        {
            return obj.word.GetHashCode();
        }
    }
}
