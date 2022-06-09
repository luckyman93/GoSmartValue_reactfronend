using System;

namespace AV.Persistence.Exceptions
{
    public static class EntityNotFoundHandler
    {
        public static T Handle<T>(Guid id, T entity) where T : class
        {
            if (entity == null)
            {
                throw new EntityNotFoundException($"Entity with id of '{id}' was not found.");
            }

            return entity;
        }
    }
}
