using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;

namespace Tutorials
{
   public class MultiplicationService
   {
        private readonly Web3 web3;

        public static string ABI = @"[{'constant':false,'inputs':[{'name':'a','type':'int256'}],'name':'multiply','outputs':[{'name':'r','type':'int256'}],'payable':false,'type':'function'},{'inputs':[{'name':'multiplier','type':'int256'}],'type':'constructor'},{'anonymous':false,'inputs':[{'indexed':true,'name':'a','type':'int256'},{'indexed':true,'name':'sender','type':'address'},{'indexed':false,'name':'result','type':'int256'}],'name':'Multiplied','type':'event'}]";

        public static string BYTE_CODE = "0x606060405260405160208060b983395060806040525160008190555060928060276000396000f3606060405260e060020a60003504631df4f1448114601c575b6002565b346002576080600435600054604080519183028083529051909173ffffffffffffffffffffffffffffffffffffffff33169184917f841774c8b4d8511a3974d7040b5bc3c603d304c926ad25d168dacd04e25c4bed919081900360200190a3919050565b60408051918252519081900360200190f3";

        public static Task<string> DeployContractAsync(Web3 web3, string addressFrom, BigInteger multiplier, HexBigInteger gas = null, HexBigInteger valueAmount = null) 
        {
            return web3.Eth.DeployContract.SendRequestAsync(ABI, BYTE_CODE, addressFrom, gas, valueAmount , multiplier);
        }

        private Contract contract;

        public MultiplicationService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionMultiply() {
            return contract.GetFunction("multiply");
        }

        public Event GetEventMultiplied() {
            return contract.GetEvent("Multiplied");
        }

        public Task<BigInteger> MultiplyAsyncCall(BigInteger a) {
            var function = GetFunctionMultiply();
            return function.CallAsync<BigInteger>(a);
        }

        public Task<string> MultiplyAsync(string addressFrom, BigInteger a, HexBigInteger gas = null, HexBigInteger valueAmount = null) {
            var function = GetFunctionMultiply();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, a);
        }



    }


    public class MultipliedEventDTO 
    {
        [Parameter("int256", "a", 1, true)]
        public BigInteger A {get; set;}

        [Parameter("address", "sender", 2, true)]
        public string Sender {get; set;}

        [Parameter("int256", "result", 3, false)]
        public BigInteger Result {get; set;}

    }


}

