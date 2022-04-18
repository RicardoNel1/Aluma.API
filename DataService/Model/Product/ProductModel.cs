using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("products")]
    public class ProductModel : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Institute { get; set; }
        public ProductTypesEnum ProductType { get; set; }
        public ProductCategoriesEnum ProductCategory { get; set; }
        public PaymentTypesEnum PaymentType { get; set; }
        public bool IsActive { get; set; }
        public RiskRatingsEnum AssociatedRisk { get; set; }

        //to be added
        //variable rates
        //product life span
        //product listing date ranges
    }
    public class ProductModelBuilder : IEntityTypeConfiguration<ProductModel>
    {
        public void Configure(EntityTypeBuilder<ProductModel> mb)
        {
            mb.HasData(new ProductModel()
            {
                Id = 1,
                Name = "Structured Note",
                Description = @"The Minimum Return Multi-Asset Global Note (the “Note”) is a five year 100% ZAR capital protected investment linked to the CITI Flexible Multi Asset V15% Index (the “Index”). The investment objective of Note is to provide no minimum ZAR return, with the maximum possible full uncapped participation in the Index, with a 100% ZAR capital protected investment, thus also providing full USD / ZAR return exposure. It is Aluma’s view that this investment could be suitable for investors who require exposure to a low risk USD Multi-Asset Balanced Portfolio and 100% ZAR capital protection.",
                Institute = "Standard Bank",
                ProductType = ProductTypesEnum.Investment,
                ProductCategory = ProductCategoriesEnum.Category2,
                PaymentType = PaymentTypesEnum.Lumpsum,
                AssociatedRisk = RiskRatingsEnum.ModeratelyAggressive,
                IsActive = true,
            });
            mb.HasData(new ProductModel()
            {
                Id = 2,
                Name = "Local Share Portfolio",
                Description = @"Our Local Share Portfolio is an investment product for discretionary money, which allows you to access to all Local Shares listed on the JSE as well as EFTs.It is subject to CGT, income tax on interest, dividends tax, and Real Estate Investment Trust (REIT) tax. Income tax is due whether interest is earned by your investment. A CGT event will occur when you do a withdrawal or a switch from an investment portfolio. A 20% withholdings tax on local dividends applies. We will deduct any dividend and REIT tax which you may owe from an income distribution before it’s invested into your investment account. Contributions, withdrawals and drawing a regular withdrawal
                                are allowable at any point in time without incurring penalties.You may change,
                                stop and resume your ad hoc or regular contributions at any time without incurring any penalties.",
                Institute = "Standard Bank",
                ProductType = ProductTypesEnum.Investment,
                ProductCategory = ProductCategoriesEnum.Category2,
                PaymentType = PaymentTypesEnum.Lumpsum,
                AssociatedRisk = RiskRatingsEnum.ModeratelyAggressive,
                IsActive = false
            });
            mb.HasData(new ProductModel()
            {
                Id = 3,
                Name = "International Share Portfolio",
                Description = @"Our International Share Portfolio is an investment product for discretionary money, which allows you to access to all International Shares listed on International Stock exchanges as well as ETFs. It is subject to CGT, income tax on interest, dividends tax, and Real Estate Investment Trust (REIT) tax. Income tax is due whenever interest is earned by your investment. A CGT event will occur when you do a withdrawal or a switch from an investment portfolio. A 20% withholdings tax on local dividends applies. We will deduct any dividend and REIT tax which you may owe from an income distribution before it’s invested into your investment account Contributions, withdrawals and drawing a regular withdrawal are allowable at any point in time without incurring penalties. You may change, stop and resume your ad hoc or regular contributions at any time without incurring any penalties.",
                Institute = "Standard Bank",
                ProductType = ProductTypesEnum.Investment,
                ProductCategory = ProductCategoriesEnum.Category2,
                PaymentType = PaymentTypesEnum.Recurringsum,
                AssociatedRisk = RiskRatingsEnum.ModeratelyAggressive,
                IsActive = false
            });
            mb.HasData(new ProductModel()
            {
                Id = 4,
                Name = "Self Managed Account",
                Description = @"Trade the JSE and International Equities, CFDs, Indices, ETFs, Forex & Commodities from a single trading account at very competitive rates. Aluma clients receive cost- effective trading via a Multi Asset Direct Market Access (DMA) class-leading trading platform. Clients have the option to manage their own trading account and execute their own trades, or to have their account managed on a discretionary basis.",
                Institute = "Standard Bank",
                ProductType = ProductTypesEnum.Investment,
                ProductCategory = ProductCategoriesEnum.Category2,
                PaymentType = PaymentTypesEnum.Recurringsum,
                AssociatedRisk = RiskRatingsEnum.ModeratelyAggressive,
                IsActive = false
            });
            mb.HasData(new ProductModel()
            {
                Id = 5,
                Name = "Private Equity Fund - Growth",
                Description = @"Limited partner interests (the 'Interests') in The Aluma Capital Private Equity Fund I Partnership (the 'Partnership') are being offered to qualified investors.
                                The Interests are offered subject to the right of Aluma Capital General Partner(Proprietary) Limited(the 'General Partner'), in its capacity as the ultimate
                                general partner of the Partnership, to reject any application in whole or in part.",
                Institute = "Aluma Capital",
                ProductType = ProductTypesEnum.Investment,
                ProductCategory = ProductCategoriesEnum.Category2,
                PaymentType = PaymentTypesEnum.Lumpsum,
                AssociatedRisk = RiskRatingsEnum.ModeratelyAggressive,
                IsActive = true
            });
            mb.HasData(new ProductModel()
            {
                Id = 6,
                Name = "Private Equity Fund - Income",
                Description = @"Limited partner interests (the 'Interests') in The Aluma Capital Private Equity Fund I Partnership (the 'Partnership') are being offered to qualified investors.
                                The Interests are offered subject to the right of Aluma Capital General Partner(Proprietary) Limited(the 'General Partner'), in its capacity as the ultimate
                                general partner of the Partnership, to reject any application in whole or in part.",
                Institute = "Aluma Capital",
                ProductType = ProductTypesEnum.Investment,
                ProductCategory = ProductCategoriesEnum.Category2,
                PaymentType = PaymentTypesEnum.Lumpsum,
                AssociatedRisk = RiskRatingsEnum.ModeratelyAggressive,
                IsActive = true
            });
            mb.HasData(new ProductModel()
            {
                Id = 7,
                Name = "Fixed Income",
                Description = @" ",
                Institute = "Vanguard",
                ProductType = ProductTypesEnum.Investment,
                ProductCategory = ProductCategoriesEnum.Category2,
                PaymentType = PaymentTypesEnum.Lumpsum,
                AssociatedRisk = RiskRatingsEnum.ModeratelyAggressive,
                IsActive = false
            });

        }
    }

}