using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;

        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                var index = cart.CartItems.FindIndex(x => x.ProductId == productId);

                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        CartId = cart.Id,
                        Quantity = quantity,
                        AddedDate = DateTime.Now
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += Math.Abs(quantity);
                }

                _cartDal.Update(cart);
            }
        }

        public void ClearCart(string cartId)
        {
            _cartDal.ClearCart(cartId);
        }

        public void DeleteFromCart(string userId, int productId)
        {
            var cart = GetCartByUserId(userId);

            if (cart != null)
            {
                _cartDal.DeleteFromCart(cart.Id, productId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartDal.GetCartByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _cartDal.Create(new Cart()
            {
                UserId = userId
            });
        }
    }
}
