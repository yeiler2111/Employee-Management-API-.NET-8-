# Ejecución del Backend .NET 8

Este documento describe los pasos necesarios para ejecutar el proyecto de backend desarrollado en .NET 8, incluyendo la configuración de la base de datos, la ejecución de migraciones y pruebas unitarias, así como la creación de un procedimiento almacenado.

## Prerrequisitos

Asegúrate de tener instalado lo siguiente en tu sistema:

* **.NET SDK 8.0**: Puedes descargarlo desde el sitio oficial de [.NET](https://dotnet.microsoft.com/download/dotnet/8.0).
* **Una instancia de SQL Server**: Necesitarás acceso a una instancia de SQL Server (local o de producción) para la base de datos del proyecto.

## Pasos de Ejecución

1.  **Clonar el Repositorio (Opcional)**

    Si aún no tienes el código fuente, clona el repositorio:

    ```bash
    git clone <URL_DEL_REPOSITORIO>
    cd <NOMBRE_DEL_PROYECTO>
    ```

2.  **Instalar Dependencias**

    Ejecuta el siguiente comando para restaurar las dependencias del proyecto:

    ```bash
    dotnet restore
    ```

3.  **Construir el Proyecto**

    Compila el proyecto .NET utilizando el siguiente comando:

    ```bash
    dotnet build
    ```

4.  **Configuración de la Base de Datos**

    Este proyecto utiliza autenticación de Windows para la conexión a la base de datos. Deberás configurar la cadena de conexión en el archivo de configuración de la aplicación (`appsettings.json` o `appsettings.Development.json`, según tu entorno).

    Asegúrate de que la cadena de conexión apunte a una instancia de SQL Server a la que tu cuenta de Windows tenga acceso.

    Ejemplo de una posible cadena de conexión:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=.;Database=NombreDeTuBaseDeDatos;Integrated Security=True;TrustServerCertificate=True"
      }
    }
    ```

    **Importante:** Reemplaza `NombreDeTuBaseDeDatos` con el nombre real de tu base de datos.

5.  **Crear la Tabla de Empleados y Aplicar Migraciones**

    Para generar la tabla `Empleados` (si aún no existe) y aplicar cualquier otra migración de Entity Framework, ejecuta el siguiente comando en la raíz del proyecto:

    ```bash
    dotnet ef database update
    ```

    Este comando creará la base de datos (si no existe) y aplicará todas las migraciones pendientes.

6.  **Creación del Procedimiento Almacenado `GetEmployeesHiredAfter`**

    Este proyecto utiliza un procedimiento almacenado llamado `GetEmployeesHiredAfter` para obtener empleados contratados después de una fecha específica. Para crearlo, ejecuta el siguiente script SQL en tu base de datos SQL Server:

    ```sql
    CREATE PROCEDURE GetEmployeesHiredAfter
        @HireDate DATETIME
    AS
    BEGIN
        SELECT EmployeeId, FirstName, LastName, Email, Phone, HireDate
        FROM Employees
        WHERE HireDate > @HireDate
    END
    ```

    Utiliza una herramienta como SQL Server Management Studio (SSMS) o Azure Data Studio para conectar a tu base de datos y ejecutar este script.

7.  **Ejecutar la Aplicación**

    Finalmente, ejecuta la aplicación de backend con el siguiente comando:

    ```bash
    dotnet run
    ```

    Esto iniciará el servidor de backend.

## Ejecución de Pruebas Unitarias

El proyecto incluye pruebas unitarias para verificar la funcionalidad del backend. Para ejecutarlas, abre la terminal en la raíz del proyecto (o en la carpeta de los proyectos de prueba) y ejecuta:

```bash
dotnet test
```

## 🏗️ Arquitectura de la API y Decisiones Técnicas

La API se ha estructurado siguiendo una **arquitectura modular en capas** (Models, Repository, Services, Controllers) 
con el objetivo de **separar responsabilidades** y facilitar el **mantenimiento** y la **escalabilidad**. Si bien para 
la escala actual del proyecto una arquitectura tan elaborada podría considerarse excesiva, se implementó con la intención 
de **demostrar un enfoque organizado y una preferencia por las buenas prácticas de desarrollo**.

* **Models**: Definen las entidades del dominio, manteniéndose independientes de la lógica de persistencia.
* **Repository**: Abstrae el acceso a datos, promoviendo el desacoplamiento de la base de datos (Entity Framework Core).
* **Services**: Contiene la lógica de negocio, coordinando las operaciones de los repositorios.
* **Controllers**: Gestionan las solicitudes y respuestas HTTP.

El uso de **DTOs** optimiza la transferencia de datos, y las carpetas `Config` y `ModelConfiguration` organizan la configuración de la base de datos y los modelos de forma separada.

Esta estructura, aunque quizás detallada para el tamaño actual, refleja una **metodología de trabajo organizada** y la intención de construir una base sólida y adaptable para el futuro.e**.

## Como puedo consumir los endpoint?
en el repo hay un archivo llamado **guia consumo endpoins** esto lo importas en postman. inmediatamente podras apreciar como se consumen.