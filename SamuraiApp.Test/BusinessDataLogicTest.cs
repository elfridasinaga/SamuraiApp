using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Test
{
    [TestClass]
    public class BussinessDataLogicTest
    {
        [TestMethod]
        public void CanAddSamuraiByName()
        {
            var builder = new DbContextOptionsBuilder<SamuraiContext>();
            builder.UseInMemoryDatabase("CanAddSamuraiByName");

            using (var context = new SamuraiContext(builder.Options))
            {
                var bs = new BusinessDataLogic(context);
                int expected = 3;
                var namelist = new string[] { "Nobunaga", "Takeda", "Iyeasu" };
                var result = bs.AddSamuraiByName(namelist);
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void CanInsertSingleSamurai()
        {
            var builder = new DbContextOptionsBuilder<SamuraiContext>();
            builder.UseInMemoryDatabase("CanInsertSingleSamurai");
            using (var context = new SamuraiContext(builder.Options))
            {
                int expected = 1;
                var bs = new BusinessDataLogic(context);
                bs.InsertNewSamurai(new Samurai { Name = "Tanjiro" });
                Assert.AreEqual(expected, context.Samurais.Count());
            }
        }

        [TestMethod]
        public void CanInsertWithQuotes()
        {
            var expected = 2;
            var newSamurai = new Samurai
            {

                Name = "Tanjiro",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "you only live once"},
                    new Quote {Text = "sword is yout friend"}
                }
            };
            var builder = new DbContextOptionsBuilder<SamuraiContext>();
            builder.UseInMemoryDatabase("SamuraiWithQuotes");
            using (var context = new SamuraiContext(builder.Options))
            {
                var bs = new BusinessDataLogic(context);
                var result = bs.InsertNewSamurai(newSamurai);
            }
            using (var context2 = new SamuraiContext(builder.Options))
            {
                var samuraiWithQuotes = context2.Samurais.Include(s => s.Quotes).FirstOrDefault();
                Assert.AreEqual(expected, samuraiWithQuotes.Quotes.Count);
            }
        }

        [TestMethod, TestCategory("SamuraiWithQuotes")]
        public void CanGetSamuraiWithQuotes()
        {
            int expected = 3;
            int samuraiId;
            var builder = new DbContextOptionsBuilder<SamuraiContext>();
            builder.UseInMemoryDatabase("SamuraiGetQuotes");
            using (var context = new SamuraiContext(builder.Options))
            {
                var newSamurai = new Samurai
                {
                    Name = "Tanjiro",
                    Quotes = new List<Quote>
                    {
                        new Quote { Text = "you only live once" },
                        new Quote { Text = "sword is yout friend" },
                        new Quote { Text="fear is your enemy"}
                    }
                };
                context.Samurais.Add(newSamurai);
                context.SaveChanges();
                samuraiId = newSamurai.Id;
            }
            using (var context2 = new SamuraiContext(builder.Options))
            {
                var bs = new BusinessDataLogic(context2);
                var result = bs.GetSamuraiWithQuotes(samuraiId);
                Assert.AreEqual(expected, result.Quotes.Count);
            }
        }
    }
}
