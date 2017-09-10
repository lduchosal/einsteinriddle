using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Genetic;

namespace EinsteinRiddle
{
    public class Solve
    {
        public static void Now()
        {

            int size = 3000;
            int generations = 1000;
            IFitnessFunction fitness = new EinsteinFitness();
            ISelectionMethod selection = new EliteSelection();
            IChromosome ancestor = new EinsteinChromosome();

            Population population = new Population(size, ancestor, fitness, selection);
            EinsteinChromosome solution = null;
            int i = 0;
            while (++i < generations)
            {
                population.RunEpoch();
                solution = (EinsteinChromosome)population.BestChromosome;
                Console.WriteLine("Population {0} = {1} ({2})", 
                    i,
                    solution.Fitness,
                    EinsteinFitness.QuickEvaluation(solution));

                if (solution.Fitness == 15)
                {
                    break;
                }
                // System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine();
            Console.WriteLine("Generation: {0}.", i);
            Console.WriteLine("Fitness: {0}/15", solution.Fitness);
            Console.WriteLine("Evaluation:");
            Console.WriteLine("{0}", EinsteinFitness.Evaluation(solution));

            foreach (Person person in solution.Persons)
            {
                Console.WriteLine("{0}. {1}, {2}, {3}, {4}, {5}",
                    person.Place,
                    person.Nationality,
                    person.Animal,
                    person.Color,
                    person.Drink,
                    person.Smoke);
            }


        }

    }

    /**
     *
     * int size = 300000;
     * int generations = 50;

Fitness: 15/15.
Premier. Norvegien, Oiseau, Verte, Cafe, PallMall
Deuxieme. Allemand, Poisson, Bleue, Eau, Malboro
Troisieme. Suedois, Chien, Blanche, Lait, Rothman
Quatrieme. Danois, Chat, Jaune, The, Dunhill
Cinquieme. Anglais, Cheval, Rouge, Biere, PhilipMorris

     * int size = 30000;
     * int generations = 100;
Fitness: 15/15.
Premier. Norvegien, Oiseau, Verte, Cafe, PallMall
Deuxieme. Allemand, Poisson, Bleue, Eau, Malboro
Troisieme. Anglais, Cheval, Rouge, Lait, Rothman
Quatrieme. Danois, Chat, Jaune, The, Dunhill
Cinquieme. Suedois, Chien, Blanche, Biere, PhilipMorris

     * int size = 30000;
     * int generations = 25;
Fitness: 15/15.
Premier. Norvegien, Oiseau, Verte, Cafe, PallMall
Deuxieme. Allemand, Poisson, Bleue, Eau, Malboro
Troisieme. Anglais, Cheval, Rouge, Lait, Rothman
Quatrieme. Danois, Chat, Jaune, The, Dunhill
Cinquieme. Suedois, Chien, Blanche, Biere, PhilipMorris

     * 
    **/
}
