using Api.Context;
using Api.Modules.Category.Mapping;
using Api.Modules.Category.Type;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Api.Modules.Category.Service;

public interface ICategoryService
{
    Task<IEnumerable<CategoryRes>> Get();
    Task<CategoryRes> Get(int id);

    Task<bool> Create(CreateCategoryReq request);

    Task<bool> Update(int id, UpdateCategoryReq request);

    Task<bool> Delete(int id);
}

public class CategoryService : ICategoryService
{
    private readonly MobileDbContext _context;

    private readonly IMapper _mapper;

    public CategoryService(MobileDbContext context)
    {
        _context = context;

        var mapperConfig = new MapperConfiguration(opt => { opt.AddProfile<CategoryProfile>(); });
        _mapper = mapperConfig.CreateMapper();
    }

    public async Task<IEnumerable<CategoryRes>> Get()
    {
        var categories = _context.Categories
            .AsEnumerable();

        return _mapper.Map<IEnumerable<Context.Entities.Category>, IEnumerable<CategoryRes>>(categories);
    }

    public async Task<CategoryRes> Get(int id)
    {
        var category = await _context.Categories
            .OrderBy(e => e.Id)
            .FirstOrDefaultAsync(e => e.Id == id);

        return _mapper.Map<Context.Entities.Category, CategoryRes>(category);
    }

    public async Task<bool> Create(CreateCategoryReq request)
    {
        try
        {
            var category = _mapper.Map<CreateCategoryReq, Context.Entities.Category>(request);

            await _context.AddAsync(category);

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message;

            return false;
        }
    }

    public async Task<bool> Update(int id, UpdateCategoryReq request)
    {
        try
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(e => e.Id == id);

            if (category is null)
                return false;

            if (!string.IsNullOrEmpty(request.Description))
                category.Description = request.Description;

            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            var message = ex.InnerException?.Message;

            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        return false;
    }
}
