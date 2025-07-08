# QuantumBookstore

A simple C# console application demonstrating a bookstore management system. This project showcases Object-Oriented Programming (OOP) principles like abstraction and inheritance to manage different types of books (PaperBook, EBook, ShowcaseBook).

## Prerequisites

- .NET 9.0 SDK or a later version.

## How to Run

You can run this project easily from your terminal using the .NET CLI.

### Clone the repo

```bash
git clone https://github.com/HossamSaberX/FawryQuantumBookstore
cd QuantumBookstore
```
### Run the Project
This command will build and run the application.

```bash
dotnet run
```

## Expected Output

The program executes a test suite that performs several actions: buying books, handling invalid purchase attempts, and cleaning up outdated inventory.

### 1. Successful Book Purchases

The first part of the output shows the successful purchase of a PaperBook and an EBook, including the corresponding shipping/mailing actions and the total price paid.

![Successful Book Purchases](screenshots/sucessful.png)

### 2. Handling Unsupported Book Types

Next, the test attempts to buy a ShowcaseBook, which is not for sale. The program correctly throws and catches an exception, displaying an error message.

![Unsupported Book Purchase Attempt](screenshots/unsupported.png)

### 3. Removing Outdated Books

Finally, the program identifies and removes books older than 50 years from the bookstore's inventory and lists the titles that were removed.

![Removing Outdated Books](screenshots/outdated.png)