# MiniSwitch

#### What are Switches?
In the FinTech space, Switches or Switching Platforms are used to route transactions from one bank to another or from one payment platform to another, a perfect example being Interswitch. This project (Miniswitch) as the name says, is a miniature version of these switches.
Take a scenario where you go to an ATM Terminal belonging to Bank A with your Bank B Debit Card to Withdraw funds. A switch will be used to route that request to Bank B to perform various checks such as funds availability, card status e.t.c. before funds are disbursed to you.

#### Specification of Switches

1. Source Node
2. Sink Node
3. Scheme:
4. Channel
5. Route
6. Fee

#### Source Node
This is basically the source of the transaction. This node contains properties such as IP Address, Hostname, Port and Scheme of every transaction.

#### Source Node
This is the destination of the transaction. This node contains properties such as IP Address, Hostname and Port of every transaction.

#### Scheme
This is the structure of the transaction. Scheme contains properties such as the Transaction Type, Route, Channel and Fee of every transaction.

#### Transaction Type
This is the type of the transaction being performed. Transaction types can be Deposit, Withdrawal, Transfer e.t.c

#### Route
The Route class will include properties like the Card BIN and the sink node which is the destination of the transaction.

#### Channel
The Channel is simply the mode of transaction. These can be either Mobile Banking, ATM, USSD and the likes and each channel can have fees attached to them.

#### How it Works
