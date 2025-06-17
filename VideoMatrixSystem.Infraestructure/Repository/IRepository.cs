using VideoMatrixSystem.Domain.Common;

namespace VideoMatrixSystem.Domain.Repository
{
    public interface IRepository<T> where T : MyEntity
    {
        /// <summary>
        /// Adds an entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>True if the entity was added successfully; otherwise False</returns>
        public Task<bool> AddAsync(T entity);

        /// <summary>
        /// Adds a list of entities to the repository
        /// </summary>
        /// <param name="entities">List of entities to add</param>
        /// <returns>True if the entities were added successfully, otherwise false</returns>
        public Task<bool> AddRangeAsync(List<T> entities);

        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>True if the entity was updated successfully; otherwise False</returns>
        public Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Updates a list of entities in the repository
        /// </summary>
        /// <param name="entities">List of entities to update</param>
        /// <returns>True if the entities were updated succesfully; otherwise false</returns>
        public Task<bool> UpdateRangeAsync(List<T> entities);

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>True if the entity was deleted successfully; otherwise false</returns>
        public Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Removes a list of entities from the repository.
        /// </summary>
        /// <param name="entities">List of entities to remove</param>
        /// <returns>True if the entities were removed successfully; otherwise false</returns>
        public Task<bool> DeleteRangeAsync(List<T> entities);

        /// <summary>
        /// Retrieves all the entities in the repository
        /// </summary>
        /// <returns>A list of entities</returns>
        public List<T> GetAll();

        /// <summary>
        /// Retrieves an entity by its unique identifier
        /// </summary>
        /// <param name="id">the unique identifier of the entity</param>
        /// <returns>The entity its found; otherwise null</returns>
        public T? GetById(int id);

        /// <summary>
        /// Checks if the entity is valid to be inserted in the repository
        /// </summary>
        /// <param name="entity">The entity to validate</param>
        /// <returns>True if the entity is valid; otherwise false</returns>
        public bool IsValid(T entity);

        /// <summary>
        /// Save the changes in the database
        /// </summary>
        public Task SaveChangesAsync();
    }
}
