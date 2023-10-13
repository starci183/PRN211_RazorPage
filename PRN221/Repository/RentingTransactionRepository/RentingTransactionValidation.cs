using DAOs.Models;

public class RentingTransactionValidation
{
    public string RentingDateError { get; private set; }
    public string TotalPriceError { get; private set; }
    public string RentingStatusError { get; private set; }

    public RentingTransactionValidation()
    {
        RentingDateError = string.Empty;
        TotalPriceError = string.Empty;
        RentingStatusError = string.Empty;
    }

    public bool HasError()
    {
        return !string.IsNullOrEmpty(RentingDateError) ||
               !string.IsNullOrEmpty(TotalPriceError) ||
               !string.IsNullOrEmpty(RentingStatusError);
    }

    public void Validate(RentingTransaction rentingTransaction)
    {
        ResetErrors();

        if (!rentingTransaction.RentingDate.HasValue || rentingTransaction.RentingDate.Value > DateTime.Now)
        {
            RentingDateError = "Invalid renting date.";
        }

        if (!rentingTransaction.TotalPrice.HasValue || rentingTransaction.TotalPrice.Value <= 0)
        {
            TotalPriceError = "Invalid total price.";
        }

        if (!rentingTransaction.RentingStatus.HasValue || (rentingTransaction.RentingStatus.Value != 0 && rentingTransaction.RentingStatus.Value != 1))
        {
            RentingStatusError = "Invalid renting status.";
        }
    }

    private void ResetErrors()
    {
        RentingDateError = string.Empty;
        TotalPriceError = string.Empty;
        RentingStatusError = string.Empty;
    }
}
