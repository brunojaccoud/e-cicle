using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using EletronicPartsCatalog.Infrastructure;
using EletronicPartsCatalog.Infrastructure.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EletronicPartsCatalog.Features.Comments
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Command(string slug, int id)
            {
                Slug = slug;
                Id = id;
            }

            public string Slug { get; }
            public int Id { get; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Slug).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Command>
        {
            private readonly EletronicPartsCatalogContext _context;

            public QueryHandler(EletronicPartsCatalogContext context)
            {
                _context = context;
            }

            public async Task Handle(Command message, CancellationToken cancellationToken)
            {
                var Project = await _context.Projects
                    .Include(x => x.Comments)
                    .FirstOrDefaultAsync(x => x.Slug == message.Slug, cancellationToken);

                if (Project == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Project = "Project not found." });
                }

                var comment = Project.Comments.FirstOrDefault(x => x.CommentId == message.Id);
                if (comment == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { Comment = "Comment not found." });
                }
                
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}