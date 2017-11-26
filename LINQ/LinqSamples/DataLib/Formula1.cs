using System.Collections.Generic;

namespace DataLib
{
    public static class Formula1
    {
        private static List<Racer> s_racers;

        private static List<Racer> InitializeRacers() =>
            new List<Racer>
            {
                new Racer("Nino", "Farina", "Italy", 33, 5, new int[] { 1950 }, new string[] { "Alfa Romeo" }),
                new Racer("Alberto", "Ascari", "Italy", 32, 13, new int[] { 1952, 1953 }, new string[] { "Ferrari" }),
                new Racer("Juan Manuel", "Fangio", "Argentina", 51, 24, new int[] { 1951, 1954, 1955, 1956, 1957 }, new string[] { "Alfa Romeo", "Maserati", "Mercedes", "Ferrari" }),
                new Racer("Mike", "Hawthorn", "UK", 45, 3, new int[] { 1958 }, new string[] { "Ferrari" }),
                new Racer("Phil", "Hill", "USA", 48, 3, new int[] { 1961 }, new string[] { "Ferrari" }),
                new Racer("John", "Surtees", "UK", 111, 6, new int[] { 1964 }, new string[] { "Ferrari" }),
                new Racer("Jim", "Clark", "UK", 72, 25, new int[] { 1963, 1965 }, new string[] { "Lotus" }),
                new Racer("Jack", "Brabham", "Australia", 125, 14, new int[] { 1959, 1960, 1966 }, new string[] { "Cooper", "Brabham" }),
                new Racer("Denny", "Hulme", "New Zealand", 112, 8, new int[] { 1967 }, new string[] { "Brabham" }),
                new Racer("Graham", "Hill", "UK", 176, 14, new int[] { 1962, 1968 }, new string[] { "BRM", "Lotus" }),
                new Racer("Jochen", "Rindt", "Austria", 60, 6, new int[] { 1970 }, new string[] { "Lotus" }),
                new Racer("Jackie", "Stewart", "UK", 99, 27, new int[] { 1969, 1971, 1973 }, new string[] { "Matra", "Tyrrell" }),
                new Racer("Emerson", "Fittipaldi", "Brazil", 143, 14, new int[] { 1972, 1974 }, new string[] { "Lotus", "McLaren" }),
                new Racer("James", "Hunt", "UK", 91, 10, new int[] { 1976 }, new string[] { "McLaren" }),
                new Racer("Mario", "Andretti", "USA", 128, 12, new int[] { 1978 }, new string[] { "Lotus" }),
                new Racer("Jody", "Scheckter", "South Africa", 112, 10, new int[] { 1979 }, new string[] { "Ferrari" }),
                new Racer("Alan", "Jones", "Australia", 115, 12, new int[] { 1980 }, new string[] { "Williams" }),
                new Racer("Keke", "Rosberg", "Finland", 114, 5, new int[] { 1982 }, new string[] { "Williams" }),
                new Racer("Niki", "Lauda", "Austria", 173, 25, new int[] { 1975, 1977, 1984 }, new string[] { "Ferrari", "McLaren" }),
                new Racer("Nelson", "Piquet", "Brazil", 204, 23, new int[] { 1981, 1983, 1987 }, new string[] { "Brabham", "Williams" }),
                new Racer("Ayrton", "Senna", "Brazil", 161, 41, new int[] { 1988, 1990, 1991 }, new string[] { "McLaren" }),
                new Racer("Nigel", "Mansell", "UK", 187, 31, new int[] { 1992 }, new string[] { "Williams" }),
                new Racer("Alain", "Prost", "France", 197, 51, new int[] { 1985, 1986, 1989, 1993 }, new string[] { "McLaren", "Williams" }),
                new Racer("Damon", "Hill", "UK", 114, 22, new int[] { 1996 }, new string[] { "Williams" }),
                new Racer("Jacques", "Villeneuve", "Canada", 165, 11, new int[] { 1997 }, new string[] { "Williams" }),
                new Racer("Mika", "Hakkinen", "Finland", 160, 20, new int[] { 1998, 1999 }, new string[] { "McLaren" }),
                new Racer("Michael", "Schumacher", "Germany", 287, 91, new int[] { 1994, 1995, 2000, 2001, 2002, 2003, 2004 }, new string[] { "Benetton", "Ferrari" }),
                new Racer("Fernando", "Alonso", "Spain", 291, 32, new int[] { 2005, 2006 }, new string[] { "Renault" }),
                new Racer("Kimi", "Räikkönen", "Finland", 271, 20, new int[] { 2007 }, new string[] { "Ferrari" }),
                new Racer("Lewis", "Hamilton", "UK", 208, 62, new int[] { 2008, 2014, 2015, 2017 }, new string[] { "McLaren", "Mercedes" }),
                new Racer("Jenson", "Button", "UK", 306, 16, new int[] { 2009 }, new string[] { "Brawn GP" }),
                new Racer("Sebastian", "Vettel", "Germany", 198, 47, new int[] { 2010, 2011, 2012, 2013 }, new string[] { "Red Bull Racing" }),
                new Racer("Nico", "Rosberg", "Germany", 207, 24, new int[] { 2016 }, new string[] { "Mercedes" })
            };

