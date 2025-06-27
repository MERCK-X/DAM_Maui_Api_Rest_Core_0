window.onload = function ()
{
	listar();
}

async function listar()
{
	var datatipousuario = await fetchGet("TipoUsuario/listarTipoUsuario", "json", null, true);
	var dataPersonaSinUsuario = await fetchGet("Persona/listarPersonaSinUsuario", "json", null, true);

	pintar(
		{
			url: "Usuario/listarUsuarios",
			cabeceras: ["Foto", "Usuario", "Persona", "Tipo de usuario"],
			//Atributos o campos de la clase PersonaCLS
			propiedades: ["fotopersona", "nombreusuario", "nombrepersona", "nombretipousuario"],
			propiedadId: "iidusuario",
			columnaimg: ["fotopersona"],

			titlePopup: "Usuario boludo ",
			editar: true,
			eliminar: true,
			popup: true,

			callbackrecuperar: function ()
			{
				document.getElementsByClassName("ocultar")[0].style.display = "none" //Ocultar combo
			},
			callbacknuevo: function ()
			{
				document.getElementsByClassName("ocultar")[0].style.display = "block" //Mostrar
			},

			

		},{
			url: "Usuario/buscarUsuarios",
			formulario:
				[
					[
						{
							class: "col-md-6",
							label: "Nombre de usuario boludo",
							type: "text",
							name: "nombreusuario"
						},
                        {
                            class: "col-md-6",
                            label: "Tipo usuario boludo",
                            type: "combobox",
							name: "iidtipousuario",
							data: datatipousuario,
							propiedadmostrar: "nombretipousuario",
							valuemostrar: "iidtipousuario",
							id: "cboTipoUsuarioBusqueda"
                        }
					]
				]
		},{
			urlguardar: "Usuario/guardarUsuario",
			urlrecuperar: "Usuario/recuperarUsuario",
			parametrorecuperar: "id",

			callbackGuardar: async function ()
			{
				var dataPersonaSinUsuario = await fetchGet("Persona/listarPersonaSinUsuario", "json", null, true);
				llenarCombo(dataPersonaSinUsuario, "cboPersonaFormulario", "iidpersona", "nombrecompleto", "--Seleccione--", "0");
			},
			type: "popup",
			formulario:
				[
					[
						{
							class: "col-md-6",
							label: "Id del usuario boludo",
							readonly: true,
							name: "iidusuario"
						},{
							class: "col-md-6 ocultar",
							label: "Persona boluda",
							type: "combobox",
							name: "iidpersona",
							data: dataPersonaSinUsuario,
							propiedadmostrar: "nombrecompleto",
							valuemostrar: "iidpersona",
							id: "cboPersonaFormulario"
						},{
							class: "col-md-6",
							label: "Nombre del usuario boludo",
							name: "nombreusuario"
						},{
							class: "col-md-6",
							label: "Tipo de usuario boludo",
							type: "combobox",
							name: "iidtipousuario",
							data: datatipousuario,
							propiedadmostrar: "nombretipousuario",
							valuemostrar: "iidtipousuario",
							id: "cboTipoUsuarioFormulario"
						}
					]
				]
		  }
	)
}