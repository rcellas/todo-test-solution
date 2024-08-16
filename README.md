# TodoApp

TodoApp es una aplicación de lista de tareas (Todo List) desarrollada en .NET que permite a los usuarios gestionar sus tareas de manera eficiente. La aplicación incluye una funcionalidad adicional para adjuntar imágenes a las tareas, permitiendo una mejor organización y contexto visual.

El apartado del frontend de la aplicación la puedes visitar en el siguiente enlace: [TodoApp Frontend](https://github.com/rcellas/todo-list-angular-v18)

## Características

- **Gestión de Tareas**: Crear, leer, actualizar y eliminar tareas.
- **Adjuntar Imágenes**: Adjunta imágenes a las tareas para proporcionar contexto adicional.

## Uso
Agregar una nueva tarea:

- Haz clic en "Agregar Tarea" y completa los campos de nombre, descripción, fecha límite, y selecciona una imagen si deseas adjuntar una.
Ver/Editar una tarea:

- Haz clic en cualquier tarea de la lista para ver detalles, editar información o agregar/quitar imágenes.


Eliminar una tarea:

- Haz clic en el ícono de la papelera para eliminar una tarea.

## Requisitos

- **.NET SDK**: .NET 8.0 o superior
- **IDE recomendado**: Visual Studio 2022 o Visual Studio Code
- **Dependencias adicionales**:
    - **Entity Framework Core**: Para la gestión de la base de datos.
    - **AutoMapper**: Para la conversión de objetos entre capas.
    - **FluentValidation**: Para la validación de entradas de usuario.

## Instalación

### Clonar el repositorio

Clona el repositorio a tu máquina local usando el siguiente comando:

```bash
git clone https://github.com/usuario/todoapp.git
```

## Restaurar dependencias
Navega al directorio del proyecto y ejecuta el siguiente comando para restaurar las dependencias:

```bash
cd todoapp
dotnet restore
```

## Configurar la base de datos

La aplicación utiliza una base de datos para almacenar las tareas y las imágenes adjuntas. Para configurar la base de datos haremos lo siguiente:

1. Configura la cadena de conexión a tu base de datos en el archivo appsettings.json:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TodoApp;Trusted_Connection=True;"
  }
}
```

2. Ejecuta las migraciones para crear la base de datos:

```bash
dotnet ef database update
```

## Configurar el servidor de imágenes

Para almacenar las imágenes adjuntas a las tareas, la aplicación necesita un servidor de almacenamiento de archivos. Puedes configurar un servidor local o utilizar un servicio de almacenamiento en la nube como AWS S3.

Para configurar un servidor en la nube de AWS S3, sigue estos pasos:

1. Crea un bucket de S3 en tu cuenta de AWS.
2. Configura las credenciales de AWS en el archivo appsettings.json:

```json
"AWS": {
    "BucketName": "your-s3-bucket-name",
    "AccessKey": "your-access-key",
    "SecretKey": "your-secret-key",
    "Region": "your-region"
  },
```

## Ejecutar la aplicación
Compila y ejecuta la aplicación con los siguientes comandos:

```bash
dotnet build
dotnet run
```

O simplemente, si tienes instalado Visual Studio o Rider puedes abrir el proyecto y ejecutarlo desde el IDE dandole al botón de "Run".



