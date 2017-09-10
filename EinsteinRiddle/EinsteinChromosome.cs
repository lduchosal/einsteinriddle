using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Genetic;



namespace EinsteinRiddle {

    public class EinsteinChromosome : IChromosome {

        public List<Person> Persons = new List<Person>();
        // public StringBuilder Evaluation = new StringBuilder();

        private double _fitness;
        //private IFitnessFunction _fitnessFunction;

        #region IChromosome Members

        public IChromosome Clone() {
            EinsteinChromosome chromosome = new EinsteinChromosome();
            List<Person> ps = new List<Person>();
            foreach (Person person in this.Persons)
            {
                Person copy = new Person();
                copy.Animal = person.Animal;
                copy.Color = person.Color;
                copy.Drink = person.Drink;
                copy.Nationality = person.Nationality;
                copy.Place = person.Place;
                copy.Smoke = person.Smoke;
                ps.Add(copy);
            }
            chromosome.Persons = ps;
            chromosome._fitness = this._fitness;
            return chromosome;

        }


        public IChromosome CreateOffspring() {
            return new EinsteinChromosome();
        }

        public EinsteinChromosome()
        {
            EinsteinChromosome.InitGene(this);
        }

        private static void InitGene(EinsteinChromosome chromosome) {
            
            List<Animal> animals = EnumHelper<Animal>.List();
            List<Drink> drinks = EnumHelper<Drink>.List();
            List<Color> colors = EnumHelper<Color>.List();
            List<Smoke> smokes = EnumHelper<Smoke>.List();
            List<Nationality> nationalities = EnumHelper<Nationality>.List();

            for (int i = 1; i <= 5; i++)
            {
                Person person = new Person();
                person.Place = (Place)i;

                person.Animal = EnumHelper<Animal>.Random(animals);
                person.Drink = EnumHelper<Drink>.Random(drinks);
                person.Color = EnumHelper<Color>.Random(colors);
                person.Smoke = EnumHelper<Smoke>.Random(smokes);
                person.Nationality = EnumHelper<Nationality>.Random(nationalities);

                chromosome.Persons.Add(person);
            }

        }



        public void Crossover(IChromosome obj) {

            EinsteinChromosome pair = (EinsteinChromosome)obj;
            int j = RandomHelper.Random.Next(5);

            for (int i=0; i<this.Persons.Count; i++)
            {
                switch (j)
                {
                    case 0:
                        Animal localAnimal = this.Persons[i].Animal;
                        Animal remoteAnimal = pair.Persons[i].Animal;
                        this.Persons[i].Animal = remoteAnimal;
                        pair.Persons[i].Animal = localAnimal;
                        break;
                    case 1:
                        Drink localDrink = this.Persons[i].Drink;
                        Drink remoteDrink = pair.Persons[i].Drink;
                        this.Persons[i].Drink = remoteDrink;
                        pair.Persons[i].Drink = localDrink;
                        break;
                    case 2:
                        Color localColor = this.Persons[i].Color;
                        Color remoteColor = pair.Persons[i].Color;
                        this.Persons[i].Color = remoteColor;
                        pair.Persons[i].Color = localColor;
                        break;
                    case 3:
                        Smoke localSmoke = this.Persons[i].Smoke;
                        Smoke remoteSmoke = pair.Persons[i].Smoke;
                        this.Persons[i].Smoke = remoteSmoke;
                        pair.Persons[i].Smoke = localSmoke;
                        break;
                    case 4:
                        Nationality localNationality = this.Persons[i].Nationality;
                        Nationality remoteNationality = pair.Persons[i].Nationality;
                        this.Persons[i].Nationality = remoteNationality;
                        pair.Persons[i].Nationality = localNationality;
                        break;
                }

            }

        }

        public void Evaluate(IFitnessFunction function) {
            this._fitness = function.Evaluate(this);
            //this._fitnessFunction = function;
        }

        public double Fitness {
            get { return this._fitness; }
        }

        public void Generate() {
            throw new NotImplementedException();
        }

        public void Mutate() {

            List<Animal> animals = EnumHelper<Animal>.List();
            List<Drink> drinks = EnumHelper<Drink>.List();
            List<Color> colors = EnumHelper<Color>.List();
            List<Smoke> smokes = EnumHelper<Smoke>.List();
            List<Nationality> nationalities = EnumHelper<Nationality>.List();

            int j = RandomHelper.Random.Next(5);

            foreach (Person person in this.Persons)
            {
                switch (j)
                {
                    case 0:
                        person.Animal = EnumHelper<Animal>.Random(animals);
                        break;
                    case 1:
                        person.Drink = EnumHelper<Drink>.Random(drinks);
                        break;
                    case 2:
                        person.Color = EnumHelper<Color>.Random(colors);
                        break;
                    case 3:
                        person.Smoke = EnumHelper<Smoke>.Random(smokes);
                        break;
                    case 4:
                        person.Nationality = EnumHelper<Nationality>.Random(nationalities);
                        break;
                }

            }
        }


        #endregion

        #region IComparable Members

        public int CompareTo(object obj) {
            EinsteinChromosome chromosome = (EinsteinChromosome)obj;
            return chromosome.Fitness.CompareTo(this.Fitness);
        }

        #endregion
    }

}
