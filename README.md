# APIDOCKER

Este proyecto es una API de ejemplo desarrollada en .NET 8 utilizando C# y Entity Framework. La API está diseñada para demostrar cómo construir y desplegar una aplicación .NET en un contenedor Docker, así como la interacción con una base de datos SQL Server.

## Descripción del Proyecto

La API se conecta a una base de datos SQL Server, la cual se crea automáticamente si no existe, utilizando la cadena de conexión definida en el `Dockerfile`. En esta base de datos, se crea una tabla llamada `Customer` si no está presente.

### Características

- **Construcción de imagen Docker**: Incluye un `Dockerfile` para crear una imagen que contiene la API.
- **Integración con Docker Hub**: Al hacer un `push` al repositorio de GitHub, la imagen se actualiza automáticamente en Docker Hub mediante GitHub Actions.
- **Base de datos SQL Server**: La base de datos se crea automáticamente y se conecta a la API si no existe.

### Requisitos

- Docker
- SQL Server

## Actualización Automática de la Imagen
Cada vez que realices un push a la rama main del repositorio, GitHub Actions se encargará de construir y actualizar automáticamente la imagen en Docker Hub.

## Contribuciones
Las contribuciones son bienvenidas. Siéntete libre de abrir un issue o enviar un pull request.
