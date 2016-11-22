# abi-code-gen

This project ideal is to create a generic code generator using as an input the compilation output of solc, for usage with the solidity extension of vs-code or as an standalone utility.

This way everyone can simply create templates that are easily pluggable without the need to create new parsers.

Currently includes the C# template for a generic service, function output and event dtos for usage with Nethereum.

##Usage

The input expected is a json file as per solc.js compilation output, depending of the code generator template most or noned of the properties will be used.
But as a minimum we need the "abi" as this is parsed by the engine.

```javascript
{
    "abi": "[{\"constant\":false,\"inputs\":[{\"name\":\"a\",\"type\":\"int256\"}],\"name\":\"multiply\",\"outputs\":[{\"name\":\"r\",\"type\":\"int256\"}],\"payable\":false,\"type\":\"function\"},{\"inputs\":[{\"name\":\"multiplier\",\"type\":\"int256\"}],\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"name\":\"a\",\"type\":\"int256\"},{\"indexed\":true,\"name\":\"sender\",\"type\":\"address\"},{\"indexed\":false,\"name\":\"result\",\"type\":\"int256\"}],\"name\":\"Multiplied\",\"type\":\"event\"}]\n",
    "bytecode": "60606040526040516020...",
...
}

```

If there are any other extra settings the code generator template needs, a file matching both the compilation output and the template used can be placed alongside.

For example, if we have "Multiplication.json" compilation output, we can put next to it "Multiplication-cs-service.json" where "cs-service" is our template.

This can include specific attributes like 'namespace' or overriding the 'contractName'.

```javascript
{
 "namespace": "Tutorials"
 "contractName": "MyMultiplicationContract"
}
``` 

To execute the code generation, we just need to pass the path of our file and the template.

```javascript
abigen.generateCode(path.join(__dirname, 'Multiplication.json'), 'cs-service');

```

## Templates

The template engine used is ejs as it allows complex parsing and the same time be fully decoupled from the "engine" itself.

To get a better understanding have a look at the templates directory or the examples.
