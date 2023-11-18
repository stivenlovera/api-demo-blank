using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_guardian.SqlQuerys.DBProyeccion
{
    public class ProyeccionQuery
    {
        public static string GetAllConsolidadosByCicloId()
        {
            return @"
            SELECT
                ac.lcontrato_id,
                substring_index (ac.snroventa, '-' , 1) as venta_sion_id,
                ac.dprecio as precio,
                avp.lciclo_id as ciclo_id,
                ac.dprecioinicial as precio_inicial,
                ac.dcuota_inicial as cuota_inicial,
                ac.snroventa as nro_venta,
                ac.lcomplejo_id as  complejo_id,
                ac.lcontacto_id as cliente_id,
                ac.lasesor_id as  vendedor_id,
                ac.lestado as estado_id,
                ac.ltipocontrato_id as tipo_venta_id,
                ac.dtfecha as fecha_venta,
                @cicloId as ciclo_id
            FROM
                grdsion.administracionventapersonal as avp
                INNER JOIN grdsion.administracioncontrato as ac on ac.lcontrato_id = avp.lcontrato_id
            where avp.lciclo_id = @cicloId
            and avp.dporcentajecomision=30
            and ac.snroventa NOT LIKE '%UPGRADE%'
            order by ac.dtfecha asc;
            ";
        }
        public static string GetAllVentaByCicloId()
        {
            return @"
                SELECT
                    venta_id,
                    lcontrato_id,
                    venta_sion_id,
                    precio,
                    precio_inicial,
                    cuota_inicial,
                    nro_venta,
                    complejo_id,
                    cliente_id,
                    vendedor_id,
                    estado_id,
                    tipo_venta_id,
                    fecha_venta,
                    ciclo_id
                FROM venta
                where ciclo_id = @cicloId
                order by fecha_venta asc;
            ";
        }
        public static string StoreVenta()
        {
            return @"
                insert into 
                    venta (
                        lcontrato_id, 
                        venta_sion_id, 
                        precio, 
                        precio_inicial, 
                        cuota_inicial, 
                        nro_venta, 
                        complejo_id, 
                        cliente_id, 
                        vendedor_id, 
                        estado_id, 
                        tipo_venta_id, 
                        fecha_venta,
                        ciclo_id
                    )
                    values
                    (
                        @LcontratoId, 
                        @VentaSionId, 
                        @Precio, 
                        @PrecioInicial, 
                        @CuotaInicial, 
                        @NroVenta, 
                        @ComplejoId, 
                        @ClienteId, 
                        @VendedorId, 
                        @EstadoId, 
                        @TipoVentaId, 
                        @FechaVenta,
                        @CicloId
                    );
                SELECT LAST_INSERT_ID() as id;
            ";
        }
        public static string StoreCouta()
        {
            return @" 
                insert into 
                `cuota` (
                    `venta_id`, 
                    `nro_cuota`, 
                    `fecha_vencimiento`, 
                    `monto_couta`, 
                    `monto_deuda`, 
                    `fecha_pago`, 
                    `monto_pago`
                )
                values
                (
                    @VentaId, 
                    @NroCuota, 
                    @FechaVencimiento, 
                    @MontoCouta, 
                    @MontoDeuda, 
                    @FechaPago, 
                    @MontoPago
                );
            ";
        }
    }
}