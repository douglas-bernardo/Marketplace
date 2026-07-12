namespace Marketplace.Framework
{
    public interface IEntityStore
    {
        /// <summary>
        /// Loads an entity by id
        /// </summary>
        Task<T> Load<T>(string entityId) where T : IInternalEventHandler;
        /// <summary>
        /// Persists an entity
        /// </summary>
        Task Save<T>(T entity) where T : IInternalEventHandler;
        /// <summary>
        /// Check if entity with a given id already exists
        /// <typeparam name="T">Entity type</typeparam>
        Task<bool> Exists<T>(string entityId);
    }
}
