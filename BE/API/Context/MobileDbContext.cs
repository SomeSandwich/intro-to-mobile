using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Api.Context;

public class MobileDbContext : DbContext
{
    public MobileDbContext(DbContextOptions<MobileDbContext> options) : base(options)
    {
    }

    #region Entities

    #endregion
}