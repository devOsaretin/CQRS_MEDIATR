using MediatR;

namespace CQRS_MEDIATR.Commands
{
  public record AddProductCommand(Product Product) : IRequest<Product>;

}