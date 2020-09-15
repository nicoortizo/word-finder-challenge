using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace Matrix
{
    public class MatrixGenerator
    {

        private char[,] matrix;
        private int sizeX;
        private int sizeY;

        private List<Word> WordList { get; } = new List<Word>();

        public MatrixGenerator(int sizeX, int sizeY, List<string> wordList)
        {
            Randomizer.Seed = new Random(8675309);
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            matrix = new char[sizeX, sizeY];

            var faker = new Faker("en");

            foreach (string word in wordList)
            {
                this.place(word, faker);
            }


            for(int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if(matrix[i,j] == '\0')
                        matrix[i, j] = faker.Random.Char(min:'a', max: 'z');
                }
            }

        }

        public List<String> ToStringArray()
        {
            var matrixString = new List<string>();
            for (int i = 0; i < sizeX; i++)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < sizeY; j++)
                {
                    sb.Append(matrix[i, j]);
               //     sb.Append(space);

                }
                matrixString.Add(sb.ToString());
            }
            return matrixString;
        }

        private void place(string word, Faker faker)
        {
            bool placed = false;
            var newWord = new Word() { WordValue = word };

            while (!placed)
            {
                newWord.PositionX = faker.Random.Int(0, sizeX - 1);
                newWord.PositionY = faker.Random.Int(0, sizeY - 1);
                newWord.Direction = faker.PickRandom<eDirection>();

                switch (newWord.Direction)
                {
                    case eDirection.horizontal:
                        placed = this.placeHorizontal(newWord);
                        break;
                    case eDirection.vertical:
                        placed = this.placeVertical(newWord);
                        break;
                    default:
                        break;
                }
            }
            WordList.Add(newWord);
        }

        private bool placeHorizontal(Word word)
        {
            bool available = true;
            if (word.PositionY + word.WordValue.Length >= sizeY) return false;

            for (int i = 0, j = word.PositionY; i < word.WordValue.Length; i++, j++)
            {
                if(matrix[word.PositionX, j] != '\0' &&
                    matrix[word.PositionX, j] != word.WordValue[i])
                {
                    available = false;
                    break;
                }

            }

            if(available)
            {
                for (int i = 0, j = word.PositionY; i < word.WordValue.Length; i++, j++)
                {
                    matrix[word.PositionX, j] = word.WordValue[i];
                }
            }
            return available;
        }

        private bool placeVertical(Word word)
        {
            bool available = true;
            if (word.PositionX + word.WordValue.Length >= sizeX) return false;

            for (int i = 0, j = word.PositionX; i < word.WordValue.Length; i++, j++)
            {
                if (matrix[j, word.PositionY] != '\0' &&
                    matrix[j, word.PositionY] != word.WordValue[i])
                {
                    available = false;
                    break;
                }

            }

            if (available)
            {
                for (int i = 0, j = word.PositionX; i < word.WordValue.Length; i++, j++)
                {
                    matrix[j, word.PositionY] = word.WordValue[i];
                }
            }
            return available;
        }
    }
}
