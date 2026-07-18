namespace AribONE.Models.Entities;

public class Order : Invoice
{
    public OrderStatus Status { get; set; }

    public static Order FromInvoice(Invoice invoice)
    {
        return new Order
        {
            // Base Invoice properties
            Num = invoice.Num,
            ShiftId = invoice.ShiftId,
            CreatedAt = invoice.CreatedAt,
            IssuedAt = invoice.IssuedAt,
            PartnerId = invoice.PartnerId,
            Partner = invoice.Partner,
            UserId = invoice.UserId,
            User = invoice.User,
            BranchId = invoice.BranchId,
            Branch = invoice.Branch,
            InvoiceLines = invoice.InvoiceLines, // Be careful: you may want to clone this
            warehouse = invoice.warehouse,
            Ship = invoice.Ship,
            ShipAddress = invoice.ShipAddress,
            ShipPhone1 = invoice.ShipPhone1,
            ShipPhone2 = invoice.ShipPhone2,
            ItemTotal = invoice.ItemTotal,
            BillDiscount = invoice.BillDiscount,
            BillDiscountId = invoice.BillDiscountId,
            ItemDiscount = invoice.ItemDiscount,
            ItemDiscountId = invoice.ItemDiscountId,
            BillTax = invoice.BillTax,
            BillTaxPercentage = invoice.BillTaxPercentage,
            BillTaxId = invoice.BillTaxId,
            Money = invoice.Money,
            MoneyId = invoice.MoneyId,
            Total = invoice.Total,
            Remain = invoice.Remain,
            TotalMoney = invoice.TotalMoney,
            BillExtraId = invoice.BillExtraId,
            TotalExtra = invoice.TotalExtra,
            TotalDiscount = invoice.TotalDiscount,
            RegNum = invoice.RegNum,
            ItemCount = invoice.ItemCount,
            IsCash = invoice.IsCash,
            IsPaid = invoice.IsPaid,
            PaidDate = invoice.PaidDate,
            PaidValue = invoice.PaidValue,
            PaidDiscount = invoice.PaidDiscount,
            MoneyTotalPaid = invoice.MoneyTotalPaid,
            PaidRegNum = invoice.PaidRegNum,
            InternalNote = invoice.InternalNote,
            Note = invoice.Note,
            // Specific to Order
            Status = OrderStatus.Pending
        };
    }
}