        public static IList<Racer> GetChampions() => s_racers ?? (s_racers = InitializeRacers());

        private static List<Team> s_teams;
        public static IList<Team> GetConstructorChampions()         
        {
            if (s_teams == null)
            {
                s_teams = new List<Team>()
                {
                    new Team("Vanwall", 1958),
                    new Team("Cooper", 1959, 1960),
                    new Team("Ferrari", 1961, 1964, 1975, 1976, 1977, 1979, 1982, 1983, 1999, 2000, 2001, 2002, 2003, 2004, 2007, 2008),
                    new Team("BRM", 1962),
                    new Team("Lotus", 1963, 1965, 1968, 1970, 1972, 1973, 1978),
                    new Team("Brabham", 1966, 1967),
                    new Team("Matra", 1969),
                    new Team("Tyrrell", 1971),
                    new Team("McLaren", 1974, 1984, 1985, 1988, 1989, 1990, 1991, 1998),
                    new Team("Williams", 1980, 1981, 1986, 1987, 1992, 1993, 1994, 1996, 1997),
                    new Team("Benetton", 1995),
                    new Team("Renault", 2005, 2006 ),
                    new Team("Brawn GP", 2009),
                    new Team("Red Bull Racing", 2010, 2011, 2012, 2013),
                    new Team("Mercedes", 2014, 2015, 2016, 2017)
                };
            }
            return s_teams;
        }

