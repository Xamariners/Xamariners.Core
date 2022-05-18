// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataUpdateService.cs" company="">
//   
// </copyright>
// <summary>
//   The IDataUpdateService service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Xamariners.Core.Model.Enums;
using Xamariners.RestClient.Helpers.Models;

namespace Xamariners.Core.Service.Interface
{
    /// <summary>
    ///     The IDataUpdateService service.
    /// </summary>
    public interface IDataUpdateService
    {
        Task<ServiceResponse<byte[]>> GetDataUpdates(Guid targetId, ObjectType targetType, DateTime timestamp);
    }
}