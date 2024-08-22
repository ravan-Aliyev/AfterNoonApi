using AfterNoonV2.Application.Constants;
using AfterNoonV2.Application.CustomAttributes;
using AfterNoonV2.Application.Enums;
using AfterNoonV2.Application.Features.Commands.Basket.CreateBasketItem;
using AfterNoonV2.Application.Features.Commands.Basket.DeleteBasketItem;
using AfterNoonV2.Application.Features.Commands.Basket.UpdateBasketItem;
using AfterNoonV2.Application.Features.Queries.Basket.GetAllBasketItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AfterNoonV2.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Admin")]
public class BasketController : ControllerBase
{
    readonly IMediator _mediator;

    public BasketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Baskets, Definition = "Get Basket Items", Action = ActionsEnum.Reading)]
    public async Task<IActionResult> GetAllBasketItems([FromQuery] GetAllBasketItemsQueryRequest request)
    {
        List<GetAllBasketItemsQueryResponse> response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Baskets, Definition = "Add Item To Basket", Action = ActionsEnum.Writing)]
    public async Task<IActionResult> AddItemToBasket(CreateBasketItemCommandRequest request)
    {
        CreateBasketItemCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Baskets, Definition = "Update Basket Item", Action = ActionsEnum.Updating)]
    public async Task<IActionResult> UpdateBasketItem(UpdateBasketItemCommandRequest request)
    {
        UpdateBasketItemCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpDelete("{BasketItemId}")]
    [Authorize(AuthenticationSchemes = "Admin")]
    [AuthorizeDefinition(Menu = AuthorizeDefinitionConst.Baskets, Definition = "Delete Basket Item", Action = ActionsEnum.Deleting)]
    public async Task<IActionResult> DeleteBasketItem([FromRoute] DeleteBasketItemCommandRequest request)
    {
        DeleteBasketItemCommandResponse response = await _mediator.Send(request);
        return Ok(response);
    }
}
