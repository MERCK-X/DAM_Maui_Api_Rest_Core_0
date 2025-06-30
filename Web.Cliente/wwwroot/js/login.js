function Ingresar()
{
	var nombreusu = get("txtusuario")
	var contra = get("txtcontra")

	fetchGet("Login/login/?nombreusuario=" + nombreusu + "&contra=" + contra, "text", function (respuesta)
	{
		if (respuesta == 0)
		{
			Error("Usuario o contraseña incorrecta")
		}
		else
		{
			document.location.href = "/Persona/Index";
		}
	})

}