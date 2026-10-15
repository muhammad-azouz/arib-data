namespace AribONE.Models.Entities;

public class Reservation : Invoice
{
    public ReservationStatus Status { get; set; }

    public static Reservation FromInvoice(Invoice invoice) =>
        CopyFrom(new Reservation { Num = invoice.Num, warehouse = invoice.warehouse }, invoice);

    /// <summary>Same copy, into a caller-made instance of either direction's concrete type
    /// (<see cref="PurchaseReservation"/> for a supplier-side one) — a generic <c>new()</c> factory
    /// can't be used because <see cref="Invoice"/> has required members. The caller still sets
    /// <c>Type</c> explicitly, as the sale side does.</summary>
    public static T CopyFrom<T>(T target, Invoice invoice) where T : Reservation
    {
        target.Num = invoice.Num;
        target.ShiftId = invoice.ShiftId;
        target.CreatedAt = invoice.CreatedAt;
        target.IssuedAt = invoice.IssuedAt;
        target.PartnerId = invoice.PartnerId;
        target.Partner = invoice.Partner;
        target.UserId = invoice.UserId;
        target.User = invoice.User;
        target.BranchId = invoice.BranchId;
        target.Branch = invoice.Branch;
        target.InvoiceLines = invoice.InvoiceLines;
        target.warehouse = invoice.warehouse;
        target.Ship = invoice.Ship;
        target.ShipAddress = invoice.ShipAddress;
        target.ShipPhone1 = invoice.ShipPhone1;
        target.ShipPhone2 = invoice.ShipPhone2;
        target.ItemTotal = invoice.ItemTotal;
        target.BillDiscount = invoice.BillDiscount;
        target.BillDiscountId = invoice.BillDiscountId;
        target.ItemDiscount = invoice.ItemDiscount;
        target.ItemDiscountId = invoice.ItemDiscountId;
        target.BillTax = invoice.BillTax;
        target.BillTaxPercentage = invoice.BillTaxPercentage;
        target.BillTaxId = invoice.BillTaxId;
        target.Money = invoice.Money;
        target.MoneyId = invoice.MoneyId;
        target.Total = invoice.Total;
        target.Remain = invoice.Remain;
        target.TotalMoney = invoice.TotalMoney;
        target.BillExtraId = invoice.BillExtraId;
        target.TotalExtra = invoice.TotalExtra;
        target.TotalDiscount = invoice.TotalDiscount;
        target.RegNum = invoice.RegNum;
        target.ItemCount = invoice.ItemCount;
        target.IsCash = invoice.IsCash;
        target.IsPaid = invoice.IsPaid;
        target.PaidDate = invoice.PaidDate;
        target.PaidValue = invoice.PaidValue;
        target.PaidDiscount = invoice.PaidDiscount;
        target.MoneyTotalPaid = invoice.MoneyTotalPaid;
        target.PaidRegNum = invoice.PaidRegNum;
        target.InternalNote = invoice.InternalNote;
        target.Note = invoice.Note;
        target.Status = ReservationStatus.Pending;
        return target;
    }
}
