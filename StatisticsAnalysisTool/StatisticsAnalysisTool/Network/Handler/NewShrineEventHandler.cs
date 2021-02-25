﻿using Albion.Network;
using StatisticsAnalysisTool.Common;
using StatisticsAnalysisTool.Enumerations;
using System.Threading.Tasks;

namespace StatisticsAnalysisTool.Network.Handler
{
    public class NewShrineEventHandler : EventPacketHandler<NewShrineEvent>
    {
        private readonly TrackingController _trackingController;
        public NewShrineEventHandler(TrackingController trackingController) : base((int) EventCodes.NewShrine)
        {
            _trackingController = trackingController;
        }

        protected override async Task OnActionAsync(NewShrineEvent value)
        {
            await Task.CompletedTask;
        }
    }
}