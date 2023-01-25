using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesmanProblem
{
    public class Salesman
    {
        private static int CITIES_COUNT = 50;
        private static int MAX_UNIMPROVED_ITERATIONS = 100;
        private static int MIN_DISTANCE = 1;
        private static int MAX_DISTANCE = 99;
        private int[,] distances = new int[CITIES_COUNT,CITIES_COUNT];
        private int genomeScore;
        private int[] genome = new int[CITIES_COUNT];
        private Random rand = new Random();
        public Salesman()
        {
            PrepareData();
            PrintPreparedData();
        }
        public void Run()
        {
            int mutationsCount = 0;
            int unimprovedMutationsCount = 0;

            int[] mutatedGenome;
            int mutatedGenomeScore;
            while (unimprovedMutationsCount < MAX_UNIMPROVED_ITERATIONS)
            {
                mutationsCount++;
                mutatedGenome = Mutate();
                mutatedGenomeScore = Score(mutatedGenome);

                if (mutatedGenomeScore < genomeScore)
                {
                    genome = mutatedGenome;
                    genomeScore = mutatedGenomeScore;
                }
                else
                {
                    unimprovedMutationsCount++;
                }
            }
            PrintBestGenome();
            PrintSummary(mutationsCount);
        }

        private int[] Mutate()
        {
            int[] mutatedGenome = (int[])genome.Clone();

            int mutateIndex1 = rand.Next(0, CITIES_COUNT);
            int mutateIndex2 = rand.Next(0, CITIES_COUNT);
            while(mutateIndex1 == mutateIndex2)
            {
                mutateIndex2 = rand.Next(0, CITIES_COUNT);
            }

            int temp = mutatedGenome[mutateIndex1];
            mutatedGenome[mutateIndex1] = mutatedGenome[mutateIndex2];
            mutatedGenome[mutateIndex2] = temp;

            return mutatedGenome;
        }

        private void PrepareData()
        {
            for (int i = 0; i < CITIES_COUNT; i++)
            {
                for (int j = i; j < CITIES_COUNT; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = 0;
                    }
                    else
                    {
                        distances[i, j] = rand.Next(MIN_DISTANCE, MAX_DISTANCE + 1);
                        distances[j, i] = distances[i, j];
                    }
                }

                genome[i] = i;
            }
            genome = genome.OrderBy(x => rand.Next()).ToArray();
            genomeScore = Score(genome);
        }

        private int Score(int[] genomeToScore)
        {
            int distance = 0;
            for(int i = 1; i < CITIES_COUNT; i++) 
            {
                distance += distances[genomeToScore[i-1],genomeToScore[i]];
            }
            return distance;
        }


        private void PrintPreparedData()
        {
            System.Console.WriteLine("---------------------");
            System.Console.WriteLine("Distance matrix: ");
            for (int i = 0; i < CITIES_COUNT; i++)
            {
                for (int j = 0; j < CITIES_COUNT; j++)
                {
                    System.Console.Write(distances[i, j].ToString("D2") + " ");
                }
                System.Console.WriteLine();
            }

            System.Console.WriteLine("---------------------");
            System.Console.WriteLine("First genome: ");
            System.Console.WriteLine(string.Join("|", genome));
            System.Console.WriteLine("Genome score: "+ genomeScore);
            System.Console.WriteLine("---------------------");
        }

        private void PrintBestGenome()
        {
            System.Console.WriteLine("Best genome: ");
            System.Console.WriteLine(string.Join("|", genome));
            System.Console.WriteLine("Genome score: "+ genomeScore);
            System.Console.WriteLine("---------------------");
        }
        private static void PrintSummary(int mutationsCount)
        {
            Console.WriteLine("Total Iterations: " + mutationsCount);
            Console.WriteLine("Best after: " + (mutationsCount - MAX_UNIMPROVED_ITERATIONS));
            Console.WriteLine("--------------------------------------------------------------------------------");
        }
    }
}