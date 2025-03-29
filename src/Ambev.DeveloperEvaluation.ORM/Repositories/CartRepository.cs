using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    /// <summary>
    /// Implementation of ICartRepository using Entity Framework Core
    /// </summary>
    public class CartRepository : ICartRepository
    {
        private readonly DefaultContext _context;

        /// <summary>
        /// Initializes a new instance of ProductRepository
        /// </summary>
        /// <param name="context">The database context</param>
        public CartRepository(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new cart in the database
        /// </summary>
        /// <param name="product">The product to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created product</returns>
        public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await _context.Carts.AddAsync(cart, cancellationToken);
            cart.DomainEvents.Add(new SaleCreatedEvent(cart));
            await _context.SaveChangesAsync(cancellationToken);

            return cart;
        }
        /// <summary>
        /// Deletes a cart from the database
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the product was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var cart = await GetByIdAsync(id, cancellationToken);
            if (cart == null)
            {
                return false;
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


        /// <summary>
        /// Retrieves a cart by their unique identifier, aggragated with cartItems and products
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product if found, null otherwise</returns>

        public async Task<Cart?> GetAggregateByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts.AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Items).ThenInclude(x => x.Product).
                FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Retrieves a list of carts
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The carats if found, null otherwise</returns>

        public async Task<IEnumerable<Cart?>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Carts.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves a cart by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the cart</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The cart if found, null otherwise</returns>

        public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }


        /// <summary>
        /// Updates a cart in the database
        /// </summary>
        /// <param name="cart">The cart to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated cart</returns>
        public async Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            var existingCart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cart.Id, cancellationToken);

            if (existingCart == null)
            {
                throw new InvalidOperationException($"Cart with ID {cart.Id} does not exist.");
            }

            if (cart.Status == CartStatus.Cancelled && existingCart.Status != cart.Status)
            {
                existingCart.AddDomainEvent(new SaleCancelledEvent(existingCart));
            }

            _context.Entry(existingCart).CurrentValues.SetValues(cart);

            foreach (var item in cart.Items)
            {
                var existingItem = existingCart.Items.FirstOrDefault(i => i.Id == item.Id);

                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(item);
                }
                else
                {
                    existingCart.Items.Add(item);
                }
            }
            foreach (var existingItem in existingCart.Items.ToList())
            {
                if (!cart.Items.Any(i => i.Id == existingItem.Id))
                {
                    existingItem.IsDeleted = true;
                }
            }

            existingCart.DomainEvents.Add(new SaleModifiedEvent(existingCart));
            await _context.SaveChangesAsync(cancellationToken);
            existingCart.Items = existingCart.Items.Where(i => !i.IsDeleted).ToList();

            return existingCart;
        }
    }
}
