CREATE or ALTER VIEW VistaEnviosDetallados AS
SELECT
    e.idEnvios,
    e.FechaSolicitud,
    e.FechaEntregaEsperada,
	c.nombreCliente AS Cliente, 
    r.Origen AS Origen,         
    r.Destino AS Destino,        
    e.Estado,
    e.Mercancia,
    e.PesoTotal AS Peso,
    e.VolumenTotal AS Volumen,
	e.CostoEnvio AS Costo
   
FROM
    Envios AS e
JOIN
    Clientes AS c ON e.idCliente = c.idClientes
JOIN
    Rutas AS r ON e.idRuta = r.idRutas;

SELECT * FROM VistaEnviosDetallados