# Investment Funds API

### Getting Started

1. Clonar el repositorio
```
 git clone {urlRepositorio}
```

2. Asegúrate de que el sevicio de MongoDB esté encendido.
3. Puedes correr la aplicación como quieras. ya sea en Visual Studio o con el comando
``` 
dotnet run
```
La aplicación viene configurada para conectarse a "mongodb://localhost:27017/", también está configurada para que al iniciarse cree la db y las collecciones además de seedings para poblarlas.

### Environment Variables

#### ConnectionStrings

| Nombre        | Tipo   | Descripción                      |
|---------------|--------|----------------------------------|
| MongoDb       | String | cadena de conexión a mongoDB       |

#### DatabaseSettings

| Nombre            | Tipo   | Descripción                      |
|-------------------|--------|----------------------------------|
| DatabaseName      | String | Nombre de la base de datos                    |
| Collections       | Object | Nombres de las colecciones |

##### Collections

| Nombre            | Tipo   | Descripción                      |
|-------------------|--------|----------------------------------|
| InvestmentFund    | String | investmentFund                    |
| Investor          | String | investor                          |
| Subscription      | String | subscription                      |
| Transaction       | String | transaction                       |

### Endpoints

### Models

###### InvestmentFund
| Propiedad          | Tipo   | Descripción                         |
|-----------------|--------|-------------------------------------|
| id              | GUID   | Identificador de fondo de inversión |
| name            | String | Nombre del fondo de inversión            |
| minimumPayment  | Number | Mínimo de pago para inscribirse                              |
| category        | String | Categoría del fondo de inversión                                 |
| state           | String | Estado del fondo de inversión hacia el inversor (Open o Subscribed) osea Abierto para inscribirse o inscrito.                                |

###### Subscription
| Nombre                | Tipo   | Descripción                                    |
|-----------------------|--------|------------------------------------------------|
| id                    | GUID   | Identificador de la inscripción           |
| investmentFundDetails | InvestmentFund | Fondo de inversión asociado             |
| amountPayment         | Number | Cantidad invertida en la inscripción                                          |

###### Transaction

| Nombre                | Tipo   | Descripción                                    |
|-----------------------|--------|------------------------------------------------|
| id                    | GUID   | Identificador de la transacción           |
| investmentFundId      | GUID   | Identificador del fondo de inversiones          |
| date                  | Date   | Fecha en la que se hizo                       |
| amountPayment         | Number | Cantidad pagada o devuelta según el tipo                                          |
| type                  | String | Tipo de log (Subscription o Cancelation)                                   |
| investmentFundDetails | InvestmentFund | Fondo de inversión asociado             |

#### /api/investor/{id} - GET

##### Query Params

| Nombre | Tipo | Descripción |
|----|----|----|
| id | GUID | Id del inversor |

##### Responses

#### 200
| Propiedad | Ejemplo | Descripción |
|--|--|--|
| Amount | decimal | Cantidad de dinero actual del inversor |

Ejemplo
```
{
    "amount": 500000
}
```

#### 500

Ejemplo
```
Something was wrong.
```

#### /api/investmentfound/ - GET


##### Responses                         

#### 200
Lista de  Lista de InvestmentFund.

Ejemplo
```
[
    {
        "id": "d1d6deba-f0c6-4a20-a453-747954ed75a0",
        "name": "FPV_BTG_PACTUAL_RECAUDADORA",
        "minimumPayment": 75000,
        "category": "FPV",
        "state": "Subscribed"
    },
    {
        "id": "071c3607-ca27-4289-bc2f-b07c9d438de4",
        "name": "FPV_BTG_PACTUAL_ECOPETROL",
        "minimumPayment": 125000,
        "category": "FPV",
        "state": "Open"
    },
    {
        "id": "483478ce-907c-4b11-bfa1-cd2236daa573",
        "name": "DEUDAPRIVADA",
        "minimumPayment": 50000,
        "category": "FIC",
        "state": "Open"
    },
    {
        "id": "6431590d-ec45-4dab-8ee6-ff0a96086398",
        "name": "FDO-ACCIONES",
        "minimumPayment": 250000,
        "category": "FIC",
        "state": "Open"
    },
    {
        "id": "fda4bba6-be1b-49c8-ba60-5e693aafb1b7",
        "name": "FPV_BTG_PACTUAL_DINAMICA",
        "minimumPayment": 100000,
        "category": "FPV",
        "state": "Open"
    }
]
```

#### 500

Ejemplo
```
Something was wrong.
```

#### /api/investmentfound/{id} - GET


##### Query Params
| Nombre | Tipo | Descripción |
|----|----|----|
| id | GUID | Id del inversor |

##### Responses                             |

#### 200
Objeto InvestmentFund

Ejemplo
```
{
    "id": "d1d6deba-f0c6-4a20-a453-747954ed75a0",
    "name": "FPV_BTG_PACTUAL_RECAUDADORA",
    "minimumPayment": 75000,
    "category": "FPV",
    "state": "Subscribed"
}
```

#### 404

Ejemplo
```
Investment found d1d6deba-f0c6-4a20-a453-747954ed75a0 was not found.
```

#### 500

Ejemplo
```
Something was wrong.
```

#### /api/subscription - GET

##### Responses

#### 200
Lista de Subscription

