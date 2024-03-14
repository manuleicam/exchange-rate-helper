# exchange-rate-helper
## **How to start**  
Run the command `"docker-compose up -d"` in the main directory of the project. This will launch a docker container with a PostGreSQL that's responsible to store all the exchange rates inserted.  
Use your IDE to start the application, and it will automatically open the Swagger page.  

## **Endpoints**

#### All Errors will be returned like
```json
{
    "StatusCode": 400,
    "Message": "ExceptionMessage"
}
```

( **GET** ) http://localhost:5022/ExchangeRate

* This will return all ExchangeRates saved in the database

( **GET** ) http://localhost:5022/ExchangeRate?FromCurrencyCode=USD&ToCurrencyCode=JPY  
**Query Parameters**: FromCurrencyCode && ToCurrencyCode  
**Response**: 200 if sucess with List\<ExchangeRates\> in body  
**Response Body**
```json
{
    "id": "90628acd-0d3a-4634-b4f7-9e17c77c9028",
    "FromCurrency": {
        "Name": "United States Of America",
        "Code": "USD"
    },
    "ToCurrency": {
        "Name": "Japan",
        "Code": "JPY"
    },
    "Rate": 1.1,
    "BidPrice": 1.1,
    "AskPrice": 1.1
}
```

WorkFlow:  
* It will look for exchange rates with the Codes passed in the parameters
    * If you only pass the _FromCurrencyCode_ it will return all exchange rates with that have this Currency Code in the _FromCurrency_
    * If you only pass the _ToCurrencyCode_ it will return all exchange rates with that have this Currency Code in the _ToCurrency_
    * If both are filled (_FromCurrencyCode_ and _FromCurrencyCode_) It will return the exchange rate with them.
        * If it does not find any in the local database it will look in the https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE API


( **POST** ) http://localhost:5022/ExchangeRate 
**Response**: 201 if Created with success with a URL to request the GET in the header  
**Request Body**:
```json
{
    "FromCurrency": {
        "Name": "United States Of America",
        "Code": "USD"
    },
    "ToCurrency": {
        "Name": "Japan",
        "Code": "JPY"
    },
    "Rate": 1.1,
    "BidPrice": 1.1,
    "AskPrice": 1.1
}
```
**Rules**:
* For this endpoint the currencies can't be the same or you will receive a 404 Bad Request.
* The name cannot be more than 50 chars and the code cannot be more than 10.

( **PUT** ) http://localhost:5022/ExchangeRate/{ExchangeRate:GUID}  
**Response**: 204 Because there is no content to responde  
**Request Body**:
```json
{
    "id": "90628acd-0d3a-4634-b4f7-9e17c77c9028",
    "FromCurrency": {
        "Name": "United States Of America",
        "Code": "USD"
    },
    "ToCurrency": {
        "Name": "Japan",
        "Code": "JPY"
    },
    "Rate": 1.1,
    "BidPrice": 1.1,
    "AskPrice": 1.1
}
```
**Rules**:
* For this endpoint the currencies can't be the same or you will receive a 404 Bad Request.
* The name cannot be more than 50 chars and the code cannot be more than 10.
* The Id from the URL Path must belong to a Exchange Rate with the same name and code in both currencies as the on sent in the body.

( **DELETE** ) http://localhost:5022/ExchangeRate/{ExchangeRate:GUID}  
**Response**: 204 Because there is no content to responde  

## **Future Work**

* Add a self update exchange rates
    * By adding a Kafka consumer capable of consuming events that represent updates in other databases;
    * Everytime a user doest a GET it will check how old is the record, and if it's old enough will go get a recent record from a Live database.
* Improve logging.
* Improve the test setup with integration tests.