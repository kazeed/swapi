using System;

namespace swplus.api.Models
{
    public class Character
    {
        public Character(Character basis, Character other)
        {
            Id = basis.Id;
            Name = other.Name;
            Height = other.Height;
            Homeworld = other.Homeworld;
            Gender = other.Gender;
            Specie = other.Specie;
            Created = basis.Created;
            Edited = DateTime.UtcNow;
        }

        public Character() { }

        public Guid Id { get; private set; }

        public string Name { get; set; }
        public int Height { get; set; }
        public string Homeworld { get; set; }
        public string Gender { get; set; }
        public string Specie { get; set; }
        public DateTime Created { get; private set; }
        public DateTime Edited { get; private set; }
    }
}
