using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Genetic;

namespace EinsteinRiddle {

    /// <summary>
    /// Énigme posée par Einstein lui-même
    /// 
    /// 5 personnes de nationalité différente 
    /// habitent 5 maisons de couleurs distinctes 
    /// fument des cigares de 5 marques différentes 
    /// boivent 5 boissons distinctes 
    /// élèvent des animaux de 5 espèces différentes 
    /// Question de l'énigme d'Einstein
    /// 
    /// 
    /// Qui a des poissons ? 
    /// Hypothèses de l'énigme d'Einstein :
    /// 
    ///  1 -  Le norvégien habite la première maison.
    ///  2 -  L'Anglais habite la maison rouge.
    ///  3 -  La maison verte est située juste  à gauche de la maison blanche
    ///  4 -  Le Danois boit du thé.
    ///  5 -  Celui qui fume des Rothmans habite à côté de celui qui élève des chats.
    ///  6 -  Celui qui habite la maison jaune fume des Dunhill.
    ///  7 -  L'Allemand fume des Malboro. 
    ///  8 -  Celui qui habite la maison du milieu boit du lait.
    ///  9 -  Celui qui fume des Rothmans a un voisin qui boit de l'eau.
    /// 10 - Celui qui fume des Pall Mall élève des oiseaux.
    /// 11 - Le Suédois élève des chiens.
    /// 12 - Le Norvégien habite à côté de la maison bleue.
    /// 13 - Celui qui élève des chevaux habite à côté de la maison jaune.
    /// 14 - Celui qui fume des Philip Morris boit de la bière.
    /// 15 - Le propriétaire de la maison verte boit du café
    /// 
    /// Une seule réponse est possible...
    /// </summary>
    public class EinsteinFitness : IFitnessFunction {

        public delegate bool EinsteinRule(EinsteinChromosome chromosome);
        public static EinsteinRule[] EinsteinRules = new EinsteinRule[] { 
            NorvegienPremier,
            AnglaisRouge,
            VerteGaucheBlanche,
            DanoisThe,
            RothmanCoteChat,
            JauneDunhill,
            AllemandMalboro,
            MilieuLait,
            RothmanCoteEau,
            PallmallOiseaux,
            SuedoisChien,
            NorvegienCoteBleue,
            ChevalCoteJaune,
            PhillipMorisBiere,
            VertCafe
        };

        public static int Evaluate(EinsteinChromosome chromosome)
        {
            int fitness = 0;
            // chromosome.Evaluation.Length = 0;
            foreach (EinsteinRule rule in EinsteinRules)
            {
                int evaluation = (rule(chromosome) ? 1 : 0);
                // chromosome.Evaluation.AppendFormat("{0}: {1}{2}", rule.Method.Name, evaluation, Environment.NewLine);
                fitness += evaluation;
            }
            return fitness;
        }
        
        public static string Evaluation(EinsteinChromosome chromosome)
        {
            int fitness = 0;
            StringBuilder sb = new StringBuilder();
            foreach (EinsteinRule rule in EinsteinRules)
            {
                int evaluation = (rule(chromosome) ? 1 : 0);
                sb.AppendFormat("{0}: {1}{2}", rule.Method.Name, evaluation, Environment.NewLine);
                fitness += evaluation;
            }
            return sb.ToString() ;
        }
        public static string QuickEvaluation(EinsteinChromosome chromosome)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (EinsteinRule rule in EinsteinRules)
            {
                sb.AppendFormat((rule(chromosome) ? "1" : "0"));
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// 1 -  Le norvégien habite la première maison.
        public static bool NorvegienPremier(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => (
                    person.Nationality == Nationality.Norvegien
                    && person.Place == Place.Premier));

            return persons.Length == 1;
        }


        ///  2 -  L'Anglais habite la maison rouge.
        public static bool AnglaisRouge(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => (
                    person.Nationality == Nationality.Anglais
                    && person.Color == Color.Rouge));

            return persons.Length == 1;
        }

