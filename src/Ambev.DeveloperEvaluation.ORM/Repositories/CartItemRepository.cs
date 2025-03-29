using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ICartItemRepository using Entity Framework Core
    /// </summary>
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DefaultContext _context;
        /// <summary>
        /// Initializes a new instance of CartItemRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public CartItemRepository(DefaultContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates a new cartItem in the database
        /// </summary>
        /// <param name="cartItem">The cartItem to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created cartItem</returns>
        public async Task<CartItem> CreateAsync(CartItem cartItem, CancellationToken cancellationToken = default)
        {
            await _context.CartItems.AddAsync(cartItem, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return cartItem;
        }

        /// <summary>
        /// Soft deletes a cartItem from the database
        /// </summary>
        /// <param name="id">The unique identifier of the cartItem to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the cartItem was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cartItem = await GetByIdAsync(id, cancellationToken);
            if (cartItem == null)
            {
                return false;
            }
            cartItem.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        /// <summary>
        /// Retrieves a list of cartItems
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cartItems if found, null otherwise</returns>
        public async Task<IEnumerable<CartItem?>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.CartItems.ToListAsync(cancellationToken);
        }

        public Task<IEnumerable<CartItem?>> GetFiltered(Func<CartItem, bool> predicate, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return (Task<IEnumerable<CartItem?>>)_context.CartItems.Where(predicate);
        }

        /// <summary>
        /// Retrieves a cartItem by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the cartItem</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cartItem if found, null otherwise</returns>
        public async Task<CartItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.CartItems.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<CartItem> UpdateAsync(CartItem cartItem, CancellationToken cancellationToken = default)
        {
            var existingCartItem = await _context.CartItems.Include(c => c.Cart).FirstOrDefaultAsync(o => o.Id == cartItem.Id, cancellationToken);
            if (existingCartItem == null)
                throw new KeyNotFoundException($"CartItem with ID {cartItem.Id} not found");

            if (existingCartItem.Cart.Status == CartStatus.Closed || existingCartItem.Cart.Status == CartStatus.Cancelled)
                throw new InvalidOperationException($"Cannot update an item that cart is {existingCartItem.Cart.Status} ");
            
            cartItem.UpdatedAt = DateTime.UtcNow;

            if (cartItem.IsDeleted && !existingCartItem.IsDeleted)
                existingCartItem.AddDomainEvent(new ItemCancelledEvent(existingCartItem));
            _context.Entry(existingCartItem).CurrentValues.SetValues(cartItem);

            await _context.SaveChangesAsync(cancellationToken);
            return existingCartItem;
        }
    }
}
