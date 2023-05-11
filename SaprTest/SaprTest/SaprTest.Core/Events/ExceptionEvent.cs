using Prism.Events;
using System;

namespace SaprTest.Core.Events;

public class ExceptionEvent : PubSubEvent<Exception>
{
}
