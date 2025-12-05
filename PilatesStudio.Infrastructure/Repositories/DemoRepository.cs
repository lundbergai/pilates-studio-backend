using Microsoft.EntityFrameworkCore;
using PilatesStudio.Domain.Entities;
using PilatesStudio.Infrastructure.Persistence;

namespace PilatesStudio.Infrastructure.Repositories;

public class DemoRepository(PilatesDbContext context)
{
    private readonly PilatesDbContext _context = context;
    
    public async Task<Demo?> GetDemoAsync(string name)
    {
        return await _context.Demos.FirstOrDefaultAsync(g => g.Name == name);
    }

    public async Task<Demo> AddDemoAsync(string name)
    {
        var demo = new Demo
        {
            Name = name
        };
        
        _context.Add(demo);
        await _context.SaveChangesAsync();
        
        return demo;
    }
}