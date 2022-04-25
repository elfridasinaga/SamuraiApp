// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

/*SamuraiContext _context = new SamuraiContext();
_context.Database.EnsureCreated();*/

//AddSamurai("Samurai 3", "Samurai 4", "Samurai 5", "Samurai 6");
//AddVariousTypes();
//QueryFilters();
//QueryAggregates();
//RetrieveAndUpdateSamurai();
//RetrieveAndUpdateMultipleSamurais();
//();
//RetrieveAndDeleteSamurai();
//QueryAndUpdateBattles_Disconnect();
//InsertNewSamuraiWithQuote();
//AddQuoteToExistingSamurai();
//AddQuoteToExistingSamuraiNoTracking(3);
//SimpleAddQuoteToExistingSamuraiNoTracking(4);
//EagerLoadSamuraiWithQuote();
//ProjectingSample();
//ExplicitLoadQuotes();
//FilteringWithRelatedData();
//ModifyRelatedData();
//ModifyRelatedDataNoTracking();
//AddNewSamuraiToBattle();
//BattleWithSamurai();
//AddAllSamuraiToAllBattle();
//RemoveSamuraiFromBattle();
//AddNewSamuraiWithHorse();
//AddNewHorseToSamurai();
//AddNewHorseNoTracking();
//GetSamuraiWithHorse();
//QueryViewBattleStat();
//QueryUsingRawSql();
//QueryUsingSPRaw();
//AddSamuraiByName("Mushahsi", "Takeda", "Nobunaga", "Iyeasu");
//AddSamurai("Shinobu");
//GetSamurais();
Console.WriteLine("Press any key");
Console.ReadLine();

