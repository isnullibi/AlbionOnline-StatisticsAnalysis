﻿using Albion.Network;
using StatisticsAnalysisTool.Enumerations;
using StatisticsAnalysisTool.Network.Manager;
using StatisticsAnalysisTool.Network.Operations.Responses;
using System.Threading.Tasks;

namespace StatisticsAnalysisTool.Network.Handler
{
    public class PartyMakeLeaderEventHandler : ResponsePacketHandler<PartyMakeLeaderResponse>
    {
        private readonly TrackingController _trackingController;

        public PartyMakeLeaderEventHandler(TrackingController trackingController) : base((int) OperationCodes.PartyMakeLeader)
        {
            _trackingController = trackingController;
        }

        protected override async Task OnActionAsync(PartyMakeLeaderResponse value)
        {
            await _trackingController.EntityController.RemoveFromParty(value.Username);
        }
    }
}