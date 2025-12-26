using System.Collections.Generic;

namespace task_4_
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount < 0)
                return false;
            else
            {
                Balance += amount;
                return true;
            }
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    public class SavingsAccount : Account
    {
        public double InterestRate { get; set; }

        public SavingsAccount(string name = "Unnamed Savings Account", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance)
        {
            InterestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            amount = amount + (amount * InterestRate / 100);
            return base.Deposit(amount);
        }
    }
    public class CheckingAccount : Account
    {
        public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0)
            : base(name, balance) { }
        public override bool Withdraw(double amount)
        {
            amount = amount + 1.50;
            return base.Withdraw(amount);
        }
    }
    public class TrustAccount : SavingsAccount
    {
        private int withdrawalCount = 0;

        public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double interestRate = 0.0)
            : base(name, balance, interestRate)
        {
        }

        public override bool Deposit(double amount)
        {
            if (amount >= 5000)
            {
                amount += 50;
            }
            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            if (withdrawalCount >= 3 || amount > (Balance * 0.20))
            {
                return false;
            }

            if (base.Withdraw(amount))
            {
                withdrawalCount++;
                return true;
            }
            return false;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<SavingsAccount>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.DepositSavings(savAccounts, 1000);
            AccountUtil.WithdrawSavings(savAccounts, 2000);

            // Checking
            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.DepositChecking(checAccounts, 1000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);
            AccountUtil.WithdrawChecking(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<TrustAccount>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.DepositTrust(trustAccounts, 1000);
            AccountUtil.DepositTrust(trustAccounts, 6000);
            AccountUtil.WithdrawTrust(trustAccounts, 2000);
            AccountUtil.WithdrawTrust(trustAccounts, 3000);
            AccountUtil.WithdrawTrust(trustAccounts, 500);

            Console.WriteLine();
        }

        public static class AccountUtil
        {
            // Utility helper functions for Account class
            public static void Deposit(IEnumerable<Account> accounts, double amount)
            {
                Console.WriteLine("\n=== Depositing to Accounts =================================");
                foreach (var acc in accounts)
                {
                    if (acc.Deposit(amount))
                        Console.WriteLine($"Deposited {amount} to {acc}");
                    else
                        Console.WriteLine($"Failed Deposit of {amount} to {acc}");
                }
            }

            public static void Withdraw(IEnumerable<Account> accounts, double amount)
            {
                Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
                foreach (var acc in accounts)
                {
                    if (acc.Withdraw(amount))
                        Console.WriteLine($"Withdrew {amount} from {acc}");
                    else
                        Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
                }
            }

            public static void DepositSavings(List<SavingsAccount> accounts, double amount) => Deposit(accounts, amount);
            public static void WithdrawSavings(List<SavingsAccount> accounts, double amount) => Withdraw(accounts, amount);
            public static void DepositChecking(List<CheckingAccount> accounts, double amount) => Deposit(accounts, amount);
            public static void WithdrawChecking(List<CheckingAccount> accounts, double amount) => Withdraw(accounts, amount);
            public static void DepositTrust(List<TrustAccount> accounts, double amount) => Deposit(accounts, amount);
            public static void WithdrawTrust(List<TrustAccount> accounts, double amount) => Withdraw(accounts, amount);
        }
    }

}

