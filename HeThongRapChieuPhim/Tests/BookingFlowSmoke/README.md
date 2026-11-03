# Booking flow smoke checks

Build the WinForms project first, then run from the solution folder:

    dotnet run --project Tests/BookingFlowSmoke/BookingFlowSmoke.csproj

Checks cover the booking wizard, seat selection, VIP price calculations, food quantities and schedule boundaries. They use sample data without connecting to the database or invoking the payment API.
