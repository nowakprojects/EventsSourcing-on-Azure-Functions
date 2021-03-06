﻿using EventSourcingOnAzureFunctions.Common.EventSourcing.Interfaces;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;
using System.Reflection;
using System.Threading.Tasks;

namespace EventSourcingOnAzureFunctions.Common.Binding
{
    public sealed class ProjectionAttributeBinding
        : IBinding
    {
        private readonly ParameterInfo _parameter;
        private readonly IEventStreamSettings _eventStreamSettings;

        /// <summary>
        /// This binding gets its properties from the Attribute 
        /// </summary>
        public bool FromAttribute
        {
            get { return true; }
        }



        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            return Task.FromResult<IValueProvider>(new ProjectionValueBinder(_parameter,
                _eventStreamSettings ));
        }

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            // TODO: Perform any conversions on the incoming value
            return Task.FromResult<IValueProvider>(new ProjectionValueBinder(_parameter,
                _eventStreamSettings ));
        }



        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor
            {
                Name = _parameter.Name,
                DisplayHints = new ParameterDisplayHints
                {
                    Description = "The Projection to which this function can get its data",
                    DefaultValue = "[not applicable]",
                    Prompt = "Please enter a Projection"
                }
            };
        }

        /// <summary>
        /// Create a new binding for the projection
        /// </summary>
        /// <param name="parameter">
        /// Details of the attribute and parameter to be bound to
        /// </param>
        public ProjectionAttributeBinding(ParameterInfo parameter,
            IEventStreamSettings eventStreamSettings)
        {
            _parameter = parameter;
            _eventStreamSettings = eventStreamSettings;
        }
    }
}