        private static List<Championship> s_championships;
        public static IEnumerable<Championship> GetChampionships()
        {
            if (s_championships == null)
            {
                s_championships = new List<Championship>
                {
                    new Championship(1950, "Nino Farina", "Juan Manuel Fangio", "Luigi Fagioli"),
                    new Championship(1951, "Juan Manuel Fangio", "Alberto Ascari", "Froilan Gonzalez"),
                    new Championship(1952, "Alberto Ascari", "Nino Farina", "Piero Taruffi"),
                    new Championship(1953, "Alberto Ascari", "Juan Manuel Fangio", "Nino Farina"),
                    new Championship(1954, "Juan Manuel Fangio", "Froilan Gonzalez", "Mike Hawthorn"),
                    new Championship(1955, "Juan Manuel Fangio", "Stirling Moss", "Eugenio Castellotti"),
                    new Championship(1956, "Juan Manuel Fangio", "Stirling Moss", "Peter Collins"),
                    new Championship(1957, "Juan Manuel Fangio", "Stirling Moss", "Luigi Musso" ),
                    new Championship(1958, "Mike Hawthorn", "Stirling Moss", "Tony Brooks"),
                    new Championship(1959, "Jack Brabham", "Tony Brooks", "Stirling Moss"),
                    new Championship(1960, "Jack Brabham", "Bruce McLaren", "Stirling Moss"),
                    new Championship(1961, "Phil Hill", "Wolfgang von Trips", "Stirling Moss"),
                    new Championship(1962, "Graham Hill", "Jim Clark", "Bruce McLaren"),
                    new Championship(1963, "Jim Clark", "Graham Hill", "Richie Ginther"),
                    new Championship(1964, "John Surtees", "Graham Hill", "Jim Clark"),
                    new Championship(1965, "Jim Clark", "Graham Hill", "Jackie Stewart"),
                    new Championship(1966, "Jack Brabham", "John Surtees", "Jochen Rindt"),
                    new Championship(1967, "Denny Hulme", "Jack Brabham", "Jim Clark"),
                    new Championship(1968, "Graham Hill", "Jackie Stewart", "Denny Hulme"),
                    new Championship(1969, "Jackie Stewart", "Jackie Ickx", "Bruce McLaren"),
                    new Championship(1970, "Jochen Rindt", "Jackie Ickx", "Clay Regazzoni"),
                    new Championship(1971, "Jackie Stewart", "Ronnie Peterson", "Francois Cevert"),
                    new Championship(1972, "Emerson Fittipaldi", "Jackie Stewart", "Denny Hulme"),
                    new Championship(1973, "Jackie Stewart", "Emerson Fittipaldi", "Ronnie Peterson"),
                    new Championship(1974, "Emerson Fittipaldi", "Clay Regazzoni", "Jody Scheckter"),
                    new Championship(1975, "Niki Lauda", "Emerson Fittipaldi", "Carlos Reutemann"),
                    new Championship(1976, "James Hunt", "Niki Lauda", "Jody Scheckter"),
                    new Championship(1977, "Niki Lauda", "Jody Scheckter", "Mario Andretti"),
                    new Championship(1978, "Mario Andretti", "Ronnie Peterson", "Carlos Reutemann"),
                    new Championship(1979, "Jody Scheckter", "Gilles Villeneuve", "Alan Jones"),
                    new Championship(1980, "Alan Jones", "Nelson Piquet", "Carlos Reutemann"),
                    new Championship(1981, "Nelson Piquet", "Carlos Reutemann", "Alan Jones"),
                    new Championship(1982, "Keke Rosberg", "Didier Pironi", "John Watson"),
                    new Championship(1983, "Nelson Piquet", "Alain Prost", "Rene Arnoux"),
                    new Championship(1984, "Niki Lauda", "Alain Prost", "Elio de Angelis"),
                    new Championship(1985, "Alain Prost", "Michele Alboreto", "Keke Rosberg"),
                    new Championship(1986, "Alain Prost", "Nigel Mansell", "Nelson Piquet"),
                    new Championship(1987, "Nelson Piquet", "Nigel Mansell", "Ayrton Senna"),
                    new Championship(1988, "Ayrton Senna", "Alain Prost", "Gerhard Berger"),
                    new Championship(1989, "Alain Prost", "Ayrton Senna", "Riccardo Patrese"),
                    new Championship(1990, "Ayrton Senna", "Alain Prost", "Nelson Piquet"),
                    new Championship(1991, "Ayrton Senna", "Nigel Mansell", "Riccardo Patrese"),
                    new Championship(1992, "Nigel Mansell", "Riccardo Patrese", "Michael Schumacher"),
                    new Championship(1993, "Alain Prost", "Ayrton Senna", "Damon Hill"),
                    new Championship(1994, "Michael Schumacher", "Damon Hill", "Gerhard Berger"),
                    new Championship(1995, "Michael Schumacher", "Damon Hill", "David Coulthard"),
                    new Championship(1996, "Damon Hill", "Jacques Villeneuve", "Michael Schumacher"),
                    new Championship(1997, "Jacques Villeneuve", "Heinz-Harald Frentzen", "David Coulthard"),
                    new Championship(1998, "Mika Hakkinen", "Michael Schumacher", "David Coulthard"),
                    new Championship(1999, "Mika Hakkinen", "Eddie Irvine", "Heinz-Harald Frentzen"),
                    new Championship(2000, "Michael Schumacher", "Mika Hakkinen", "David Coulthard"),
                    new Championship(2001, "Michael Schumacher", "David Coulthard", "Rubens Barrichello"),
                    new Championship(2002, "Michael Schumacher", "Rubens Barrichello", "Juan Pablo Montoya"),
                    new Championship(2003, "Michael Schumacher", "Kimi Räikkönen", "Juan Pablo Montoya"),
                    new Championship(2004, "Michael Schumacher", "Rubens Barrichello", "Jenson Button"),
                    new Championship(2005, "Fernando Alonso", "Kimi Räikkönen", "Michael Schumacher"),
                    new Championship(2006, "Fernando Alonso", "Michael Schumacher", "Felipe Massa"),
                    new Championship(2007, "Kimi Räikkönen", "Lewis Hamilton", "Fernando Alonso"),
                    new Championship(2008, "Lewis Hamilton", "Felipe Massa", "Kimi Räikkönen"),
                    new Championship(2009, "Jenson Button", "Sebastian Vettel", "Rubens Barrichello"),
                    new Championship(2010, "Sebastian Vettel", "Fernando Alonso", "Mark Webber"),
                    new Championship(2011, "Sebastian Vettel", "Jenson Button", "Mark Webber"),
                    new Championship(2012, "Sebastian Vettel", "Fernando Alonso", "Kimi Räikkönen"),
                    new Championship(2013, "Sebastian Vettel", "Fernando Alonso", "Mark Webber"),
                    new Championship(2014, "Lewis Hamilton", "Nico Rosberg", "Daniel Ricciardo"),
                    new Championship(2015, "Lewis Hamilton", "Nico Rosberg", "Sebastian Vettel"),
                    new Championship(2016, "Nico Rosberg", "Lewis Hamilton", "Daniel Ricciardo"),
                    new Championship(2017, "Lewis Hamilton", "Sebastian Vettel", "Valtteri Bottas")
                };
            }
            return s_championships;
        }

