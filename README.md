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

![image](https://github.com/ankurdang5/BankSystemApi/blob/main/user.png)

Here are sample requests for the provided endpoints:

**GET /api/Accounts** - Retrieve a list of all accounts:
```
Sample Request
GET /api/Accounts
```

**GET /api/Accounts/{accountId}** - Retrieve details of a specific account:
```
GET /api/bank/1
```

**POST /api/Accounts** - Create a new bank account:
```
Content-Type: application/json
{
  "name": "Savings",
  "balance": 5000.00
}
```

**DELETE /api/Accounts/{accountId}** - Delete an account:
```
DELETE /api/Accounts/1
```

**POST /api/Accounts/{accountId}/deposit** - Deposit funds into an account:
```
POST /api/Accounts/1/deposit
Content-Type: application json
{
  "amount": 1000.00
}
```

**POST /api/Accounts/{accountId}/withdraw** - Withdraw funds from an account:
```
POST /api/Accounts/1/withdraw
Content-Type: application/json

{
  "amount": 500.00
}
```
