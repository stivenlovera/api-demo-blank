using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_guardian.Contexts;
using api_guardian.Entities.DBComisiones;
using api_guardian.Entities.GrdSion.Queries;
using api_guardian.Utils;
using Microsoft.EntityFrameworkCore;

namespace api_guardian.Repository
{
    public class AdministracionContactoRepository : AdministracionContactoQuery
    {
        private readonly ILogger<AdministracionContactoRepository> logger;
        private readonly DbContextGrdSion dbContextGrdSion;

        public AdministracionContactoRepository(
            ILogger<AdministracionContactoRepository> logger,
            DbContextGrdSion dbContextGrdSion
        )
        {
            this.logger = logger;
            this.dbContextGrdSion = dbContextGrdSion;
        }
        public async Task<AdministracionContacto> GetByPatrocinador(long lpatrocinanteId)
        {
            this.logger.LogInformation("AdministracionContactoRepository/GetByPatrocinador({lpatrocinanteId})", lpatrocinanteId);
            var resultado = await this.dbContextGrdSion.AdministracionContacto.Where(x => x.Lcontacto_id == lpatrocinanteId).FirstOrDefaultAsync();
            this.logger.LogInformation("AdministracionNivelRepository/GetByPatrocinador SUCCESS => {resultado} registros", resultado != null ? 1 : 0);
            return resultado;
        }
        public async Task<List<ObtenerResultadorRedQuery>> ObtenerContactoDocCI(List<string> CIs, DateTime fechaInicio, DateTime fechaFin, int cbaja)
        {
            this.logger.LogInformation("AdministracionContactoRepository/ObtenerContactoDocCI({CIs},{fechaInicio},{fechaInicio})", Helper.Log(CIs), fechaInicio, fechaFin);
            var query = BuscarSegunCI(CIs, fechaInicio, fechaFin, cbaja);
            this.logger.LogWarning("Query({query})", query);
            var resultado = await this.dbContextGrdSion.ObtenerResultadorRedQuery.FromSqlRaw(query).ToListAsync();
            this.logger.LogInformation("AdministracionNivelRepository/ObtenerContactoDocCI SUCCESS => {resultado} registros", resultado.Count);
            return resultado;
        }
    }
    public class AdministracionContactoQuery
    {
        public string BuscarSegunCI(List<string> clientes, DateTime fechaInicio, DateTime fechaFin, int cbaja)
        {
            return @$"
                SELECT
                    a.dtfechaadd,
                    a.scedulaidentidad,
                    ac.dtfecha,
                    a.snombrecompleto,
                    a.lcontacto_id,
                    a.lpatrocinante_id,
                    a.lnivel_id,
                    ac.lcontrato_id,
                    ac.slote,
                    ac.smanzano,
                    a.cbaja,
                    a.stelefonomovil
                FROM
                    administracioncontacto as a
                    INNER JOIN administracioncontrato as ac ON ac.lcontacto_id = a.lcontacto_id
                WHERE
                    ac.dtfecha BETWEEN '{fechaInicio.ToString("yyyy-MM-dd")}' and '{fechaFin.ToString("yyyy-MM-dd")}'
                    and a.scedulaidentidad in (
                        '{String.Join("','", clientes)}'
                    )
                    and a.cbaja = '0'
                ORDER BY dtfechaadd DESC;
            ";
        }
    }
}