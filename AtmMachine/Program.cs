using System;
using System.Collections.Generic;


//  Author:  JaySon
// jaysonceojaytech@gmail.com


class Program
{
    static Dictionary<string, Account> accounts = new Dictionary<string, Account>();

    static void Main()
    {
        // Create some sample accounts
        accounts.Add("1234567890", new Account("John Doe", "1234", 1000.0));
        accounts.Add("0987654321", new Account("Jane Doe", "5678", 1500.0));

        // ATM loop
        while (true)
        {
            Console.WriteLine("Enter your card number (or 'exit' to quit):");
            string cardNumber = Console.ReadLine();

            if (cardNumber.ToLower() == "exit")
                break;

            if (accounts.ContainsKey(cardNumber))
            {
                Console.WriteLine("Enter your PIN:");
                string pin = Console.ReadLine();

                if (ValidatePin(cardNumber, pin))
                {
                    Console.WriteLine($"Welcome, {accounts[cardNumber].HolderName}!");

                    while (true)
                    {
                        DisplayMenu();

                        string choice = Console.ReadLine();

                        switch (choice)
                        {
                            case "1":
                                Deposit(cardNumber);
                                break;
                            case "2":
                                Withdraw(cardNumber);
                                break;
                            case "3":
                                CheckBalance(cardNumber);
                                break;
                            case "4":
                                Console.WriteLine("Thank you for using our ATM. Goodbye!");
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid PIN. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid card number. Please try again.");
            }
        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\nATM Menu:");
        Console.WriteLine("1. Deposit");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Check Balance");
        Console.WriteLine("4. Exit");
        Console.Write("Enter your choice: ");
    }

    static bool ValidatePin(string cardNumber, string enteredPin)
    {
        return accounts[cardNumber].Pin == enteredPin;
    }

    static void Deposit(string cardNumber)
    {
        Console.Write("Enter the amount to deposit: ");
        if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
        {
            accounts[cardNumber].Balance += amount;
            Console.WriteLine($"Deposit successful. New balance: ${accounts[cardNumber].Balance}");
        }
        else
        {
            Console.WriteLine("Invalid amount. Please enter a valid positive number.");
        }
    }

    static void Withdraw(string cardNumber)
    {
        Console.Write("Enter the amount to withdraw: ");
        if (double.TryParse(Console.ReadLine(), out double amount) && amount > 0)
        {
            if (amount <= accounts[cardNumber].Balance)
            {
                accounts[cardNumber].Balance -= amount;
                Console.WriteLine($"Withdrawal successful. New balance: ${accounts[cardNumber].Balance}");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }
        else
        {
            Console.WriteLine("Invalid amount. Please enter a valid positive number.");
        }
    }

    static void CheckBalance(string cardNumber)
    {
        Console.WriteLine($"Your current balance: ${accounts[cardNumber].Balance}");
    }
}

class Account
{
    public string HolderName { get; }
    public string Pin { get; }
    public double Balance { get; set; }

    public Account(string holderName, string pin, double balance)
    {
        HolderName = holderName;
        Pin = pin;
        Balance = balance;
    }
}
