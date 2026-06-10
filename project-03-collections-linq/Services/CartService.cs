using CollectionsLinq.Models;

namespace CollectionsLinq.Services;

public class CartService
{
    private readonly Dictionary<int, CartItem> _cart = new();
    private readonly HashSet<int> _wishlist = new();

    // --- Cart ---

    public void AddToCart(Product product, int quantity = 1)
    {
        if (_cart.ContainsKey(product.Id))
        {
            var existing = _cart[product.Id];
            _cart[product.Id] = existing with { Quantity = existing.Quantity + quantity };
        }
        else
        {
            _cart[product.Id] = new CartItem(product, quantity);
        }
        Console.WriteLine($"🛒 Đã thêm: {product.Name} x{quantity}");
    }

    public void RemoveFromCart(int productId)
    {
        if (_cart.Remove(productId))
            Console.WriteLine("✅ Đã xóa khỏi giỏ hàng.");
        else
            Console.WriteLine("❌ Sản phẩm không có trong giỏ.");
    }

    public void PrintCart()
    {
        if (_cart.Count == 0) { Console.WriteLine("Giỏ hàng trống."); return; }

        Console.WriteLine("\n===== 🛒 GIỎ HÀNG =====");
        foreach (var item in _cart.Values)
            Console.WriteLine(item);

        decimal total = _cart.Values.Sum(i => i.Subtotal);
        Console.WriteLine(new string('-', 45));
        Console.WriteLine($"{"TỔNG CỘNG:",35} {total:C}");
    }

    public void ApplyVoucher(string code)
    {
        var vouchers = new Dictionary<string, double>
        {
            { "SALE10", 0.10 },
            { "SALE20", 0.20 },
            { "VIP30",  0.30 },
        };

        if (!vouchers.TryGetValue(code.ToUpper(), out double discount))
        {
            Console.WriteLine("❌ Mã voucher không hợp lệ.");
            return;
        }

        decimal total    = _cart.Values.Sum(i => i.Subtotal);
        decimal saved    = total * (decimal)discount;
        decimal newTotal = total - saved;
        Console.WriteLine($"🎉 Voucher {code} — Giảm {discount:P0} — Tiết kiệm {saved:C}");
        Console.WriteLine($"💰 Tổng sau giảm: {newTotal:C}");
    }

    // --- Wishlist ---

    public void AddToWishlist(int productId)
    {
        if (_wishlist.Add(productId))
            Console.WriteLine("❤️  Đã thêm vào wishlist.");
        else
            Console.WriteLine("ℹ️  Sản phẩm đã có trong wishlist.");
    }

    public void PrintWishlist(List<Product> allProducts)
    {
        var items = allProducts.Where(p => _wishlist.Contains(p.Id)).ToList();
        Console.WriteLine("\n===== ❤️  WISHLIST =====");
        if (items.Count == 0) { Console.WriteLine("Wishlist trống."); return; }
        items.ForEach(p => Console.WriteLine(p));
    }
}