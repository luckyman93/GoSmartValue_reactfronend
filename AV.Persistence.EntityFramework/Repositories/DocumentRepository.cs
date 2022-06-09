using AV.Common.Entities;
using AV.Common.Interfaces.Repositories;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(ValuationsContext context) : base(context)
        {

        }
    }
}