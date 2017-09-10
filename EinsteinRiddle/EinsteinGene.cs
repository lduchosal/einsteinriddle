using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EinsteinRiddle
{

    public class Person {

        public Drink Drink;
        public Smoke Smoke;
        public Animal Animal;
        public Color Color;
        public Place Place;
        public Nationality Nationality;

    }

    public enum Drink
    {
        The,
        Lait,
        Eau,
        Biere,
        Cafe
    }

    public enum Smoke
    {
        Malboro,
        PallMall,
        Rothman,
        PhilipMorris,
        Dunhill
    }

    public enum Animal
    {
        Chat,
        Oiseau,
        Chien,
        Cheval,
        Poisson
    }

    public enum Nationality
    {
        Anglais,
        Danois,
        Allemand,
        Suedois,
        Norvegien
    }

    public enum Place : int
    {
        Premier = 1,
        Deuxieme,
        Troisieme,
        Quatrieme,
        Cinquieme
    }

    public enum Color
    {
        Bleue,
        Verte,
        Jaune,
        Rouge,
        Blanche
    }

}
