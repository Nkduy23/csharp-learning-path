using CollectionsLinq.Models;

namespace CollectionsLinq.Services;

public class ProductService
{
    private List<Product> _products = new();
    private int _nextId = 1;

    public ProductService()
    {
        SeedData();
    }

    private void SeedData()
    {
        var seed = new List<(string Name, decimal Price, string Category, int Stock, int Sold)>
        {
            ("Laptop Dell XPS 15",    28_000_000, "Laptop",      15,  120),
            ("Laptop Macbook Pro",    45_000_000, "Laptop",       8,   85),
            ("Laptop Asus Vivobook",  18_000_000, "Laptop",      22,  200),
            ("iPhone 15 Pro",         30_000_000, "Điện thoại",  30,  350),
            ("Samsung Galaxy S24",    25_000_000, "Điện thoại",  25,  280),
            ("Xiaomi 14",             15_000_000, "Điện thoại",  40,  410),
            ("Tai nghe Sony WH-1000", 8_000_000,  "Phụ kiện",   50,   95),
            ("Chuột Logitech MX",     1_500_000,  "Phụ kiện",   80,  560),
            ("Bàn phím Keychron K2",  2_200_000,  "Phụ kiện",   60,  320),
            ("Màn hình LG 27inch",    7_500_000,  "Màn hình",   18,   75),
            ("Màn hình Samsung 24",   4_500_000,  "Màn hình",   20,  130),
        };

        foreach (var (name, price, cat, stock, sold) in seed)
        {
            _products.Add(new Product
            {
                Id = _nextId++,
                Name = name,
                Price = price,
                Category = cat,
                Stock = stock,
                SoldCount = sold
            });
        }
    }

    // --- CRUD ---

    public void Add(string name, decimal price, string category, int stock)
    {
        _products.Add(new Product
        {
            Id = _nextId++,
            Name = name,
            Price = price,
            Category = category,
            Stock = stock
        });
        Console.WriteLine("✅ Đã thêm sản phẩm.");
    }

    public bool Remove(int id)
    {
        var p = _products.FirstOrDefault(p => p.Id == id);
        if (p == null) return false;
        _products.Remove(p);
        return true;
    }

    public void PrintAll(List<Product>? list = null)
    {
        var source = list ?? _products;
        if (source.Count == 0) { Console.WriteLine("Không có sản phẩm."); return; }
        source.ForEach(p => Console.WriteLine(p));
    }

    // --- Tìm kiếm & Lọc ---

    public List<Product> FilterByCategory(string category) =>
        _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                 .ToList();

    public List<Product> FilterByPrice(decimal min, decimal max) =>
        _products.Where(p => p.Price >= min && p.Price <= max)
                 .OrderBy(p => p.Price)
                 .ToList();

    public List<Product> Search(string keyword) =>
        _products.Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                 .ToList();

    public List<Product> SortBy(string field, bool descending = false)
    {
        return field.ToLower() switch
        {
            "price" => descending
                ? _products.OrderByDescending(p => p.Price).ToList()
                : _products.OrderBy(p => p.Price).ToList(),
            "name" => descending
                ? _products.OrderByDescending(p => p.Name).ToList()
                : _products.OrderBy(p => p.Name).ToList(),
            "sold" => _products.OrderByDescending(p => p.SoldCount).ToList(),
            _ => _products
        };
    }

    public List<Product> Top5BestSelling() =>
        _products.OrderByDescending(p => p.SoldCount).Take(5).ToList();

    // --- Thống kê GroupBy ---

    public void PrintStatsByCategory()
    {
        var stats = _products
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Category = g.Key,
                Count    = g.Count(),
                AvgPrice = g.Average(p => p.Price),
                TotalSold = g.Sum(p => p.SoldCount),
                MinPrice = g.Min(p => p.Price),
                MaxPrice = g.Max(p => p.Price),
            })
            .OrderByDescending(g => g.TotalSold);

        Console.WriteLine($"\n{"Danh mục",-15} {"SL",4} {"Giá TB",15} {"Đã bán",8} {"Giá thấp",14} {"Giá cao",14}");
        Console.WriteLine(new string('-', 75));
        foreach (var s in stats)
            Console.WriteLine($"{s.Category,-15} {s.Count,4} {s.AvgPrice,15:C} {s.TotalSold,8} {s.MinPrice,14:C} {s.MaxPrice,14:C}");
    }

    public List<Product> GetProducts() => _products;
}