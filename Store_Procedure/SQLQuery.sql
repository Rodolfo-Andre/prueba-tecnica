CREATE OR ALTER PROCEDURE SP_OBTENER_TRABAJADORES
	 @Nombre VARCHAR(150)
AS
BEGIN
    SELECT
        T.Id,
        T.TipoDocumento,
        T.NumeroDocumento,
        T.Nombres,
        T.Sexo,
        T.IdDepartamento,
        T.IdProvincia,
        T.IdDistrito,
        DE.NombreDepartamento,
        P.NombreProvincia,
        DI.NombreDistrito
    FROM dbo.Trabajadores AS T
    INNER JOIN dbo.Departamento AS DE ON T.IdDepartamento = DE.Id
    INNER JOIN dbo.Provincia AS P ON T.IdProvincia = P.Id
    INNER JOIN dbo.Distrito AS DI ON T.IdDistrito = DI.Id
	WHERE T.Nombres LIKE '%'+ @Nombre +'%'
END;

EXEC SP_OBTENER_TRABAJADORES '';