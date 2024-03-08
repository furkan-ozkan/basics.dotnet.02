using api.Dtos.Stock;
using api.Models;

namespace api.Mapper
{
    public static class StockMapper
    {
        public static StockRequestDto ToStockRequestDto(this Stock stockModel)
        {
            return new StockRequestDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentRequestDto()).ToList()
            };
        }

        public static Stock ToStockFromCreateDTO(this StockCreateRequestDto stockCreateRequestDto)
        {
            return new Stock
            {
                Symbol = stockCreateRequestDto.Symbol,
                CompanyName = stockCreateRequestDto.CompanyName,
                Purchase = stockCreateRequestDto.Purchase,
                LastDiv = stockCreateRequestDto.LastDiv,
                Industry = stockCreateRequestDto.Industry,
                MarketCap = stockCreateRequestDto.MarketCap
            };
        }

        public static Stock UpdateToStockFromStockDTO(this StockUpdateRequestDto updateStockDto, Stock stock)
        {
            stock.Symbol = updateStockDto.Symbol;
            stock.CompanyName = updateStockDto.CompanyName;
            stock.Purchase = updateStockDto.Purchase;
            stock.LastDiv = updateStockDto.LastDiv;
            stock.Industry = updateStockDto.Industry;
            stock.MarketCap = updateStockDto.MarketCap;

            return stock;
        }
    }
}