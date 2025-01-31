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

            var listInvoice = invoices.Select(x => new InvoiceAndProduct()
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
            var Invoices = list.Select(x => new InvoiceDTO() {   Price = x.Price, ProductId = x.Id, Quantities = x.Quantities });
            return Ok(Invoices);
        }

        [HttpGet("GetByDateOfSale")]
        public async Task<IActionResult> GetByDOS(DateTime dos)
        {
            var invoice = await _unitOfWork.Invoices.FindAsync(x => x.DOS == dos);

            if (invoice == null)
            {
                return NotFound("Invoice not found");
            }

            var product = await _unitOfWork.Products.FindAsync(x => x.Id == invoice.ProductId);

            if (product == null)
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


        [HttpPut("EditInvoice")]
        public async Task<IActionResult> EditProduct(InvoiceAndProduct invoiceDTO)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdAsync(invoiceDTO.Id);
            if (invoice != null)
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
