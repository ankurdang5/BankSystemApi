# BankSystem API - AccountsController
This is an ASP.NET Core Web API controller designed to manage bank accounts and implement basic banking operations in line with the specified business requirements.

**Swagger Endpoints**
![image](https://github.com/ankurdang5/BankSystemApi/blob/main/BankApi.png)

- `GET /api/Accounts` - Retrieve a list of all accounts.
- `GET /api/Accounts/GetAccount/{accountId}` - Retrieve details of a specific account.
- `POST /api/Accounts` - Create a new bank account.
- `DELETE /api/Accounts/{accountId}` - Delete an account.
- `POST /api/Accounts/{accountId}/deposit` - Deposit funds into an account.
- `POST /api/Accounts/{accountId}/withdraw` - Withdraw funds from an account.

**Schemas**

![image](https://github.com/RahulGo8u/BankSystemAPI/assets/44201543/d5be147d-4a3f-482f-bf6e-a4f8fa987081)

Here are sample requests for the provided endpoints:

**GET /api/bank** - Retrieve a list of all accounts:
```
Sample Request
GET /api/bank
```

**GET /api/bank/{accountId}** - Retrieve details of a specific account:
```
GET /api/bank/1
```

**POST /api/bank** - Create a new bank account:
```
Content-Type: application/json
{
  "name": "Savings",
  "balance": 5000.00
}
```

**PUT /api/bank/{accountId}** - Update an existing account:
```
PUT /api/bank/1
Content-Type: application/json
{
  "name": "Updated Savings",
  "balance": 5500.00
}
```

**DELETE /api/bank/{accountId}** - Delete an account:
```
DELETE /api/bank/1
```

**POST /api/bank/{accountId}/deposit** - Deposit funds into an account:
```
POST /api/bank/1/deposit
Content-Type: application json
{
  "amount": 1000.00
}
```

**POST /api/bank/{accountId}/withdraw** - Withdraw funds from an account:
```
POST /api/bank/1/withdraw
Content-Type: application/json

{
  "amount": 500.00
}
```
