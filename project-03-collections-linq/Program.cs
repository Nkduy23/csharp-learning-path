using CollectionsLinq.Services;

var productService = new ProductService();
var cartService    = new CartService();

while (true)
{
    Console.WriteLine("\n===== 🛍️  PRODUCT CATALOG =====");
    Console.WriteLine("1.  Xem tất cả sản phẩm");
    Console.WriteLine("2.  Lọc theo danh mục");
    Console.WriteLine("3.  Lọc theo giá");
    Console.WriteLine("4.  Tìm kiếm sản phẩm");
    Console.WriteLine("5.  Sắp xếp (giá / tên / bán chạy)");
    Console.WriteLine("6.  Top 5 bán chạy");
    Console.WriteLine("7.  Thống kê theo danh mục");
    Console.WriteLine("8.  Thêm sản phẩm");
    Console.WriteLine("9.  Xóa sản phẩm");
    Console.WriteLine("──────────────────────────────");
    Console.WriteLine("10. Thêm vào giỏ hàng");
    Console.WriteLine("11. Xem giỏ hàng");
    Console.WriteLine("12. Xóa khỏi giỏ hàng");
    Console.WriteLine("13. Áp dụng voucher");
    Console.WriteLine("14. Thêm vào wishlist");
    Console.WriteLine("15. Xem wishlist");
    Console.WriteLine("0.  Thoát");
    Console.Write("👉 Chọn: ");

    switch (Console.ReadLine())
    {
        case "1":
            productService.PrintAll();
            break;

        case "2":
            Console.Write("Danh mục (Laptop / Điện thoại / Phụ kiện / Màn hình): ");
            productService.PrintAll(productService.FilterByCategory(Console.ReadLine() ?? ""));
            break;

        case "3":
            Console.Write("Giá từ: "); decimal min = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Giá đến: "); decimal max = decimal.Parse(Console.ReadLine() ?? "0");
            productService.PrintAll(productService.FilterByPrice(min, max));
            break;

        case "4":
            Console.Write("Từ khóa: ");
            productService.PrintAll(productService.Search(Console.ReadLine() ?? ""));
            break;

        case "5":
            Console.Write("Sắp xếp theo (price / name / sold): ");
            string field = Console.ReadLine() ?? "price";
            Console.Write("Giảm dần? (y/n): ");
            bool desc = Console.ReadLine()?.ToLower() == "y";
            productService.PrintAll(productService.SortBy(field, desc));
            break;

        case "6":
            Console.WriteLine("\n===== 🏆 TOP 5 BÁN CHẠY =====");
            productService.PrintAll(productService.Top5BestSelling());
            break;

        case "7":
            productService.PrintStatsByCategory();
            break;

        case "8":
            Console.Write("Tên: ");      string name = Console.ReadLine() ?? "";
            Console.Write("Giá: ");      decimal price = decimal.Parse(Console.ReadLine() ?? "0");
            Console.Write("Danh mục: "); string cat = Console.ReadLine() ?? "";
            Console.Write("Kho: ");      int stock = int.Parse(Console.ReadLine() ?? "0");
            productService.Add(name, price, cat, stock);
            break;

        case "9":
            Console.Write("ID sản phẩm cần xóa: ");
            int delId = int.Parse(Console.ReadLine() ?? "0");
            Console.WriteLine(productService.Remove(delId) ? "✅ Đã xóa." : "❌ Không tìm thấy.");
            break;

        case "10":
            productService.PrintAll();
            Console.Write("ID sản phẩm: ");
            int cartId = int.Parse(Console.ReadLine() ?? "0");
            var product = productService.GetProducts().FirstOrDefault(p => p.Id == cartId);
            if (product == null) { Console.WriteLine("❌ Không tìm thấy."); break; }
            Console.Write("Số lượng: ");
            int qty = int.Parse(Console.ReadLine() ?? "1");
            cartService.AddToCart(product, qty);
            break;

        case "11":
            cartService.PrintCart();
            break;

        case "12":
            Console.Write("ID sản phẩm cần xóa khỏi giỏ: ");
            cartService.RemoveFromCart(int.Parse(Console.ReadLine() ?? "0"));
            break;

        case "13":
            Console.Write("Nhập mã voucher (SALE10 / SALE20 / VIP30): ");
            cartService.ApplyVoucher(Console.ReadLine() ?? "");
            break;

        case "14":
            Console.Write("ID sản phẩm: ");
            cartService.AddToWishlist(int.Parse(Console.ReadLine() ?? "0"));
            break;

        case "15":
            cartService.PrintWishlist(productService.GetProducts());
            break;

        case "0":
            Console.WriteLine("👋 Tạm biệt!");
            return;

        default:
            Console.WriteLine("❌ Lựa chọn không hợp lệ.");
            break;
    }
}