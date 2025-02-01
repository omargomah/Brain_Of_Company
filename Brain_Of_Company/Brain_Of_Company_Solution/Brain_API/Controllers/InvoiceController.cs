using Brain_API.DTO;
using Brain_Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Brain_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : APIBaseController
    {
        public InvoiceController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpGet("GetInvoiceWithProductName")]
        public async Task<IActionResult> GetProductWithCategoryName()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var invoices = await _unitOfWork.Invoices.GetAllAsync();

            var listInvoice = invoices.Where(x => x.IsDeleted == false).Select(x => new InvoiceAndProduct()
            {
                Id = x.Id,
                Price = x.Price,
                ProductId = x.ProductId,
                Quantities = x.Quantities,
                DateOfSale = x.DOS,
                ProductName = products.FirstOrDefault(c => c.Id == x.ProductId)?.Name ?? "Unknown",
            });

            return Ok(listInvoice);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var list = await _unitOfWork.Invoices.GetAllAsync();
            var Invoices = list.Where(x => x.IsDeleted == false).Select(x => new InvoiceDTO() {   Price = x.Price, ProductId = x.Id, Quantities = x.Quantities });
            return Ok(Invoices);
        }

        [HttpGet("GetByDateOfSale")]
        public async Task<IActionResult> GetByDOS(DateTime dos)
        {
            var invoice = await _unitOfWork.Invoices.FindAsync(x => x.DOS == dos);

            if (invoice == null || invoice.IsDeleted == true)
            {
                return NotFound("Invoice not found");
            }

            var product = await _unitOfWork.Products.FindAsync(x => x.Id == invoice.ProductId);

            if (product == null || product.IsDeleted == true)
            {
                return NotFound("Product not found");
            }

            InvoiceAndProduct invoiceDTO = new InvoiceAndProduct()
            {
                Id = invoice.Id,
                ProductName = product.Name, 
                Price = invoice.Price,
                ProductId = product.Id,
                Quantities = invoice.Quantities,
                DateOfSale = invoice.DOS
            };

            return Ok(invoiceDTO);
        }

        [HttpGet("GetInvoicesByDateRange")]
        public async Task<IActionResult> GetInvoicesByDateRange(DateTime startDate, DateTime endDate)
        {
            var invoices = await _unitOfWork.Invoices.FindAllAsync(x => x.DOS >= startDate && x.DOS <= endDate && x.IsDeleted == false);

            if (invoices == null || !invoices.Any())
            {
                return NotFound("No invoices found in the given date range.");
            }

            var products = await _unitOfWork.Products.GetAllAsync();

            var invoicesWithProductNames = invoices.Select(invoice => new InvoiceAndProduct
            {
                Id = invoice.Id,
                ProductId = invoice.ProductId,
                ProductName = products.FirstOrDefault(p => p.Id == invoice.ProductId)?.Name ?? "Unknown",
                Price = invoice.Price,
                Quantities = invoice.Quantities,
                DateOfSale = invoice.DOS
            }).ToList();

            return Ok(invoicesWithProductNames);
        }

        [HttpGet("CalculateTotalSales")]
        public async Task<IActionResult> GetTotalSales(DateTime startDate, DateTime endDate)
        {
            var invoices = await _unitOfWork.Invoices.FindAllAsync(x => x.DOS >= startDate && x.DOS <= endDate && x.IsDeleted == false);

            if (invoices == null || !invoices.Any())
            {
                return NotFound("No invoices found in the given date range.");
            }
            var totalSale = invoices.Select(x => x.Price * x.Quantities).Sum(); 
            return Ok($"totalSale was {totalSale} at this interval");
        }




        [HttpPut("EditInvoice")]
        public async Task<IActionResult> EditProduct(InvoiceAndProduct invoiceDTO)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdAsync(invoiceDTO.Id);
            if (invoice != null && invoice.IsDeleted == false)
            {

                invoice.DOS = invoiceDTO.DateOfSale;
                var tempo = await _unitOfWork.Invoices.FindAsync(x =>x.DOS == invoiceDTO.DateOfSale);
                if (tempo == null || tempo.Id == invoice.Id)
                { 
                    invoice.Price = invoiceDTO.Price;
                    invoice.Quantities = invoiceDTO.Quantities;
                   
                    _unitOfWork.Save();
                    return Ok("Updated");
                }
            }
            return BadRequest("Invalid Id");
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddInvoice(InvoiceDTO invoiceDTO)
        {
            
                var invoice = new Invoice()
                {
                    DOS = DateTime.Now,
                    Price = invoiceDTO.Price,
                    ProductId = invoiceDTO.ProductId,
                    Quantities = invoiceDTO.Quantities,
                    IsDeleted = false,

                };
            
                await _unitOfWork.Invoices.AddAsync(invoice);
               int success = _unitOfWork.Save();
                if (success != 0)
                {
                     return Ok("Added!");
                }
            return BadRequest("not true");
        }

        [HttpDelete("DeleteProductById")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdAsync(id);
            if (invoice != null)
            {
                invoice.IsDeleted = true;
                _unitOfWork.Save();
                return Ok("Deleted");
            }

            return NotFound("Not Found");
        }
    }
}
