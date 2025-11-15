using Joseco.DDD.Core.Results;
using MediatR;
using PlanesRecetas.domain.Care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanesRecetas.application.Care
{
    public class CreateCategoryCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public TipoAlimento Tipo { get; set; }

    }
}
