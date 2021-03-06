﻿using BatchDecorator.API.Enums;
using BatchDecorator.API.Helpers;
using BatchDecorator.API.Services;
using Newtonsoft.Json;
using NLog;
using System.Collections.Generic;

namespace BatchDecorator.API.Decorators
{
    public class DecoratorFactory
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public IBatchProcess Component
        { get; private set; }

        public List<DecoratorType> DecoratorsSequence
        { get; set; }


        public DecoratorFactory(IBatchProcess component)
        {
            Component = component;
        }

        public IBatchProcess GetBatchProcess()
        {
            IBatchProcess result = Component;
            if (DecoratorsSequence != null)
            {
                foreach (var decorator in DecoratorsSequence)
                {
                    _logger.Info($"[DecoratorFactory:GetBatchProcess] {JsonConvert.SerializeObject(decorator)}");
                    result = result.Decorate(decorator);
                }
            }

            return result;
        }
    }

}
