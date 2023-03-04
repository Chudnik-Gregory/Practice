using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class ExtractSearchA : ExtractSearchBase
    {
        public override string Name { get; } = nameof(ExtractSearchA);
    }
}
