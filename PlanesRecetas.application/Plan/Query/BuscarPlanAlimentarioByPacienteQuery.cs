using Joseco.DDD.Core.Results;
using MediatR;


namespace PlanesRecetas.application.Plan.Query
{
    public sealed record BuscarPlanAlimentarioByPacienteQuery(Guid Id , DateTime FechaInicio, bool IgnorarFiltroFecha =false, bool ReadOnly = false): IRequest<Result<List<PlanAlimentacionDto>>>
    {
    }
}
