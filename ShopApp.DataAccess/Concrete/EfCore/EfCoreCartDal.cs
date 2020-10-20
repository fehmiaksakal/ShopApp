using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart, ShopContext>, ICartDal
    {

        public override void Update(Cart entity)
        {
            using (var context = new ShopContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                return context.Carts
                    .Include(x => x.CartItems)
                    .ThenInclude(x => x.Product)
                    .FirstOrDefault(x => x.UserId == userId);
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"DELETE FROM CartItem WHERE CartId = @p0 AND ProductId = @p1";
                context.Database.ExecuteSqlRaw(cmd, cartId, productId);
            }
        }
    }
}
