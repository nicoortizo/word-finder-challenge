using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder
{
    public class WordFinder : IWordFinder
    {
        const int MATRIX_ROW_SIZE = 64;
        const int MATRIX_COLUM_SIZE = 64;

        private IEnumerable<string> matrix;
        private ConcurrentDictionary<string,int> foundList;
        public WordFinder(IEnumerable<string> matrix)
        {
            ValidateMatrix(matrix);
            this.matrix = matrix;
            this.foundList = new ConcurrentDictionary<string, int>();
        }

        private void ValidateMatrix(IEnumerable<string> matrix) {
            if (matrix.Count() != MATRIX_ROW_SIZE) throw new Exception("Invalid matrix row size");

            for (int i = 0; i < matrix.Count(); i++)      
                if(matrix.ElementAt(i).Length != MATRIX_COLUM_SIZE)
                    throw new Exception("Invalid matrix column size");
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var thorizontal = Task.Run(() => FindHorizontal(wordstream));
            

            var tvertical = Task.Run(() => FindVertical(wordstream));
            tvertical.Wait();
            thorizontal.Wait();
            return foundList?.OrderByDescending(w => w.Value)
                    ?.Select(w => w.Key)
                    ?.Take(10)
                    ?? new List<string>();
        }

        private void FindHorizontal(IEnumerable<string> wordstream)
        {
            for (int w = 0; w < wordstream.Count(); w++)
            {
                for (int i = 0; i < matrix.Count(); i++)
                {
                    int matrixLength = matrix.First().Length;
                    for (int j = 0; j < matrixLength;)
                    {
                        var indexfound = matrix.ElementAt(i).IndexOf(wordstream.ElementAt(w), j);
                        if (indexfound >= 0)
                        {
                            foundList.AddOrUpdate(wordstream.ElementAt(w), 1, (k, v) => ++v);
                            j = indexfound + wordstream.ElementAt(w).Length;
                        }
                        else
                        {
                            j = matrixLength;
                        }
                    }

                }
            }
        }

        private void FindVertical(IEnumerable<string> wordstream)
        {
            for (int w = 0; w < wordstream.Count(); w++)
            {
                for (int i = 0; i < matrix.First().Length; i++)
                {
                    string verticalString = new string(matrix.Select(c => c[i]).ToArray());

                    int matrixLength = matrix.Count();
                    for (int j = 0; j < matrixLength;)
                    {
                        var indexfound = verticalString.IndexOf(wordstream.ElementAt(w), j);
                        if (indexfound >= 0)
                        {
                            foundList.AddOrUpdate(wordstream.ElementAt(w), 1, (k, v) => ++v);
                            j = indexfound + wordstream.ElementAt(w).Length;
                        }
                        else
                        {
                            j = matrixLength;
                        }
                    }
                }
            }
        }

    }
}
