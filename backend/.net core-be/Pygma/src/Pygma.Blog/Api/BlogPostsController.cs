using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pygma.Blog.Api.Base;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;

namespace Pygma.Blog.Api
{
    [Route("api/blog-posts")]
    public class BlogPostsController: BlogControllerBase
    {
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly IMapper _mapper;

        public BlogPostsController(IBlogPostsRepository blogPostsRepository,
            IMapper mapper)
        {
            _blogPostsRepository = blogPostsRepository;
            _mapper = mapper;
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> CreateBlogPostAsync(int orderId, [FromBody] CreateBlogPostVm createBlogPostVm)
        {
            var offer = new BlogPost();
            await _blogPostsRepository.CreateAsync(_mapper.Map(createBlogPostVm, offer));

            return Ok(offer.Id);
        }

        [HttpGet("{blogPostId:int:min(1)}", Name = nameof(GetBlogPostAsync))]
        public async Task<ActionResult<BlogPostVm>> GetBlogPostAsync(int blogPostId)
        {
             var offer = await _BlogPostsRepository.FindByIdAsync(offerId);
             
             return _mapper.Map<BlogPostVm>(offer);
        }

        [HttpPut("{offerId:int:min(1)}")]
        public async Task<ActionResult> UpdateBlogPostAsync(int orderId, int offerId, UpdateBlogPostVm updateBlogPostVm)
        {
            var offer = await _BlogPostsRepository.FindByIdAsync(offerId);

            if (offer is null)
            {
                return NotFound();
            }

            await _BlogPostsRepository.UpdateAsync(_mapper.Map(updateBlogPostVm, offer));

            return NoContent();
        }

        [HttpGet("{offerId:int:min(1)}/export")]
        public async Task<FileStreamResult> ExportCabinetOrderAsync(int orderId, int offerId)
        {
            var cabinetOrder = await _cabinetOrdersRepository.FindByIdExportFullAsync(orderId);

            // update order with customer margin
            
            var webRootFolder = _hostingEnvironment.WebRootPath + "/CabinetOrders";
            var fileName = $"CabinetOrder-{orderId}-Offer-{offerId}.xlsx";

            var memoryStream = await ExcelHelpers
                .ExportToMemory(webRootFolder, fileName, new CabinetsOrderExcelExporter(cabinetOrder));

            return File(memoryStream, ContentType.Excel, fileName);
        }
    }
}