using DAOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomerValidation
{
    public string CustomerNameError { get; private set; }
    public string TelephoneError { get; private set; }
    public string EmailError { get; private set; }
    public string CustomerBirthdayError { get; private set; }
    public string CustomerStatusError { get; private set; }
    public string PasswordError { get; private set; }

    public CustomerValidation()
    {
        CustomerNameError = string.Empty;
        TelephoneError = string.Empty;
        EmailError = string.Empty;
        CustomerBirthdayError = string.Empty;
        CustomerStatusError = string.Empty;
        PasswordError = string.Empty;
    }

    public bool HasError()
    {
        return !string.IsNullOrEmpty(CustomerNameError) ||
               !string.IsNullOrEmpty(TelephoneError) ||
               !string.IsNullOrEmpty(EmailError) ||
               !string.IsNullOrEmpty(CustomerBirthdayError) ||
               !string.IsNullOrEmpty(CustomerStatusError) ||
               !string.IsNullOrEmpty(PasswordError);
    }

    public void Validate(Customer customer)
    {
        ResetErrors();

        if (string.IsNullOrWhiteSpace(customer.CustomerName) || customer.CustomerName.Length > 64)
        {
            CustomerNameError = "Customer name is required and must be 1 to 64 characters long.";
        }

        if (customer.Telephone == null || customer.Telephone.Length > 12)
        {
            TelephoneError = "Telephone number must be at most 12 characters long.";
        }

        if (string.IsNullOrWhiteSpace(customer.Email) || !IsValidEmail(customer.Email))
        {
            EmailError = "Invalid email address.";
        }

        if (!customer.CustomerBirthday.HasValue || customer.CustomerBirthday.Value < DateTime.Now.AddYears(-150) || customer.CustomerBirthday.Value > DateTime.Now)
        {
            CustomerBirthdayError = "Invalid customer birthday.";
        }

        if (!customer.CustomerStatus.HasValue || (customer.CustomerStatus.Value != 0 && customer.CustomerStatus.Value != 1))
        {
            CustomerStatusError = "Invalid customer status.";
        }

        if (string.IsNullOrWhiteSpace(customer.Password) || customer.Password.Length < 8)
        {
            PasswordError = "Password is required and must be at least 8 characters long.";
        }
    }

    private void ResetErrors()
    {
        CustomerNameError = string.Empty;
        TelephoneError = string.Empty;
        EmailError = string.Empty;
        CustomerBirthdayError = string.Empty;
        CustomerStatusError = string.Empty;
        PasswordError = string.Empty;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var address = new System.Net.Mail.MailAddress(email);
            return address.Address == email;
        }
        catch
        {
            return false;
        }
    }
}