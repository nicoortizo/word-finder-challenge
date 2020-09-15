using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Matrix;
using Bogus;
using WordFinder;

namespace WordFinderChanllenge
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var faker = new Faker("en");

                var words = faker.Random.WordsArray(faker.Random.Int(min: 500, max: 500));

                Console.WriteLine("Random Words------------------");
                foreach (var line in words)
                {
                    System.Console.WriteLine(line);
                }

                var matrixWords = new List<string>();
                for (int i = 0; i < faker.Random.Int(min: 100, max: 300); i++)
                {
                    matrixWords.Add(faker.PickRandom(words));
                }

                Console.WriteLine("\nRandom Words to generate Matrix----------");
                foreach (var line in matrixWords)
                {
                    System.Console.WriteLine(line);
                }


                var matrix = new MatrixGenerator(64, 64, matrixWords);

                var matrixList = matrix.ToStringArray();

                Console.WriteLine("\nMatrix-------------");
                foreach (var line in matrixList)
                {
                    System.Console.WriteLine(line);
                }

                var finder = new WordFinder.WordFinder(matrixList);

                var s1 = new Stopwatch();
                s1.Start();

                var result = finder.Find(words);
                s1.Stop();

                Console.WriteLine($"\nlapsed time {s1.ElapsedMilliseconds} ms -----------------");


                Console.WriteLine("\nTop 10 most repeated found-----------------");
                foreach (var line in result)
                {
                    System.Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("\nPress any key to finish-----------------");
                System.Console.ReadKey();
            }
        }
    }
}