        /// 3 -  La maison verte est située juste  à gauche de la maison blanche
        public static bool VerteGaucheBlanche(EinsteinChromosome chromosome)
        {
            Person verte = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Color == Color.Verte));

            Person blanche = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Color == Color.Blanche));

            return (verte.Place < blanche.Place);

        }


        /// 4 - Le Danois boit du thé.
        public static bool DanoisThe(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => (
                    person.Nationality == Nationality.Danois
                    && person.Drink == Drink.The));

            return persons.Length == 1;
        }


        /// 5 -  Celui qui fume des Rothmans habite à côté de celui qui élève des chats.
        public static bool RothmanCoteChat(EinsteinChromosome chromosome)
        {
            Person rothmans = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Smoke == Smoke.Rothman));

            Person chat = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Animal == Animal.Chat));

            return (Math.Abs(rothmans.Place - chat.Place) == 1);
        }

        /// 6 -  Celui qui habite la maison jaune fume des Dunhill.
        public static bool JauneDunhill(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => (person.Smoke == Smoke.Dunhill
                    && person.Color == Color.Jaune));

            return (persons.Length == 1);
        }

        ///  7 -  L'Allemand fume des Malboro. 
        public static bool AllemandMalboro(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => (person.Nationality == Nationality.Allemand
                    && person.Smoke == Smoke.Malboro));

            return (persons.Length == 1);
        }
        
        ///  8 -  Celui qui habite la maison du milieu boit du lait.
        public static bool MilieuLait(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => person.Place == Place.Troisieme
                    && person.Drink == Drink.Lait);

            return (persons.Length == 1);
        }

        ///  9 -  Celui qui fume des Rothmans a un voisin qui boit de l'eau.
        public static bool RothmanCoteEau(EinsteinChromosome chromosome)
        {
            Person rothmans = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Smoke == Smoke.Rothman));

            Person eau = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Drink == Drink.Eau));

            return (Math.Abs(rothmans.Place - eau.Place) == 1);
        }

        /// 10 - Celui qui fume des Pall Mall élève des oiseaux.
        public static bool PallmallOiseaux(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => (person.Smoke == Smoke.PallMall
                    && person.Animal == Animal.Oiseau));

            return (persons.Length == 1);
        }

        /// 11 - Le Suédois élève des chiens.
        public static bool SuedoisChien(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => (person.Nationality == Nationality.Suedois
                    && person.Animal == Animal.Chien));

            return (persons.Length == 1);
        }

        /// 12 - Le Norvégien habite à côté de la maison bleue.
        public static bool NorvegienCoteBleue(EinsteinChromosome chromosome)
        {
            Person norvegien = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Nationality == Nationality.Norvegien));

            Person bleue = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Color == Color.Bleue));

            return (Math.Abs(norvegien.Place - bleue.Place) == 1);
        }


        /// 13 - Celui qui élève des chevaux habite à côté de la maison jaune.
        public static bool ChevalCoteJaune(EinsteinChromosome chromosome)
        {
            Person cheval = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Animal == Animal.Cheval));

            Person jaune = Array.Find<Person>(chromosome.Persons.ToArray(),
                person => (person.Color == Color.Jaune));

            return (Math.Abs(cheval.Place - jaune.Place) == 1);
        }

        /// 14 - Celui qui fume des Philip Morris boit de la bière.
        public static bool PhillipMorisBiere(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => (person.Smoke == Smoke.PhilipMorris
                    && person.Drink == Drink.Biere));

            return (persons.Length == 1);
        }

        /// 15 - Le propriétaire de la maison verte boit du café
        public static bool VertCafe(EinsteinChromosome chromosome)
        {
            Person[] persons = Array.FindAll<Person>(chromosome.Persons.ToArray(),
                person => (person.Color == Color.Verte
                    && person.Drink == Drink.Cafe));

            return (persons.Length == 1);
        }

        #region IFitnessFunction Members

        public double Evaluate(IChromosome chromosome)
        {
            return EinsteinFitness.Evaluate((EinsteinChromosome)chromosome);
        }

        public object Translate(IChromosome chromosome)
        {
            Person poisson = Array.Find<Person>(
                ((EinsteinChromosome)chromosome).Persons.ToArray(),
                person => (person.Animal == Animal.Poisson));

            return poisson;
        }

        #endregion
    }

}
