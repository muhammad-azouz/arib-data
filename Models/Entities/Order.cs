namespace AribONE.Models.Entities;

public class Order : Bill
{
    public OrderStatus Status { get; set; }

    public static Order FromBill(Bill bill)
    {
        return new Order
        {
            // Base Bill properties
            Num = bill.Num,
            ShiftId = bill.ShiftId,
            CreatedAt = bill.CreatedAt,
            IssuedAt = bill.IssuedAt,
            CustomerId = bill.CustomerId,
            Customer = bill.Customer,
            UserId = bill.UserId,
            User = bill.User,
            BranchId = bill.BranchId,
            Branch = bill.Branch,
            BillEntries = bill.BillEntries, // Be careful: you may want to clone this
            warehouse = bill.warehouse,
            Ship = bill.Ship,
            ShipAddress = bill.ShipAddress,
            ShipPhone1 = bill.ShipPhone1,
            ShipPhone2 = bill.ShipPhone2,
            ItemTotal = bill.ItemTotal,
            BillDiscount = bill.BillDiscount,
            BillDiscountId = bill.BillDiscountId,
            ItemDiscount = bill.ItemDiscount,
            ItemDiscountId = bill.ItemDiscountId,
            BillTax = bill.BillTax,
            BillTaxPercentage = bill.BillTaxPercentage,
            BillTaxId = bill.BillTaxId,
            Money = bill.Money,
            MoneyId = bill.MoneyId,
            Total = bill.Total,
            Remain = bill.Remain,
            TotalMoney = bill.TotalMoney,
            BillExtraId = bill.BillExtraId,
            TotalExtra = bill.TotalExtra,
            TotalDiscount = bill.TotalDiscount,
            RegNum = bill.RegNum,
            ItemCount = bill.ItemCount,
            IsCash = bill.IsCash,
            IsPaid = bill.IsPaid,
            PaidDate = bill.PaidDate,
            PaidValue = bill.PaidValue,
            PaidDiscount = bill.PaidDiscount,
            MoneyTotalPaid = bill.MoneyTotalPaid,
            PaidRegNum = bill.PaidRegNum,
            InternalNote = bill.InternalNote,
            Note = bill.Note,
            // Specific to Order
            Status = OrderStatus.Pending
        };
    }
}