Ejemplo
```
[
    {
        "id": "18472af2-88d6-49fb-851c-0256e7d6fc4a",
        "investmentFundDetails": {
            "id": "d1d6deba-f0c6-4a20-a453-747954ed75a0",
            "name": "FPV_BTG_PACTUAL_RECAUDADORA",
            "minimumPayment": 75000,
            "category": "FPV",
            "state": "Subscribed"
        },
        "amountPayment": 80000
    }
]
```


#### 500

Ejemplo
```
Something was wrong.
```

#### /api/subscription - POST

### Body

| Nombre            | Tipo   | Descripción                          |
|-------------------|--------|--------------------------------------|
| investorId        | GUID   | Identificador del inversor |
| investmentFundId  | GUID   | identificador del fondo de inversión |
| amountPayment     | Number | Dinero pagado para inscripción                                |

#### Ejemplo

```
{
    "investorId": "8d4ad04f-d9a5-414d-88c2-7f8d806ef119",
    "investmentFundId": "1888d0-71efe3-4735-a729-a76dd517fe5d",
    "amountPayment": 60000
}
```

##### Responses

#### 201
Creación de inscripción, cambia de estado el fondo de inversión y hace el respectivo descuento en la cantidad de dinero del inversor.

Ejemplo
```
User 1888d0-71efe3-4735-a729-a76dd517fe5d has been subscribed to {request.InvestmentFundId} InvestmentFund with a payment 600000000
```

#### 400
El inversor no tiene dinero suficiente para hacer el pago
Ejemplo
```
You can't subscribe to FPV_BTG_PACTUAL_RECAUDADORA. Investor amount is less than the amount payment.
```

#### 400
El inversor no tiene dinero suficiente ni para un pago mínimo.
Ejemplo
```
You can't subscribe to FPV_BTG_PACTUAL_RECAUDADORA. AmountPayment is less than the minimum payment required.
```

#### 404

Ejemplo
```
Investment found d1d6deba-f0c6-4a20-a453-747954ed75a0 was not found.
```

#### 500

Ejemplo
```
Something was wrong.
```

#### /api/subscription - DELETE

### Body

| Nombre            | Tipo   | Descripción                          |
|-------------------|--------|--------------------------------------|
| investorId        | GUID   | Identificador del inversor |
| investmentFundId  | GUID   | identificador del fondo de inversión |

#### Ejemplo

```
{
    "investorId": "8d4ad04f-d9a5-414d-88c2-7f8d806ef119",
    "investmentFundId": "171888d0-efe3-4735-a729-a76dd517fe5d"
}
```

##### Responses

#### 200
cancela la inscripción, cambia de estado el fondo de inversión y hace el respectivo retorno en la cantidad de dinero del inversor.

Ejemplo
```
User 1888d0-71efe3-4735-a729-a76dd517fe5d has been canceled to subscription 1888d0-713e3-4435-a729-a76d434517fe5d
```

#### 400
El inversor no tiene dinero suficiente para hacer el pago
Ejemplo
```
You can't subscribe to FPV_BTG_PACTUAL_RECAUDADORA. Investor amount is less than the amount payment.
```

#### 400
El inversor no tiene dinero suficiente ni para un pago mínimo.
Ejemplo
```
You can't subscribe to FPV_BTG_PACTUAL_RECAUDADORA. AmountPayment is less than the minimum payment required.
```

#### 404

Ejemplo
```
Investment found d1d6deba-f0c6-4a20-a453-747954ed75a0 was not found.
```

#### 500

Ejemplo
```
Something was wrong.
```

#### /api/transaction - GET

##### Responses

#### 200
Lista de Transaction

Ejemplo
```
[
    {
        "id": "be12f58e-7662-4f00-b028-d6f909395c02",
        "investmentFundId": "d1d6deba-f0c6-4a20-a453-747954ed75a0",
        "date": "2024-08-15T14:12:29.418Z",
        "amountPayment": 80000,
        "type": "Subscription",
        "investmentFundDetails": {
            "id": "d1d6deba-f0c6-4a20-a453-747954ed75a0",
            "name": "FPV_BTG_PACTUAL_RECAUDADORA",
            "minimumPayment": 75000,
            "category": "FPV",
            "state": "Subscribed"
        }
    },
    {
        "id": "a7c1afff-c405-4e3f-b827-fe2caac26089",
        "investmentFundId": "d1d6deba-f0c6-4a20-a453-747954ed75a0",
        "date": "2024-08-15T14:17:47.007Z",
        "amountPayment": 80000,
        "type": "Cancelation",
        "investmentFundDetails": {
            "id": "d1d6deba-f0c6-4a20-a453-747954ed75a0",
            "name": "FPV_BTG_PACTUAL_RECAUDADORA",
            "minimumPayment": 75000,
            "category": "FPV",
            "state": "Subscribed"
        }
    },
    {
        "id": "9ad5a0bc-b564-45cd-ac06-ecd259fb6f2e",
        "investmentFundId": "d1d6deba-f0c6-4a20-a453-747954ed75a0",
        "date": "2024-08-15T14:17:54.517Z",
        "amountPayment": 80000,
        "type": "Cancelation",
        "investmentFundDetails": {
            "id": "d1d6deba-f0c6-4a20-a453-747954ed75a0",
            "name": "FPV_BTG_PACTUAL_RECAUDADORA",
            "minimumPayment": 75000,
            "category": "FPV",
            "state": "Subscribed"
        }
    }
]
```


#### 500

Ejemplo
```
Something was wrong.
```
