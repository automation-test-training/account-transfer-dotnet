using System.Threading.Tasks;
using AccountTransfer;
using Xunit;

namespace AccountTransferTest
{
    public class AccountTransferFact
    {
        private readonly Account payer;
        private readonly Account payee;

        public AccountTransferFact()
        {
            //Given
            payer = new Account();
            payee = new Account();
        }

        [Fact]
        public void should_transfer_100_from_one_acount_to_another()
        {
            //Given
            payer.TransferIn(100);
            //When
            payer.TransferTo(payee, 100m);
            //Then
            Assert.Equal(0, payer.Balance);
            Assert.Equal(100, payee.Balance);
        }

        [Fact]
        public void should_throw_exception_when_transfer_from_one_acount_to_another_and_balance_is_not_enough()
        {
            //Given
            payer.TransferIn(100);
            //When
            Assert.Throws<BanlanceNotEnough>(() => payer.TransferTo(payee, 200m));
            //Then
            Assert.Equal(100, payer.Balance);
            Assert.Equal(0, payee.Balance);
        }

        [Fact]
        public void should_transfer_correctly_in_concurrency()
        {
            //Given
            payer.TransferIn(10000);
            payee.TransferIn(10000);
            //When
            var task1 = Task.Run(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    payer.TransferTo(payee, 100);
                }    
            });
            var task2 = Task.Run(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    payee.TransferTo(payer, 100);
                }    
            });
            //Then
            Task.WaitAll(task1, task2);
            Assert.Equal(10000, payer.Balance);
            Assert.Equal(10000, payee.Balance);
        }
    }

}