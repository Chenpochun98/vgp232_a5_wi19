using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Assignment5.Data;

namespace Assignment5
{
    class Test
    {
        const string TEST_DATA_OUTPUT_FILE = "output.csv";
        const string TEST_DATA_INPUT_FILE = "pokemon151.xml";
        const string TES_POKEMONBAG_OUTPUT_FILLE = "pokemonBag.xml";

        string testOutputFile = "";
        string testbagOutputFile = "";
        string testInputFile = "";

        Pokedex pokedex = new Pokedex();
        PokemonReader pokemonReader = new PokemonReader();

        [Test]
        public void LoadTest()
        {
            try
            {
                pokedex = pokemonReader.Load(testInputFile);
            }
            catch
            {
                Assert.IsFalse(true);
            }
        }

   
        [Test]
        public void FindHighestDefense()
        {
            LoadTest();
            pokedex = pokedex.GetHighestDefense();
            Pokemon highestPokemon = pokedex.GetHighestDefense();

        }

        [Test]
        public void FindHighestHp()
        {
            LoadTest();
            pokedex.Sort(Pokemon.CompareByPokemonHP);
            Pokemon highestPokemon = pokedex.GetHighestHP();

        }

        [Test]
        public void FindHighestAttack()
        {
            LoadTest();
            pokedex.Sort(Pokemon.CompareByPokemonAttack);
            Pokemon highestPokemon = pokedex.GetHighestAttack();

        }

        [Test]
        public void FindHighestMaxCp()
        {
            LoadTest();
            pokedex.Sort(Pokemon.CompareByPokemonMaxCP);
            Pokemon highestPokemon = pokedex.GetHighestMaxCP();

        }

        [TestCase(151)]
        public void Load(int pokenum)
        {
            pokedex.Clear();
            LoadTest();
            Assert.AreEqual(pokenum, pokedex.Count);
        }

        [TestCase(151)]
        public void Save(int pokenum)
        {
            pokedex.Clear();
            LoadTest();
            pokedex.Save(testOutputFile, false);
            Assert.AreEqual(File.Exists(testOutputFile), true);
            Assert.AreEqual(pokenum, pokedex.Count);
        }
        
        [SetUp]
        public void Setup()
        {
            //string currentDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            string appDir = AppContext.BaseDirectory;
            testOutputFile = Path.Combine(appDir, TEST_DATA_OUTPUT_FILE);
            testInputFile = Path.Combine(appDir, TEST_DATA_INPUT_FILE);


            if (File.Exists(TEST_DATA_OUTPUT_FILE))
            {
                File.Delete(TEST_DATA_OUTPUT_FILE);
            }

            if (!File.Exists(TEST_DATA_INPUT_FILE))
            {
                using (var writer = new StreamWriter(TEST_DATA_INPUT_FILE))
                {
                    writer.WriteLine("Test,Test,Test");
                }
            }
        }


        [TearDown]
        public void TearDown()
        {
            if (File.Exists(TEST_DATA_OUTPUT_FILE))
            {
                File.Delete(TEST_DATA_OUTPUT_FILE);
            }
        }

    }
}
}