        private static IList<Racer> _moreRacers;
        private static IList<Racer> GetMoreRacers()
        {
            if (_moreRacers == null)
            {
                _moreRacers = new List<Racer>();
                _moreRacers.Add(new Racer("Luigi", "Fagioli", "Italy", starts: 7, wins: 1));
                _moreRacers.Add(new Racer("Jose Froilan", "Gonzalez", "Argentina", starts: 26, wins: 2));
                _moreRacers.Add(new Racer("Piero", "Taruffi", "Italy", starts: 18, wins: 1));
                _moreRacers.Add(new Racer("Stirling", "Moss", "UK", starts: 66, wins: 16));
                _moreRacers.Add(new Racer("Eugenio", "Castellotti", "Italy", starts: 14, wins: 0));
                _moreRacers.Add(new Racer("Peter", "Collins", "UK", starts: 32, wins: 3));
                _moreRacers.Add(new Racer("Luigi", "Musso", "Italy", starts: 24, wins: 1));
                _moreRacers.Add(new Racer("Tony", "Brooks", "UK", starts: 38, wins: 6));
                _moreRacers.Add(new Racer("Bruce", "McLaren", "New Zealand", starts: 100, wins: 4));
                _moreRacers.Add(new Racer("Wolfgang von", "Trips", "Germany", starts: 27, wins: 2));
                _moreRacers.Add(new Racer("Richie", "Ginther", "USA", starts: 52, wins: 1));
                _moreRacers.Add(new Racer("Jackie", "Ickx", "Belgium", starts: 116, wins: 8));
                _moreRacers.Add(new Racer("Clay", "Regazzoni", "Switzerland", starts: 132, wins: 5));
                _moreRacers.Add(new Racer("Ronnie", "Peterson", "Sweden", starts: 123, wins: 10));
                _moreRacers.Add(new Racer("Francois", "Cevert", "France", starts: 46, wins: 1));
                _moreRacers.Add(new Racer("Carlos", "Reutemann", "Argentina", starts: 146, wins: 12));
                _moreRacers.Add(new Racer("Gilles", "Villeneuve", "Canada", starts: 67, wins: 6));
                _moreRacers.Add(new Racer("Didier", "Pironi", "France", starts: 70, wins: 3));
                _moreRacers.Add(new Racer("John", "Watson", "UK", starts: 152, wins: 5));
                _moreRacers.Add(new Racer("Rene", "Arnoux", "France", starts: 149, wins: 7));
                _moreRacers.Add(new Racer("Elio", "de Angelis", "Italy", starts: 108, wins: 2));
                _moreRacers.Add(new Racer("Michele", "Alboreto", "Italy", starts: 194, wins: 5));
                _moreRacers.Add(new Racer("Gerhard", "Berger", "Austria", starts: 210, wins: 10));
                _moreRacers.Add(new Racer("Riccardo", "Patrese", "Italy", starts: 256, wins: 6));
                _moreRacers.Add(new Racer("David", "Coulthard", "UK", starts: 246, wins: 13));
                _moreRacers.Add(new Racer("Heinz-Harald", "Frentzen", "Germany", starts: 156, wins: 3));
                _moreRacers.Add(new Racer("Eddie", "Irvine", "UK", starts: 147, wins: 4));
                _moreRacers.Add(new Racer("Rubens", "Barrichello", "Brazil", starts: 322, wins: 11));
                _moreRacers.Add(new Racer("Juan Pablo", "Montoya", "Columbia", starts: 94, wins: 7));
                _moreRacers.Add(new Racer("Felipe", "Massa", "Brazil", starts: 269, wins: 11));
                _moreRacers.Add(new Racer("Mark", "Webber", "Australia", starts: 215, wins: 9));
                _moreRacers.Add(new Racer("Daniel", "Ricciardo", "Australia", starts: 129, wins: 5));
                _moreRacers.Add(new Racer("Valtteri", "Bottas", "Finland", starts: 97, wins: 3));
            }
            return _moreRacers;
        }
    }
}