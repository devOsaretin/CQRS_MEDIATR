using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace CQRS_MEDIATR.Queries
{
  public record GetProductByIdQuery(int Id) : IRequest<Product>;

}