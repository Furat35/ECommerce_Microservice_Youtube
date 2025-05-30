﻿namespace EventBus.Base
{
    public class SubscriptionInfo(Type handlerType)
    {
        public Type HandlerType { get; } = handlerType;

        public static SubscriptionInfo Typed(Type handlerType)
        {
            return new SubscriptionInfo(handlerType);
        }
    }
}
