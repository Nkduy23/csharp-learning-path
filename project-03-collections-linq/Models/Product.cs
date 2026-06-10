namespace CollectionsLinq.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Stock { get; set; }
    public int SoldCount { get; set; }

    public override string ToString() =>
        $"[{Id}] {Name,-25} | {Category,-12} | {Price,12:C} | Kho: {Stock} | Đã bán: {SoldCount}";
}