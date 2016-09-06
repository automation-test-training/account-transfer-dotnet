using AccountTransfer;
using Xunit;

namespace AccountTransferTest
{
    public class AccountFact
    {
        private readonly Account account;

        public AccountFact()
        {
            //Given
            account = new Account();
        }

        [Fact]
        public void should_transfer_100_in_an_account()
        {
            //When
            account.TransferIn(100m);
            //Then
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void should_transfer_in_200_to_an_account()
        {
            //When
            account.TransferIn(200);
            //Then
            Assert.Equal(200, account.Balance);
        }

        [Fact]
        public void should_transfer_in_100_two_times_in_an_account()
        {
            //When
            account.TransferIn(100m);
            account.TransferIn(100m);
            //Then
            Assert.Equal(200, account.Balance);
        }

        [Fact]
        public void should_tranfer_out_100_from_an_account()
        {
            //Given
            account.TransferIn(200);
            //When
            var cash = account.TransferOut(100m);
            //Then
            Assert.Equal(100, cash);
            Assert.Equal(100, account.Balance);
        }

        [Fact]
        public void should_throw_exception_when_transfer_out_account_and_balance_is_not_enough()
        {
            //When
            account.TransferIn(100);
            //Then
            Assert.Throws<BanlanceNotEnough>(()=>account.TransferOut(200));
            Assert.Equal(100, account.Balance);
        }
    }
}