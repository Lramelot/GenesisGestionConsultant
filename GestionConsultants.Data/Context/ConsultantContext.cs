using Microsoft.EntityFrameworkCore;

namespace GestionConsultants.Data.Context
{
    public class ConsultantContext : DbContext
    {
        public ConsultantContext(DbContextOptions<ConsultantContext> options) : base(options) { }
    }
}