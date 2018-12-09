# Checkout CLI

## Requirements

* Requires .NET Framework v4.6.1
* Visual Studio 2017

## Building
The application can be built in Visual Studio 2017.

## Usage

The Checkout takes a list of store keeping units as arguments and prints the total cost.

Example:

```
Checkout.exe A
```

Which will produce:

```
Receipt
-------
Total: £50.00
```

Multiple store keeping units can also be passed.

Example:

```
Checkout.exe AAABB
```

Which will produce:

```
Receipt
-------
Total: £175.00
```

Multiple orders can also be passed by supplying multiple arguments.

Example:

```
Checkout.exe AAABB ABC CBA
```

Which will produce:

```
Receipt
-------
Total: £175.00

Receipt
-------
Total: £100.00

Receipt
-------
Total: £100.00
```

# Errors
Invalid formatted strings will be reported by the CLI.

Example:
```
Checkout.exe A-A-A-A
```
Which will produce:

```
Your order could not be processed as the following order: A-A-A-A is not in a valid format.
Your order should be a string of store keeping units containing no whitespace or delimeters, for example: A, AA or ABB
```

Products that are not found will also be reported to the user.

Example:

```
Checkout.exe Z
```

Which will produce:
```
Could not process order for the following reason: 'Z' is not a known product. Please contact your store manager.
```