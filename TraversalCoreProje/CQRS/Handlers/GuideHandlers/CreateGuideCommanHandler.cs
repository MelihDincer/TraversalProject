using MediatR;
using System.Threading.Tasks;
using System.Threading;
using TraversalCoreProje.CQRS.Commands.GuideCommands;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;

namespace TraversalCoreProje.CQRS.Handlers.GuideHandlers
{
    public class CreateGuideCommanHandler : IRequestHandler<CreateGuideCommand> //Geriye değer döndürmeyeceğimiz için ikinci overloadı kullanmadık. Tek parametre kullandık bu yüzden.
    {
        private readonly Context _context;
        public CreateGuideCommanHandler(Context context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(CreateGuideCommand request, CancellationToken cancellationToken) //Void yani değer döndürmediğimiz için burada Unit adında bir değer yazıldı, zaten bu interface ile default olarak gelen bir metot.
        {
            _context.Guides.Add(new Guide
            {
                Name = request.Name,
                Description = request.Description,
                Status = true
            });
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
