create table Zona
  (
  id_zona serial primary key,
  nombre_zona varchar(20) not null,
  ubicacion_zona varchar(20) not null,
  cod_sede integer not null
);
create table Lugar
  (
    id_lugar serial primary key,
    nombre_lugar varchar(20) not null,
    tipo_lugar varchar(2) not null,
    lugar_per integer
  );
create table Beneficiario
  (
    id_bene serial primary key,
    nombre_bene varchar(20) not null,
    apelldido_bene varchar(20) not null,
    id_lugar integer not null,
    cod_personal integer not null
  );
create table Correo
  (
    mail varchar (30) primary key,
    id_cliente integer not null
  );
create table Web
  (
  url varchar(30) primary key,
  id_proveedor integer not null
);
create table Red_social
  (
    plataforma varchar(20) primary key,
    usuario varchar(20) not null,
    cod_personal integer not null
  );
create table Personal
  (
      id_personal serial primary key,
      nombre_personal varchar(20) not null,
      apellido_personal varchar(20) not null,
      nombre2_personal varchar(20),
      apellido2_personal varchar(20),
      fechainicio date not null,
      fechafin date,
      experiencia varchar(200),
      id_lugar integer not null
  );
create table Rol
  (
    id_rol serial primary key,
    nombre_rol varchar(20) not null
  );
create table Usuario
  (
    id_usuario serial primary key,
    usuario varchar not null,
    pass varchar(20) not null,
    cod_rol integer not null
  );
create table Privilegio
  (
    id_privilegio serial primary key,
    nombre_privilegio serial

  );
create table Solicitud
  (
    nro_solicitud serial primary key,
    fechasol date not null,
    observ varchar(80),
    id_cliente integer not null
  );
create table Pieza(
  cod_pieza serial primary key,
  nombre_pieza varchar(50) not null,
  desc_pieza varchar (80) not null

);
create table Inventario(
  cod_inv serial primary key,
  cant int not null,
  fechainv date not null,
  obser varchar(80),
  cod_material integer not null
);
create table Material(
  cod_material serial primary key,
  nombre varchar(50) not null,
  descrip varchar (80) not null,
  cant_teo int not null
);
create table Funcion(
  id_funcion serial primary key,
  nombref varchar(50) not null,
  descrf varchar(80),
  cod_sede integer not null
);
create table Proveedor(
  id_proveedor serial primary key,
  nombre varchar(50) not null,
  fechainic date not null
);
create table Avion(
  cod_avion serial primary key,
  nombre varchar(50)not null,
  id_modelo integer not null,
  cod_ensamav integer not null

);
create table Orden_compra(
  id_orden serial primary key,
  fecha date not null,
  observ varchar(80),
  id_proveedor integer not null,
  cod_material integer not null
);
create table Prueba(
  cod_prueba serial primary key,
  nombre_prueb varchar(50) not null,
  descrip_prue varchar (80) not null
);
create table Sede(
  cod_sede serial primary key,
  nombre_sede varchar(50) not null
);
create table Equipo
  (
    cod_equipo serial primary key,
    cod_personal integer not null,
    id_zona integer not null

  );
create table Telefono
  (
  cod_telf serial primary key,
    cod_area integer not null,
    numerotelf integer not null,
    cod_personal integer not null,
    id_proveedor integer not null,
    id_bene integer not null,
    id_cliente integer not null
  );
create table Rol_Priv
  (
    id_rp serial primary key,
    cod_rol integer not null,
    id_privilegio integer not null
  );
create table Proveedor_Material
  (
    id_pvm serial primary key,
    id_proveedor integer not null,
    cod_material integer not null
  );
create table Pago
    (
      id_pago serial primary key,
      monto numeric(10,2),
      id_orden integer not null,
      id_tipopago integer not null
    );
create table Ensam_Pieza
  (
    cod_ensampi serial primary key,
    fechainic date not null,
    fechafin date,
    cod_pieza integer not null,
    cod_equipo integer not null

  );
create table Tipo_Pago
  (
  id_tipopago serial primary key,
  nombre_pago varchar(20),
  nro_tarjeta integer,
  fecha_venci date,
  titular varchar(40),
  tipotdc varchar(20),
  cod_seg integer,
  titular_cheque integer,
  nrotrans integer,
  banco varchar(40),
  titular_transf varchar(40)
  );
create table Cliente
  (
  id_cliente serial primary key,
  nombre_cliente varchar(20),
  montoac numeric(10,2),
  fechaini date,
  dni integer unique,
  apellido varchar(20),
  rif integer
  );
create table Estatus
  (
    id_status serial primary key,
    nombre_status varchar(20),
    tipo_status varchar(20)
  );
create table Modelo(
    id_modelo serial primary key,
    tripulacion integer,
    capacidad integer,
    dist_as numeric(10,2),
    ancho_as numeric(10,2),
    longitud numeric(10,2),
    envergadura numeric(10,2),
    an_fuselaje numeric(10,2),
    alto_fuselaje numeric(10,2),
    ancho_cabina numeric(10,2),
    alto_cabina numeric(10,2),
    peso_vacio numeric(10,2),
    peso_maxd numeric(10,2),
    peso_maxa numeric(10,2),
    volumenca numeric(10,2),
    cardespegue numeric(10,2),
    techoserv numeric(10,2),
    velo_crucero numeric(10,2),
    velomax numeric(10,2),
    alcancemax numeric(10,2),
    maxcomb numeric(10,2),
    motor varchar(80),
    empuje numeric(10,2),
    diame_ala numeric(10,2),
    long_motor numeric(10,2)
  );
create table Pieza_prueba
  (
    cod_piezapru serial primary key,
    cod_pieza integer not null,
    cod_prueba integer not null

  );
create table Pieza_Material
    (
      cod_piezamat serial primary key,
      cod_pieza integer not null,
      cod_material integer not null

    );
create table Estatus_Pieza
  (
    cod_piezastat serial primary key,
    cod_pieza integer not null,
    id_status integer not null
  );
create table Pieza_Pieza
    (
      usada_pieza integer not null,
      generada_pieza integer not null
    );
create table Modelo_Pieza
      (
        cod_modpi serial primary key,
        cod_pieza integer not null,
        id_modelo integer not null
      );
create table Ensamb_Avion
  (
    cod_ensamav serial primary key,
    cod_ensampi integer not null,
    cod_avion integer not null
  );
create table Avion_Pieza
    (
      cod_avionpi serial primary key,
      cod_pieza integer not null,
      cod_avion integer not null
    );
create table Material_Prueba
  (
    cod_pruebamat serial primary key,
    fechaini date not null,
    fechafin date,
    cod_material integer not null,
    cod_prueba integer not null
  );
