alter table Orden_compra add constraint oc_prov
foreign key (id_proveedor) references Proveedor (id_proveedor);
alter table Orden_compra add constraint oc_mat
foreign key (cod_material) references Material (cod_material);

alter table Equipo add constraint eq_zona
foreign key (id_zona) references Zona (id_zona);

alter table Telefono add constraint tel_per
foreign key (cod_personal) references Personal(id_personal);
alter table Telefono add constraint tel_prov
foreign key (id_proveedor) references Proveedor(id_proveedor);
alter table Telefono add constraint tel_bene
foreign key (id_bene) references Beneficiario(id_bene);
alter table Telefono add constraint tel_cli
foreign key (id_cliente) references Cliente(id_cliente);

alter table Rol_Priv add constraint rol
foreign key (cod_rol) references Rol(id_rol);
alter table Rol_Priv add constraint priv
foreign key (id_privilegio) references Privilegio(id_privilegio);

alter table Proveedor_Material add constraint pm_prov
foreign key (id_proveedor) references Proveedor(id_proveedor);
alter table Proveedor_Material add constraint pm_mat
foreign key (cod_material) references Material(cod_material);

alter table Pago add constraint pago_oc
foreign key (id_orden) references Orden_compra(id_orden);
alter table Pago add constraint pago_tp
foreign key (id_tipopago) references Tipo_Pago(id_tipopago);

alter table Ensam_Pieza add constraint ep_pie
foreign key (cod_pieza) references Pieza(cod_pieza);
alter table Ensam_Pieza add constraint ep_eq
foreign key (cod_equipo) references Equipo(cod_equipo);

alter table Pieza_Material add constraint pim_pie
foreign key (cod_pieza) references Pieza(cod_pieza);
alter table Pieza_Material add constraint pim_mat
foreign key (cod_material) references Material(cod_material);

alter table Estatus_Pieza add constraint ep_pi
foreign key (cod_pieza) references Pieza(cod_pieza);
alter table Estatus_Pieza add constraint ep_st
foreign key (id_status) references Estatus(id_status);

alter table Pieza_Pieza add constraint pi_1
foreign key (usada_pieza) references Pieza(cod_pieza);
alter table Pieza_Pieza add constraint pi_2
foreign key (generada_pieza) references Pieza(cod_pieza);

alter table Modelo_Pieza add constraint mopi_pi
foreign key (cod_pieza) references Pieza(cod_pieza);
alter table Modelo_Pieza add constraint mopi_mo
foreign key (id_modelo) references Modelo(id_modelo);

alter table Ensamb_Avion add constraint ea_ens
foreign key (cod_ensampi) references Ensam_Pieza(cod_ensampi);
alter table Ensamb_Avion add constraint ea_avi
foreign key (cod_avion) references Avion(cod_avion);

alter table Avion_Pieza add constraint avpi_pi
foreign key (cod_pieza) references Pieza(cod_pieza);
alter table Avion_Pieza add constraint avpi_av
foreign key (cod_avion) references Avion(cod_avion);

alter table Material_Prueba add constraint mapu_mat
foreign key (cod_material) references Material(cod_material);
alter table Material_Prueba add constraint mapu_pru
foreign key (cod_prueba) references Prueba(cod_prueba);
