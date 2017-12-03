$(function (){
    	
	$('#selectPais').on('change',buscarEstado);

	$('#selectEstado').on('change',buscarCiudad);

});

function  buscarEstado() {

var id_pais= $(this).val();
$.get('/buscarEstado/'+id_pais+'/estados', function (data)
	{	
		var html_select='<option value="">Seleccione ciudad</option>';
		$('#selectCiudad').html(html_select);

		var html_select='<option value="">Seleccione estado</option>';
		$('#selectEstado').html(html_select);
		for (var i = 0; i < data.length; ++i) 
		{
			html_select +='<option value="'+data[i].idLugar+'">'+data[i].nombre+'</option>';
			$('#selectEstado').html(html_select);
		}
	});
}

function  buscarCiudad() {

var id_estado= $(this).val();
$.get('/buscarCiudad/'+id_estado+'/ciudades', function (data)
	{	
		var html_select='<option value="">Seleccione ciudad</option>';
		$('#selectCiudad').html(html_select);
		for (var i = 0; i < data.length; ++i) 
		{
			html_select +='<option value="'+data[i].idLugar+'">'+data[i].nombre+'</option>';
			$('#selectCiudad').html(html_select);
		}
	});
}