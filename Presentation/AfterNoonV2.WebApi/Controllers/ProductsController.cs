using AfterNoonV2.Application.Abstractions.Storage;
using AfterNoonV2.Application.Constants;
using AfterNoonV2.Application.CustomAttributes;
using AfterNoonV2.Application.Enums;
using AfterNoonV2.Application.Features.Commands.Product.CreateProduct;
using AfterNoonV2.Application.Features.Commands.Product.DeleteProduct;
using AfterNoonV2.Application.Features.Commands.Product.UpdateProduct;
using AfterNoonV2.Application.Features.Commands.ProductImage.UploadProductImage;
using AfterNoonV2.Application.Features.Queries.Product.GetAllProduct;
using AfterNoonV2.Application.Features.Queries.Product.GetProductById;
using AfterNoonV2.Application.Features.Queries.ProductImage.GetImage;
using AfterNoonV2.Application.Repositeries;
using AfterNoonV2.Application.RequestParameters;
using AfterNoonV2.Application.ViewModel;
using AfterNoonV2.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AfterNoonV2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {
            GetAllProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{ProductId}")]
        public async Task<IActionResult> Get([FromRoute] GetProductQueryRequest request)
        {
            GetProductQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Products, Definition = "Add Product", Action = ActionsEnum.Writing)]
        public async Task<IActionResult> Create(CreateProductCommandRequest request)
        {
            CreateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Products, Definition = "Update Product", Action = ActionsEnum.Updating)]
        public async Task<IActionResult> Update(UpdateProductCommandRequest request)
        {
            UpdateProductCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{ProductId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Products, Definition = "Delete Product", Action = ActionsEnum.Deleting)]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest id)
        {
            DeleteProductCommandResponse response = await _mediator.Send(id);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Products, Definition = "Upload Product Image", Action = ActionsEnum.Writing)]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest request)
        {
            request.Files = Request.Form.Files;
            await _mediator.Send(request);
            return Ok();
        }


        [HttpGet("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Products, Definition = "Get Product Image", Action = ActionsEnum.Reading)]
        public async Task<IActionResult> GetImages([FromRoute] GetImageQueryRequest request)
        {
            IReadOnlyList<GetImageQueryResponse> response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
