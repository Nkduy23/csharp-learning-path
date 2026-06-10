namespace CollectionsLinq.Models;

public record CartItem(Product Product, int Quantity)
{
    public decimal Subtotal => Product.Price * Quantity;

    public override string ToString() =>
        $"{Product.Name,-25} x{Quantity} = {Subtotal:C}";
}