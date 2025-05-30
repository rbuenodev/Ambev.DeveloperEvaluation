﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartItemRepository
    {
        /// <summary>
        /// Creates a new cartItem in the repository
        /// </summary>
        /// <param name="cartItem">The cartItem to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created cartItem</returns>
        Task<CartItem> CreateAsync(CartItem cartItem, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves a cartItem by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the cartItem</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cartItem if found, null otherwise</returns>
        Task<CartItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all cartItems
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The list of cartItems if found, null otherwise</returns>
        Task<IEnumerable<CartItem?>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a cartItem from the repository
        /// </summary>
        /// <param name="id">The unique identifier of the cartItem to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the cartItem was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves cartItems by a filter
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="predicate">predicate function</param>
        /// <returns>The list of cartItems if found, null otherwise</returns>
        public Task<IEnumerable<CartItem?>> GetFiltered(Expression<Func<CartItem, bool>> predicate, CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates a cartItem in the repository
        /// </summary>
        /// <param name="cartItem">The cartItem to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated cartItem</returns>
        Task<CartItem> UpdateAsync(CartItem cartItem, CancellationToken cancellationToken = default);
    }
}
