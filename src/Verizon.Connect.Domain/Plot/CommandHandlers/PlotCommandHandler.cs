namespace Verizon.Connect.Domain.Plot.CommandHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Verizon.Connect.Domain.Plot.Commands;
    using Verizon.Connect.Domain.Plot.Repositories;

    public class PlotCommandHandler : IRequestHandler<RegisterNewPlotCommand, bool>
    {
        private readonly IPlotRepository plotRepository;

        public PlotCommandHandler(IPlotRepository plotRepository)
        {
            this.plotRepository = plotRepository;
        }

        public async Task<bool> Handle(RegisterNewPlotCommand request, CancellationToken cancellationToken)
        {
            if(!request.IsValid())
            {
                return false;
            }

            //não tem jornada iniciada
            await this.plotRepository.Add(request.Entity);
            return true;
        }
    }
}
