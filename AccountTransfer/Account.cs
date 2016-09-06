namespace AccountTransfer
{
    public class Account {
        private static object obj = new object();
        public decimal Balance { get; private set; }

        public void TransferIn(decimal amount)
        {
            Balance += amount;
        }
        public decimal TransferOut(decimal amount)
        {
            if (amount > Balance)
            {
                throw new BanlanceNotEnough();
            }
            Balance -= amount;
            return amount;
        }

        public void TransferTo(Account payee, decimal amount)
        {
            lock (obj)
            {
                TransferOut(amount);
                payee.TransferIn(amount); 
            }
        }
    }

}