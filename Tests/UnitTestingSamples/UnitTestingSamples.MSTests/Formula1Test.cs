using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace UnitTestingSamples.MSTests
{
    [TestClass]
    public class Formula1Test
    {
        internal static string Formula1SampleData()
        {
            return @"
<Racers>
  <Racer>
    <Firstname>Nino</Firstname>
    <Lastname>Farina</Lastname>
    <Country>Italy</Country>
    <Starts>33</Starts>
    <Wins>5</Wins>
  </Racer>
  <Racer>
    <Firstname>Alberto</Firstname>
    <Lastname>Ascari</Lastname>
    <Country>Italy</Country>
    <Starts>32</Starts>
    <Wins>10</Wins>
  </Racer>
  <Racer>
    <Firstname>Juan Manuel</Firstname>
    <Lastname>Fangio</Lastname>
    <Country>Argentina</Country>
    <Starts>51</Starts>
    <Wins>24</Wins>
  </Racer>
  <Racer>
    <Firstname>Mike</Firstname>
    <Lastname>Hawthorn</Lastname>
    <Country>UK</Country>
    <Starts>45</Starts>
    <Wins>3</Wins>
  </Racer>
  <Racer>
    <Firstname>Phil</Firstname>
    <Lastname>Hill</Lastname>
    <Country>USA</Country>
    <Starts>48</Starts>
    <Wins>3</Wins>
  </Racer>
  <Racer>
    <Firstname>John</Firstname>
    <Lastname>Surtees</Lastname>
    <Country>UK</Country>
    <Starts>111</Starts>
    <Wins>6</Wins>
  </Racer>
  <Racer>
    <Firstname>Jim</Firstname>
    <Lastname>Clark</Lastname>
    <Country>UK</Country>
    <Starts>72</Starts>
    <Wins>25</Wins>
  </Racer>
  <Racer>
    <Firstname>Jack</Firstname>
    <Lastname>Brabham</Lastname>
    <Country>Australia</Country>
    <Starts>125</Starts>
    <Wins>14</Wins>
  </Racer>
  <Racer>
    <Firstname>Denny</Firstname>
    <Lastname>Hulme</Lastname>
    <Country>New Zealand</Country>
    <Starts>112</Starts>
    <Wins>8</Wins>
  </Racer>
  <Racer>
    <Firstname>Graham</Firstname>
    <Lastname>Hill</Lastname>
    <Country>UK</Country>
    <Starts>176</Starts>
    <Wins>14</Wins>
  </Racer>
  <Racer>
    <Firstname>Jochen</Firstname>
    <Lastname>Rindt</Lastname>
    <Country>Austria</Country>
    <Starts>60</Starts>
    <Wins>6</Wins>
  </Racer>
  <Racer>
    <Firstname>Jackie</Firstname>
    <Lastname>Stewart</Lastname>
    <Country>UK</Country>
    <Starts>99</Starts>
    <Wins>27</Wins>
  </Racer>
  <Racer>
    <Firstname>Emerson</Firstname>
    <Lastname>Fittipaldi</Lastname>
    <Country>Brazil</Country>
    <Starts>143</Starts>
    <Wins>14</Wins>
  </Racer>
  <Racer>
    <Firstname>James</Firstname>
    <Lastname>Hunt</Lastname>
    <Country>UK</Country>
    <Starts>91</Starts>
    <Wins>10</Wins>
  </Racer>
  <Racer>
    <Firstname>Mario</Firstname>
    <Lastname>Andretti</Lastname>
    <Country>USA</Country>
    <Starts>128</Starts>
    <Wins>12</Wins>
  </Racer>
  <Racer>
    <Firstname>Jody</Firstname>
    <Lastname>Scheckter</Lastname>
    <Country>South Africa</Country>
    <Starts>112</Starts>
    <Wins>10</Wins>
  </Racer>
  <Racer>
    <Firstname>Alan</Firstname>
    <Lastname>Jones</Lastname>
    <Country>Australia</Country>
    <Starts>115</Starts>
    <Wins>12</Wins>
  </Racer>
  <Racer>
    <Firstname>Keke</Firstname>
    <Lastname>Rosberg</Lastname>
    <Country>Finland</Country>
    <Starts>114</Starts>
    <Wins>5</Wins>
  </Racer>
  <Racer>
    <Firstname>Niki</Firstname>
    <Lastname>Lauda</Lastname>
    <Country>Austria</Country>
    <Starts>170</Starts>
    <Wins>25</Wins>
  </Racer>
  <Racer>
    <Firstname>Nelson</Firstname>
    <Lastname>Piquet</Lastname>
    <Country>Brazil</Country>
    <Starts>204</Starts>
    <Wins>23</Wins>
  </Racer>
  <Racer>
    <Firstname>Ayrton</Firstname>
    <Lastname>Senna</Lastname>
    <Country>Brazil</Country>
    <Starts>161</Starts>
    <Wins>41</Wins>
  </Racer>
  <Racer>
    <Firstname>Nigel</Firstname>
    <Lastname>Mansell</Lastname>
    <Country>UK</Country>
    <Starts>187</Starts>
    <Wins>31</Wins>
  </Racer>
  <Racer>
    <Firstname>Alain</Firstname>
    <Lastname>Prost</Lastname>
    <Country>France</Country>
    <Starts>197</Starts>
    <Wins>51</Wins>
  </Racer>
  <Racer>
    <Firstname>Damon</Firstname>
    <Lastname>Hill</Lastname>
    <Country>UK</Country>
    <Starts>114</Starts>
    <Wins>22</Wins>
  </Racer>
  <Racer>
    <Firstname>Jacques</Firstname>
    <Lastname>Villeneuve</Lastname>
    <Country>Canada</Country>
    <Starts>165</Starts>
    <Wins>11</Wins>
  </Racer>
  <Racer>
    <Firstname>Mika</Firstname>
    <Lastname>Hakkinen</Lastname>
    <Country>Finland</Country>
    <Starts>160</Starts>
    <Wins>20</Wins>
  </Racer>
  <Racer>
    <Firstname>Michael</Firstname>
    <Lastname>Schumacher</Lastname>
    <Country>Germany</Country>
    <Starts>308</Starts>
    <Wins>91</Wins>
  </Racer>
  <Racer>
    <Firstname>Fernando</Firstname>
    <Lastname>Alonso</Lastname>
    <Country>Spain</Country>
    <Starts>248</Starts>
    <Wins>32</Wins>
  </Racer>
  <Racer>
    <Firstname>Kimi</Firstname>
    <Lastname>Raikkonen</Lastname>
    <Country>Finland</Country>
    <Starts>226</Starts>
    <Wins>20</Wins>
  </Racer>
</Racers>";
        }

        internal static XElement Formula1VerificationData()
        {
            return XElement.Parse(@"
<Racers>
  <Racer Name=""Mika Hakkinen"" Country=""Finland"" Wins=""20"" />
  <Racer Name=""Kimi Raikkonen"" Country=""Finland"" Wins=""20"" />
  <Racer Name=""Keke Rosberg"" Country=""Finland"" Wins=""5"" />
</Racers>");
        }

        public class F1TestLoader : IChampionsLoader
        {
            public XElement LoadChampions() => XElement.Parse(Formula1SampleData());
        }

        [TestMethod]
        public void ChampionsByCountryFilterFinland()
        {
            Formula1 f1 = new Formula1(new F1TestLoader());
            XElement actual = f1.ChampionsByCountry("Finland");
            Assert.AreEqual(Formula1VerificationData().ToString(),
              actual.ToString());
        }
    }
}
