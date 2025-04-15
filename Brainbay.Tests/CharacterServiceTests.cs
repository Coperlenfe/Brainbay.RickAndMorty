using NUnit.Framework;
using Brainbay.Infrastructure.Services;
using Brainbay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Brainbay.Tests
{
    public class CharacterServiceTests : IDisposable
    {
        private RickAndMortyDbContext _context;
        private CharacterService _service;

        public void Dispose()
        {
            _context?.Dispose();
        }
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RickAndMortyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new RickAndMortyDbContext(options);
            _service = new CharacterService(_context);
        }


        [Test]
        public async Task FetchAndSaveCharactersAsync_SetsLastSyncTime()
        {
            var options = new DbContextOptionsBuilder<RickAndMortyDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new RickAndMortyDbContext(options);
            var service = new CharacterService(context);

            await service.FetchAndSaveCharactersAsync();

            Assert.That(service.LastSyncTime, Is.Not.Null);
        }

        [Test]
        public async Task FetchAndSaveCharactersAsync_OnlySavesAliveCharacters()
        {
            var result = await _service.FetchAndSaveCharactersAsync();

            Assert.That(result, Is.All.Matches<Brainbay.Domain.Entities.Character>(c => c.Status == "Alive"));
            Assert.That(_context.Characters.Count(), Is.EqualTo(result.Count));
        }

        [Test]
        public async Task FetchAndSaveCharactersAsync_SecondCallWithin5Minutes_ReturnsEmpty()
        {
            var firstResult = await _service.FetchAndSaveCharactersAsync();

            var secondResult = await _service.FetchAndSaveCharactersAsync();

            Assert.That(firstResult.Count, Is.GreaterThan(0));
            Assert.That(secondResult, Is.Empty);
        }



        [Test]
        public async Task FetchAndSaveCharactersAsync_ClearsOldDataBeforeSavingNew()
        {
            _context.Characters.Add(new Brainbay.Domain.Entities.Character
            {
                Id = 1,
                Name = "Fake Character",
                Status = "Dead",
                Species = "Alien"
            });
            await _context.SaveChangesAsync();

            await _service.FetchAndSaveCharactersAsync();

            var allCharacters = _context.Characters.ToList();
            Assert.That(allCharacters.Any(c => c.Name == "Fake Character"), Is.False);
        }


        [Test]
        public async Task FetchAndSaveCharactersAsync_ReturnsNonEmptyList()
        {
            var result = await _service.FetchAndSaveCharactersAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        }



    }
}
