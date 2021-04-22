using swplus.api.Models;
using System;
using System.Collections.Generic;

namespace swplus.api.Db
{
    public interface ICharacterRepository
    {
        Character Add(Character item);
        void Delete(Guid id);
        Character Edit(Guid id, Character edited);
        List<Character> GetAll();
        Character GetById(Guid id);
    }
}