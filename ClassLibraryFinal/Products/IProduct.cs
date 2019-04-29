namespace ClassLibraryFinal
{
    public interface IProduct
    {
        string Name { get; }
        string ShortDescription { get; }
        double Price { get; }
        double ShippingWeight { get; }
        double TaxRate { get; }
    }
}