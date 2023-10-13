using DAOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CarValidation
{
        public string CarNameError { get; private set; }
        public string CarDescriptionError { get; private set; }
        public string NumberOfDoorsError { get; private set; }
        public string SeatingCapacityError { get; private set; }
        public string FuelTypeError { get; private set; }
        public string YearError { get; private set; }
        public string ManufacturerIdError { get; private set; }
        public string CarStatusError { get; private set; }
        public string CarRentingPricePerDayError { get; private set; }

    public CarValidation()
    {
        CarNameError = string.Empty;
        CarDescriptionError = string.Empty;
        NumberOfDoorsError = string.Empty;
        SeatingCapacityError = string.Empty;
        FuelTypeError = string.Empty;
        YearError = string.Empty;
        ManufacturerIdError = string.Empty;
        CarStatusError = string.Empty;
        CarRentingPricePerDayError = string.Empty;
    }
    public bool HasError()
        {
            return !string.IsNullOrEmpty(CarNameError) ||
                   !string.IsNullOrEmpty(CarDescriptionError) ||
                   !string.IsNullOrEmpty(NumberOfDoorsError) ||
                   !string.IsNullOrEmpty(SeatingCapacityError) ||
                   !string.IsNullOrEmpty(FuelTypeError) ||
                   !string.IsNullOrEmpty(YearError) ||
                   !string.IsNullOrEmpty(ManufacturerIdError) ||
                   !string.IsNullOrEmpty(CarStatusError) ||
                   !string.IsNullOrEmpty(CarRentingPricePerDayError);
        }

    public void Validate(CarInformation car)
    {
        ResetErrors();

        if (string.IsNullOrWhiteSpace(car.CarName) || car.CarName.Length > 64)
        {
            CarNameError = "Car name is required and must be 1 to 64 characters long.";
        }

        if (string.IsNullOrWhiteSpace(car.CarDescription) || car.CarDescription.Length > 2000)
        {
            CarDescriptionError = "Car description is required and must be at most 2000 characters long.";
        }

        if (!car.NumberOfDoors.HasValue || car.NumberOfDoors.Value <= 0 || car.NumberOfDoors.Value > 24)
        {
            NumberOfDoorsError = "Number of doors must be between 1 and 24.";
        }

        if (!car.SeatingCapacity.HasValue || car.SeatingCapacity.Value <= 0 || car.SeatingCapacity > 128)
        {
            SeatingCapacityError = "Seating capacity must must be between 1 and 128.";
        }

        if (string.IsNullOrWhiteSpace(car.FuelType) || car.FuelType.Length > 64)
        {
            FuelTypeError = "Fuel type is required and must be 1 to 64 characters long.";
        }

        if (!car.Year.HasValue || car.Year.Value < 1900 || car.Year.Value > DateTime.Now.Year)
        {
            YearError = "Invalid year.";
        }

        if (!car.CarStatus.HasValue || car.CarStatus.Value < 0 || car.CarStatus.Value > 255)
        {
            CarStatusError = "Invalid car status.";
        }

        if (!car.CarRentingPricePerDay.HasValue || car.CarRentingPricePerDay.Value <= 0)
        {
            CarRentingPricePerDayError = "Invalid renting price per day.";
        }
    }

    private void ResetErrors()
        {
            CarNameError = string.Empty;
            CarDescriptionError = string.Empty;
            NumberOfDoorsError = string.Empty;
            SeatingCapacityError = string.Empty;
            FuelTypeError = string.Empty;
            YearError = string.Empty;
            ManufacturerIdError = string.Empty;
            CarStatusError = string.Empty;
            CarRentingPricePerDayError = string.Empty;
        }
}
