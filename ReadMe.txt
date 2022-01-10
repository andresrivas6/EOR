Para el desarrollo de este desafío utilice .Net Core 3.1, con las librerías de Entity Framework Core para la conexión a la base de datos.
Es una solución de desarrollo Web MVC al que le adicioné una capa de Infraestructura donde manejo las interfaces y los servicios 
que son los encargados de realizar las transacciones en la base de datos.
Por lo anterior también utilice Injección de Dependencias para poder utilizar los servicios en los controladores.

La base de datos está montada en SQL SERVER 15.0.2000 RTM

los usuarios fueron ingresados de manera manual por medio del siguiente script:
insert into usuario (nombre, "user", pass, esAdmin, creadoPor, fechaCreado) values('Administrador','admin',
ENCRYPTBYPASSPHRASE('123eor321','eor2022'),1
,'system',getdate())

insert into usuario (nombre, "user", pass, esAdmin, creadoPor, fechaCreado) values('Francisco','fran',
ENCRYPTBYPASSPHRASE('123eor321','fran2022'),1
,'system',getdate())

insert into usuario (nombre, "user", pass, esAdmin, creadoPor, fechaCreado) values('Andres','andre',
ENCRYPTBYPASSPHRASE('123eor321','andre2022'),0
,'system',getdate())

Como se observa la clave está encriptada, y utilice el hash "123eor321" para encriptar y desencriptar con el método que Sql Server tiene.

Actualmente existen 3 usuario
usuario:    admin
Contraseña: admin2022

usuario:    fran
Contraseña: fran2022

usuario:    andre
Contraseña: andre2022
