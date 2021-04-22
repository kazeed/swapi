using LiteDB;
using swplus.api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace swplus.api.Db
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly string dbPath;

        public CharacterRepository(string dbDir)
        {
            if (string.IsNullOrEmpty(dbDir))
            {
                throw new ArgumentException($"'{nameof(dbDir)}' cannot be null or empty.", nameof(dbDir));
            }

            if (!Directory.Exists(dbDir)) Directory.CreateDirectory(dbDir);

            this.dbPath = Path.Combine(dbDir, "swapi.db");
        }

        public Character Add(Character item)
        {
            using var db = new LiteDatabase(dbPath);
            var col = db.GetCollection<Character>(nameof(Character));
            var existing = col.Query().Where(i => i.Name.Equals(item.Name) && i.Homeworld.Equals(item.Homeworld) && i.Gender.Equals(item.Gender) && i.Specie.Equals(item.Specie)).FirstOrDefault();
            if (existing != null)
            {
                return existing; // idempotency;
            }
            else
            {
                col.Insert(item.Id, item);
                return item;
            }
        }

        public Character GetById(Guid id)
        {
            using var db = new LiteDatabase(dbPath);
            var col = db.GetCollection<Character>(nameof(Character));
            return col.FindById(id);
        }

        public List<Character> GetAll()
        {
            using var db = new LiteDatabase(dbPath);
            var col = db.GetCollection<Character>(nameof(Character));
            var results = col.FindAll();

            return results.ToList();
        }

        public Character Edit(Guid id, Character edited)
        {
            if (edited is null)
            {
                throw new ArgumentNullException(nameof(edited));
            }

            var existing = this.GetById(id) ?? throw new Exception($"Cannot find item {id}");
            var modified = new Character(existing, edited);
            using var db = new LiteDatabase(dbPath);
            var col = db.GetCollection<Character>(nameof(Character));
            
            col.Update(id, modified);

            return modified;
        }

        public void Delete(Guid id)
        {
            var existing = this.GetById(id) ?? throw new Exception($"Cannot find item {id}");
            using var db = new LiteDatabase(dbPath);
            var col = db.GetCollection<Character>(nameof(Character));
            col.Delete(id);
        }
    }
}
