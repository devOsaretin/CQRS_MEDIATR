using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS_MEDIATR.Commands;
using CQRS_MEDIATR.Notifications;
using CQRS_MEDIATR.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_MEDIATR.Controllers
{
  [Route("api/products")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    // the IMediator interface consists of ISender and IPublisher
    //we could use ISender if we want to be strict about sending a request or IPublisher for publishing a request

    //private readonly IMediator _mediator;
    private readonly ISender _sender;
    private readonly IPublisher _publisher;

    public ProductsController(ISender sender, IPublisher publisher)
    {
      _sender = sender;
      _publisher = publisher;
    }

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
      var products = await _sender.Send(new GetProductsQuery());
      return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] Product product)
    {
      //return Ok(await _sender.Send(new AddProductCommand(product)));
      var productToReturn = await _sender.Send(new AddProductCommand(product));
      await _publisher.Publish(new ProductAddedNotification(productToReturn));
      return CreatedAtRoute("GetProductById", new { id = productToReturn.Id }, productToReturn);
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    public async Task<ActionResult<Product>> GetProductById(int id)
    {
      var product = await _sender.Send(new GetProductByIdQuery(id));
      return Ok(product);
    }
  }
}