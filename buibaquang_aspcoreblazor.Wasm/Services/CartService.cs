using buibaquang_aspcoreblazor.Models.Cart;

namespace buibaquang_aspcoreblazor.Wasm.Services
{
    public interface ICartService
    {
        Cart GetCart();
        void AddToCart(CartItem item);
        void RemoveFromCart(Guid productId);
        void ClearCart();
    }

    public class CartService : ICartService
    {
        private Cart _cart = new Cart();

        public Cart GetCart()
        {
            return _cart;
        }

        public void AddToCart(CartItem item)
        {
            var existingItem = _cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                _cart.Items.Add(item);
            }
        }

        public void RemoveFromCart(Guid productId)
        {
            var item = _cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _cart.Items.Remove(item);
            }
        }

        public void ClearCart()
        {
            _cart.Items.Clear();
        }
    }

}
