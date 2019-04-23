namespace Verizon.Connect.Domain.Plot.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Verizon.Connect.Domain.Plot.Dto;
    using Verizon.Connect.Domain.Plot.Models;

    public interface IPlotRepository
    {
        /// <summary>
        /// Add new Plot
        /// </summary>
        /// <param name="plotEntity">Plot Entity</param>
        /// <returns>Success</returns>
        Task<bool> Add(PlotEntity plotEntity);

        /// <summary>
        /// Query Plots
        /// </summary>
        /// <param name="vId">Vehicle Id</param>
        /// <param name="initialTimeStamp">Initial TimeStamp</param>
        /// <param name="finalTimeStamp">Final TimeStamp</param>
        /// <returns>Enumerable of PlotEntity</returns>
        Task<IEnumerable<PlotQueryResultDto>> QueryByTimeFrame(int vId, DateTime initialTimeStamp, DateTime finalTimeStamp);

        /// <summary>
        /// Add IgnitionOn Plot
        /// </summary>
        /// <param name="entityVId">Vehicle Id</param>
        /// <param name="timeStamp">TimeStamp</param>
        /// <returns>Success</returns>
        Task<bool> AddIgnitionOn(int entityVId, DateTime timeStamp);

        /// <summary>
        /// Get last IgnitionOn
        /// </summary>
        /// <param name="entityVId">Vehicle Id</param>
        /// <param name="timeStamp">Final TimeStamp</param>
        /// <returns>Last Ignition On</returns>
        Task<DateTime?> GetLastIgnitionOn(int entityVId, DateTime timeStamp);
    }
}
