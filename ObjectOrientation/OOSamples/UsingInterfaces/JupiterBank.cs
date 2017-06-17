using System;

namespace Wrox.ProCSharp.JupiterBank
{
    public class GoldAccount : IBankAccount
    {
        private decimal _balance;

        public void PayIn(decimal amount) => _balance += amount;

        public bool Withdraw(decimal amount)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                return true;
            }
            Console.WriteLine("Withdrawal attempt failed.");
            return false;
        }

        public decimal Balance => _balance;

        public override string ToString() =>
            $"Venus Bank Saver: Balance = {_balance,6:C}";
    }

    public class CurrentAccount : ITransferBankAccount
    {
        private decimal _balance;

        public void PayIn(decimal amount) => _balance += amount;

        public bool Withdraw(decimal amount)
        {
            if (_balance >= amount)
            {
                _balance -= amount;
                return true;
            }
            Console.WriteLine("Withdrawal attempt failed.");
            return false;
        }

        public decimal Balance => _balance;

        public bool TransferTo(IBankAccount destination, decimal amount)
        {
            bool result = Withdraw(amount);
            if (result)
            {
                destination.PayIn(amount);
            }
            return result;
        }

        public override string ToString() =>
          $"Jupiter Bank Current Account: Balance = {_balance,6:C}";
    }
}