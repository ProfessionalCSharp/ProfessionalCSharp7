using System.Collections.Generic;

namespace LiveShaping
{
    public class Formula1
    {
        private List<Racer> _racers;
        public IEnumerable<Racer> Racers => _racers ?? (_racers = GetRacers());

        private List<Racer> GetRacers()
        {
            return new List<Racer>()
              {
                new Racer { Name="Sebastian Vettel", Team="Red Bull Racing", Number=1 },
                new Racer { Name="Mark Webber", Team="Red Bull Racing", Number=2 },
                new Racer { Name="Jenson Button", Team="McLaren", Number=3 },
                new Racer { Name="Lewis Hamilton", Team="McLaren", Number=4 },
                new Racer { Name="Fernando Alonso", Team="Ferrari", Number=5 },
                new Racer { Name="Felipe Massa", Team="Ferrari", Number=6 },
                new Racer { Name="Michael Schumacher", Team="Mercedes", Number=7 },
                new Racer { Name="Nico Rosberg", Team="Mercedes", Number=8 },
                new Racer { Name="Kimi Raikkonen", Team="Lotus", Number=9 },
                new Racer { Name="Romain Grosjean", Team="Lotus", Number=10 },
                new Racer { Name="Paul di Resta", Team="Force India", Number=11 },
                new Racer { Name="Nico Hülkenberg", Team="Force India", Number=12 },
                new Racer { Name="Kamui Kobayashi", Team="Sauber", Number=14 },
                new Racer { Name="Sergio Perez", Team="Sauber", Number=15 },
                new Racer { Name="Daniel Riccardio", Team="Toro Rosso", Number=16 },
                new Racer { Name="Jean-Eric Vergne", Team="Toro Rosso", Number=17 },
                new Racer { Name="Pastor Maldonado", Team="Williams", Number=18 },
                new Racer { Name="Bruno Senna", Team="Williams", Number=19 },
                new Racer { Name="Heikki Kovalainen", Team="Caterham", Number=20 },
                new Racer { Name="Witali Petrow", Team="Caterham", Number=21 },
                new Racer { Name="Pedro de la Rosa", Team="HRT", Number=22 },
                new Racer { Name="Narain Karthikeyan", Team="HRT", Number=23 },
                new Racer { Name="Timo Glock", Team="Marussia", Number=24 },
                new Racer { Name="Charles Pic", Team="Marussia", Number=25 }
              };
        }
    }
}
