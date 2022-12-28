/*create database pruebadb;*/
/*use pruebadb;

create table Usuario(
	id_usuario int primary key identity(1,1),
	usuario varchar(50),
	pass varchar(75)
);

create proc sp_RegistroUsuario(
	@usuario varchar(50),
	@pass varchar(75),
	@success bit output,
	@message varchar(250) output
)
as
begin
	if(not exists(select * from Usuario where usuario = @usuario))
		begin
			insert into Usuario(usuario, pass) values(@usuario, @pass)
			set @success = 1
			set @message='Se registro el usuario'
		end
	else
		begin
			set @success = 0
			set @message = 'El usuario ya existe'
		end
end


create table Encuesta(
	id_encuesta int primary key identity(1,1),
	descripcion varchar(150),
);

create table Campo(
	id_campo int primary key identity(1,1),
	idEncuesta int not null,
	nombre varchar(50),
	titulo varchar(50),
	requerido varchar(50),
	tipo varchar(50),
	constraint fk_encuesta_campo foreign key (idEncuesta) references Encuesta (id_encuesta)
);

create table Valor(
	id_valor int primary key identity(1,1),
	idEncuesta int not null,
	nombre varchar(50),
	valor varchar(250),
	constraint fk_encuesta_valor foreign key (idEncuesta) references Encuesta (id_encuesta)
);


create proc ValidacionUsuario(
	@usuario varchar(50),
	@pass varchar(75))
as
begin
	if(exists(select * from Usuario where usuario=@usuario and pass=@pass))
		select id_usuario from Usuario where usuario=@usuario and pass=@pass
	else
		select '0'
end 
*/