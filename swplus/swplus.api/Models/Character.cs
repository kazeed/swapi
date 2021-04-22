using System;

namespace swplus.api.Models
{
    public class Character
    {
        public Character(string name, int height, string homeworld, string gender, string specie)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            if (string.IsNullOrEmpty(homeworld))
            {
                throw new ArgumentException($"'{nameof(homeworld)}' cannot be null or empty.", nameof(homeworld));
            }

            if (string.IsNullOrEmpty(gender))
            {
                throw new ArgumentException($"'{nameof(gender)}' cannot be null or empty.", nameof(gender));
            }

            if (string.IsNullOrEmpty(specie))
            {
                throw new ArgumentException($"'{nameof(specie)}' cannot be null or empty.", nameof(specie));
            }

            Name = name;
            Height = height;
            Homeworld = homeworld;
            Gender = gender;
            Specie = specie;

            // Auto props
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
            Edited = DateTime.UtcNow;
        }

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
