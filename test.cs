using System;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace MyNamespace
{
   public class MyContractService
   {
        private readonly Web3.Web3 web3;
        private string abi = @'[{"constant":false,"inputs":[{"name":"a","type":"int256"}],"name":"multiply","outputs":[{"name":"r","type":"int16"}],"payable":false,"type":"function"},{"inputs":[{"name":"multiplier","type":"int256"},{"name":"another","type":"int256"}],"type":"constructor"},{"anonymous":false,"inputs":[{"indexed":true,"name":"a","type":"int256"},{"indexed":true,"name":"sender","type":"address"},{"indexed":false,"name":"result","type":"int256"}],"name":"Multiplied","type":"event"}]';
        private Contract contract;
        public MyContractService(Web3.Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(abi, address);
        }

        public Function GetFunctionMultiply() {
            return contract.GetFunction('multiply');
        }

        public Event GetEventMultiplied() {
            return contract.GetEvent('Multiplied');
        }

        public async Task<short> MultiplyAsyncCall(BigInteger a) {
            var function = GetFunctionMultiply();
            return function.CallAsync<short>(a);
        }

        public async Task<string> MultiplyAsync(string addressFrom, BigInteger a, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionMultiply();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, a);
        }


    }
}






