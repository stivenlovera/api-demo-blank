using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Entities.Proyeccion;
using api_guardian.Repository;
using api_guardian.Repository.BDComisiones;

namespace api_guardian.Module
{
    public class ProyeccionModule
    {
        private readonly ILogger<ProyeccionModule> logger;
        private readonly ProyeccionRepository proyeccionRepository;
        private readonly CnxBdComsionesRepository cnxBdComsionesRepository;
        private readonly InCoutaRespository inCoutaRespository;

        public ProyeccionModule(
            ILogger<ProyeccionModule> logger,
            ProyeccionRepository proyeccionRepository,
            CnxBdComsionesRepository cnxBdComsionesRepository,
            InCoutaRespository inCoutaRespository
        )
        {
            this.logger = logger;
            this.proyeccionRepository = proyeccionRepository;
            this.cnxBdComsionesRepository = cnxBdComsionesRepository;
            this.inCoutaRespository = inCoutaRespository;
        }
        public async Task<bool> CopiarCosolidadoByCiclo(int cicloId)
        {
            this.logger.LogInformation("ProyeccionModule/CopiarCosolidadoByCiclo({cicloId})", cicloId);

            var replicar = await this.proyeccionRepository.GetAllConsolidadoByCicloId(cicloId);
            this.logger.LogInformation("Consolidados {consolidados} registros encontrados", replicar.Count);
            await this.proyeccionRepository.StoreVenta(replicar);
            var consolidados = await this.proyeccionRepository.GetAllByCicloId(cicloId);

            var insertarCoutas = new List<Cuota>();
            foreach (var venta in consolidados)
            {
                var conexion = await this.cnxBdComsionesRepository.GetConexionDataBase(venta.VentaSionId);
                if (conexion != null)
                {
                    var getCoutas = await this.inCoutaRespository.GetInCoutaToProyeccion(conexion.Nombrebd, venta.VentaSionId);
                    this.logger.LogInformation("son {getCoutas} coutas de la venta {VentaSionId}", getCoutas.Count, venta.VentaSionId);
                    this.logger.LogInformation("añadiendo para insertar total {Count}", insertarCoutas.Count);
                    var insertarCouta = getCoutas.Select(x => new Cuota
                    {
                        VentaId = venta.VentaId,
                        FechaPago = x.FechaPago,
                        FechaVencimiento = x.FechaVencimiento,
                        MontoCouta = x.MontoCouta,
                        MontoDeuda = x.MontoDeuda,
                        MontoPago = x.MontoPago,
                        NroCuota = x.NroCuota
                    });
                    insertarCoutas.AddRange(insertarCouta);
                }
            }
            this.logger.LogInformation("coutas a insertar {Count}", insertarCoutas.Count);
            var insertCoutas = await this.proyeccionRepository.StoreCoutas(insertarCoutas);
            return true;
        }
        public async Task<bool> Datatable(int cicloId)
        {
            this.logger.LogInformation("ProyeccionModule/CopiarCosolidadoByCiclo({cicloId})", cicloId);

            var replicar = await this.proyeccionRepository.GetAllConsolidadoByCicloId(cicloId);
            this.logger.LogInformation("Consolidados {consolidados} registros encontrados", replicar.Count);
            await this.proyeccionRepository.StoreVenta(replicar);
            var consolidados = await this.proyeccionRepository.GetAllByCicloId(cicloId);

            var insertarCoutas = new List<Cuota>();
            foreach (var venta in consolidados)
            {
                var conexion = await this.cnxBdComsionesRepository.GetConexionDataBase(venta.VentaSionId);
                if (conexion != null)
                {
                    var getCoutas = await this.inCoutaRespository.GetInCoutaToProyeccion(conexion.Nombrebd, venta.VentaSionId);
                    this.logger.LogInformation("son {getCoutas} coutas de la venta {VentaSionId}", getCoutas.Count, venta.VentaSionId);
                    this.logger.LogInformation("añadiendo para insertar total {Count}", insertarCoutas.Count);
                    var insertarCouta = getCoutas.Select(x => new Cuota
                    {
                        VentaId = venta.VentaId,
                        FechaPago = x.FechaPago,
                        FechaVencimiento = x.FechaVencimiento,
                        MontoCouta = x.MontoCouta,
                        MontoDeuda = x.MontoDeuda,
                        MontoPago = x.MontoPago,
                        NroCuota = x.NroCuota
                    });
                    insertarCoutas.AddRange(insertarCouta);
                }
            }
            this.logger.LogInformation("coutas a insertar {Count}", insertarCoutas.Count);
            var insertCoutas = await this.proyeccionRepository.StoreCoutas(insertarCoutas);
            return true;
        }
    }
}