window.onload = function ()
{
	listar();
}

function listar()
{
	pintar(
	{
		url: "Persona/listarPersonas",
		cabeceras: ["Foto","Nombre completo", "Fecha nacimiento", "Correo"],
		//Atributos o campos de la clase PersonaCLS
		propiedades: ["fotocadena","nombrecompleto", "fechanacimientocadena", "correo"],
        propiedadId: "iidpersona",
        columnaimg: "fotocadena",

		popup: true,
		editar: true,
		eliminar: true,
		titlePopup: "Persona",

        //url para eliminar
		urleliminar: "Persona/eliminarPersona",
		parametroeliminar: "id",
	},{
		url: "Persona/filtrarPersonas",
		formulario:
		[
			[
				{
					class: "col-md-6",
					label: "Ingrese nombre completo",
					type: "text",
					name: "nombrecompleto"
				}
			]
		]
	},{
		type: "popup",
		urlrecuperar: "Persona/recuperarPersona",
		parametrorecuperar: "id",

		urlguardar: "Persona/guardarPersona",
        

        formulario:
        [
            [
                {
                    class: "col-md-6 d-none",
                    label: "Id persona",
                    type: "text",
                    name: "iidpersona",
				}, {
					class: "col-md-6",
					label: "Nombre",
					type: "text",
					name: "nombre",
					classControl:"ob max-10",
				}, {
					class: "col-md-6",
					label: "Apellido paterno",
					type: "text",
					name: "appaterno",
					classControl: "ob",
				}, {
					class: "col-md-6",
					label: "Apellido materno",
					type: "text",
					name: "apmaterno",
					classControl: "ob",
				}, {
					class: "col-md-6",
					label: "Fecha de nacimiento del boludo",
					type: "date",
					name: "fechanacimientocadena",
					classControl: "ob",
				}, {
					class: "col-md-6",
					label: "Correo del boludo",
					type: "text",
					name: "correo",
					classControl: "ob",
				}, {
					class: "col-md-6",
					label: "Setso",
					type: "radio",
					labels: ["Masculino", "Femenino"],
					values: ["2", "1"],
					ids: ["rbMasculino", "rbFemenino"],
                    checked: "rbMasculino", 
					name: "iidsexo",
				}, {
					class: "col-md-6",
					label: "foto",
					type: "file",
					label: "Suba una foto...",
					name: "fotoenviar",
					preview: "true",
					imgwidth: "100px",
					imgheigth: "100px",
					namefoto: "fotocadena",
					classControl: "ob",
				}, 
			]
        ]
	}
	)
}