namespace DataService.Enum
{
    //not going to use the products enum any more,  but will keep the enum class, for future product enums.  (status,  types)
    public enum ProductsEnum
    {
        StructuredNote,
        LocalSharePortfolio,
        InternationalSharedPortfolio,
        SelfManagedAccount,
        PE1,
        PE2
    }

    public enum PaymentTypesEnum
    {
        Lumpsum, 
        Recurringsum
    }

    public enum ProductTypesEnum
    {
        Investment
    }

    public enum ProductCategoriesEnum
    {
        Category1, 
        Category2,
    }

}