using MediatR;

namespace FileOrganizer.Domain
{
    public sealed class GetStatisticsQuery : IRequest<Statistics>
    {
        // empty
    }
}