/*void AddSamurai(params string[] names)
{
    //menambahkan banyak samurai
    foreach (string name in names)
    {
        _context.Samurais.Add(new Samurai { Name = name });
    }
    //var samurai = new Samurai { Name = name };
    //_context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void GetSamurais()
{
    var samurais = _context.Samurais
        .TagWith("Get Samurai Method")
        .ToList();
    Console.WriteLine($"Jumlah samurai adalah {samurais.Count}");
    foreach(var samurai in samurais)
    {
        Console.WriteLine(samurai.Name);
    }
}
void GetBattles()
{
    var battles = _context.Battles.AsNoTracking().ToList();
    foreach (var battle in battles)
    {
        Console.WriteLine($"{battle.BattleId}- {battle.Name}-{battle.StartDate}-{battle.EndDate}");
    }
}
void AddVariousTypes()
{
    _context.Samurais.AddRange(
        new Samurai { Name = "Kensin" },
        new Samurai { Name = "Shisio" }
        );
    _context.Battles.AddRange(
        new Battle { Name = "Battle of Anegawa" },
        new Battle { Name = "Battle of Nagashino" }
        );
    _context.SaveChanges();

    *//*_context.AddRange(new Samurai { Name = "Shinobu" }, new Samurai { Name = "Muichiro" },
        new Battle { Name = "Battle of District Arc" }, new Battle { Name = "Battle of Kyoto" });
    _context.SaveChanges();*//*
}
void QueryFilters()
{
    //var samurais = _context.Samurais.Where(s => s.Name == "Rengoku").ToList();
    var samurais = _context.Samurais.Where(s => s.Name.Contains("Tan")).ToList();
    //var samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "Tan%")).ToList();
    foreach (var samurai in samurais)
    {
        Console.WriteLine(samurai.Name);
    }
}
void QueryAggregates()
{
    var name = "Rengoku";
    //var samurai=_context.Samurais.FirstOrDefault(s=>s.Name==name);
    var samurai = (from s in _context.Samurais
                   where s.Name == name
                   select s).FirstOrDefault();
    //var samurai = _context.Samurais.Find(2);
    Console.WriteLine($"ID{samurai.Id}- Nama{samurai.Name}");
}
void RetrieveAndUpdateSamurai()
{
    var samurai = _context.Samurais.SingleOrDefault(s => s.Id == 2);
    samurai.Name = "Kyojiro Rengoku";
    _context.SaveChanges();

}
void RetrieveAndUpdateMultipleSamurais()
{
    var samurais = _context.Samurais.Skip(0).Take(4).ToList();
    samurais.ForEach(s => s.Name += " San");
    _context.SaveChanges();
}
void MultipleDatabaseOperations()
{
    var samurai = _context.Samurais.OrderBy(s => s.Id).LastOrDefault();
    samurai.Name += " San";
    _context.Samurais.Add(new Samurai { Name = "Gyomei Himejima" });
    _context.SaveChanges();
}
void RetrieveAndDeleteSamurai()
{
    //var samurai = _context.Samurais.Find(6);
    //_context.Samurais.Remove(samurai);
    var samurais = _context.Samurais.Where(s => s.Name.StartsWith("Samurai")).ToList();
    _context.Samurais.RemoveRange(samurais);
    _context.SaveChanges();
}
void QueryAndUpdateBattles_Disconnect()
{
    List<Battle> disconnectedBattles;
    using (var context1 = new SamuraiContext())
    {
        //disconnectedBattles = context1.Battles.AsNoTracking().ToList();
        disconnectedBattles = context1.Battles.ToList();
    }//context1 sudah dipose
    disconnectedBattles.ForEach(b =>
    {
        b.StartDate = new DateTime(1580, 11, 01);
        b.EndDate = new DateTime(1585, 05, 01);
    });

    using (var context2 = new SamuraiContext())
    {
        context2.UpdateRange(disconnectedBattles);
        context2.SaveChanges();
    }
}
void InsertNewSamuraiWithQuote()
{
    var samurai = new Samurai
    {
        Name = "Miyamoto Musashi",
        Quotes = new List<Quote>
        {
            new Quote {Text = "Think lightly of yourself and deeply world"},
            new Quote {Text = "Do nothing that is of no use"}
        }
    };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void AddQuoteToExistingSamurai()
{
    var samurai = _context.Samurais.Where(s => s.Id == 1).FirstOrDefault();
    samurai.Quotes.Add(new Quote
    {
        Text = "Do not fear death"
    });
    _context.SaveChanges();
}
void AddQuoteToExistingSamuraiNoTracking(int samuraiId)
{
    var samurai = _context.Samurais.Find(samuraiId);
    samurai.Quotes.Add(new Quote
    {
        Text = "Do not fear death"
    });

    using (var context1 = new SamuraiContext())
    {
        context1.Samurais.Attach(samurai);
        context1.SaveChanges();
    }
}
void SimpleAddQuoteToExistingSamuraiNoTracking(int samuraiId)
{
    var quote = new Quote
    {
        Text = "Never stray from the way",
        SamuraiId = samuraiId
    };

    using (var context1 = new SamuraiContext())
    {
        context1.Quotes.Add(quote);
        context1.SaveChanges();
    }
}
void EagerLoadSamuraiWithQuote()
{
    //var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
    //var splitquery = _context.Samurais.AsSplitQuery().Include(s => s.Quotes).ToList();
    //var filteredEntity = _context.Samurais.Include(s => s.Quotes.Where(
    //     q => q.Text.Contains("fear"))).ToList();
    *//*var filteredEntityInclude = _context.Samurais.Where(s => s.Name.Contains("San"))
        .Include(s => s.Quotes).ToList();*//*

    //var filterSingle = _context.Samurais.Where(s => s.Id == 1).Include(s => s.Quotes).FirstOrDefault();
}
void ProjectingSample()
{
    //var results = _context.Samurais.Select(s => new {s.Id, s.Name}).ToList();
    //var idandnameresult=_context.Samurais.Select(s=>new IdAndName(s.Id, s.Name)).ToList();
    *//*var resultsWithCount = _context.Samurais.Select(s =>
    new { s.Id, s.Name, NumOfQuote = s.Quotes.Count }).ToList();*//*

    var samuraiAndQuotes = _context.Samurais.Select(s => new
    {
        Samurai = s,
        BestQuote = s.Quotes.Where(q => q.Text.Contains("fear"))
    }).ToList();
}
void ExplicitLoadQuotes()
{
    *//*_context.Set<Horse>().Add(new Horse { SamuraiId = 1, Name = "Yamatomaru" });
    _context.SaveChanges();
    _context.ChangeTracker.Clear();*//*

    var samurai = _context.Samurais.Find(1);
    _context.Entry(samurai).Collection(s => s.Quotes).Load();
}
void FilteringWithRelatedData()
{
    var samurais = _context.Samurais.Where(s =>
    s.Quotes.Any(q => q.Text.Contains("fears"))).ToList();
}
void ModifyRelatedData()
{
    var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 21);
    samurai.Quotes[0].Text = "hello world";
    _context.Quotes.Remove(samurai.Quotes[1]);
    _context.SaveChanges();
}
void ModifyRelatedDataNoTracking()
{
    var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 21);
    var quote = samurai.Quotes[0];
    quote.Text = "hello ef core 6.0";

    using (var context1 = new SamuraiContext())
    {
        //context1.Quotes.Update(quote);
        context1.Entry(quote).State = EntityState.Modified;
        context1.SaveChanges();
    }
}
void AddNewSamuraiToBattle()
{
    var battle = _context.Battles.FirstOrDefault();
    battle.Samurais.Add(new Samurai { Name = "Nobunaga Oda" });
    _context.SaveChanges();
}
void BattleWithSamurai()
{
    //var battle = _context.Battles.Include(b => b.Samurais).FirstOrDefault(b => b.BattleId == 1);
    var battle = _context.Battles.Include(b => b.Samurais).ToList();
}
void AddAllSamuraiToAllBattle()
{
    var allbattles = _context.Battles.ToList();
    var allsamurais = _context.Samurais.ToList();
    foreach (var battle in allbattles)
    {
        battle.Samurais.AddRange(allsamurais);
    }
    _context.SaveChanges();
}
void RemoveSamuraiFromBattle()
{
    var battleWithSamurai = _context.Battles.Include(b => b.Samurais.Where(s => s.Id == 22))
        .SingleOrDefault(b => b.BattleId == 1);
    var samurai = battleWithSamurai.Samurais[0];
    battleWithSamurai.Samurais.Remove(samurai);
    _context.SaveChanges();
}
void AddNewSamuraiWithHorse()
{
    var samurai = new Samurai { Name = "Kensin Himura" };
    samurai.Horse = new Horse { Name = "Nekochan" };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void AddNewHorseToSamurai()
{
    var horse = new Horse { Name = "Red Hare", SamuraiId = 2 };
    _context.Add(horse);
    _context.SaveChanges();
}
void AddNewHorseNoTracking()
{
    var samurai = _context.Samurais.AsNoTracking().FirstOrDefault(s => s.Id == 3);
    samurai.Horse = new Horse { Name = "Silver Thunder" };

    using (var context1 = new SamuraiContext())
    {
        context1.Samurais.Attach(samurai);
        context1.SaveChanges();
    }
}
void GetSamuraiWithHorse()
{
    var samurais = _context.Samurais.Include(s => s.Horse).ToList();
    //var horseaja=_context.Set<Horse>().Find(2);
}
void QueryViewBattleStat()
{
    *//*var result = _context.SamuraiBattleStats.ToList();
    foreach (var stat in result)
    {
        Console.WriteLine($"{stat.Name}- {stat.NumberOfBattles}- {stat.EarliestBattle}");
    }*//*
    

    var firststat = _context.SamuraiBattleStats.FirstOrDefault(s => s.Name == "Kensin");
    Console.WriteLine($"{firststat.Name}-{firststat.NumberOfBattles}-{firststat.EarliestBattle}");
}
void QueryUsingRawSql()
{
    //var samurais = _context.Samurais.FromSqlRaw("select * from Samurais").ToList();
    var name = "Kensin";
    var samurais = _context.Samurais.FromSqlInterpolated($"select * from samurais where Name={name}").ToList();
}
void QueryUsingSPRaw()
{
    //var text = "fear";
    //var samurais = _context.Samurais.FromSqlRaw("exec dbo.SamuraiWhoSaidAWord {0}", text).ToList();
    //var samurais = _context.Samurais.FromSqlInterpolated($"exec dbo.SamuraiWhoSaidAWord {text}").ToList();
    var samuraiId = 16;
    //_context.Database.ExecuteSqlRaw("exec dbo.DeleteQuotesForSamurai {0}", samuraiId);
    _context.Database.ExecuteSqlInterpolated($"exec dbo.DeleteQuotesFromSamurai {samuraiId}");
    Console.WriteLine($"Data samurai id {samuraiId} berhasil didelete");
}
void AddSamuraiByName(params string[] names)
{
    var bs = new BusinessDataLogic();
    var newSamuraiCreateCount = bs.AddSamuraiByName(names);
}*/


struct IdAndName
{
    public IdAndName(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id;
    public string Name;
}
